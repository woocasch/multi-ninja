using System.Collections.ObjectModel;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Streams;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Infrastructure.Repository;

public sealed class StreamsRepository : IStreams
{
    private readonly Collection<StreamRecord> streams = [];

    private readonly Collection<EventRecord> events = [];

    private readonly Dictionary<string, int> processorPositions = [];

    public async Task CreateStream(CreateStreamParameters parameters, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var record = new StreamRecord(parameters.StreamId, parameters.EntityType, parameters.EntityId, 0);
        if (this.streams.Any(s => s.StreamId == record.StreamId && Equals(s.EntityType, record.EntityType)))
        {
            throw new InvalidOperationException("Stream already exists");
        }

        this.streams.Add(record);
    }

    public async Task<ulong> AddEvent(AddEventParameters parameters, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var record = new EventRecord(
            parameters.StreamId,
            parameters.EntityType,
            parameters.StorageDate,
            parameters.EventData,
            parameters.EntityVersion);
        var stream = this.streams
            .SingleOrDefault(s => s.StreamId == record.StreamId && Equals(s.EntityType, record.EntityType));
        if (stream is null)
        {
            throw new InvalidOperationException("Stream doesn't exist");
        }

        if (stream.Version != 0 && stream.Version != parameters.EntityVersion)
        {
            throw new InvalidOperationException(
                $"Versions mismatch (stream: {stream.Version}, record: {record.Version}).");
        }

        if (record.Version == 0)
        {
            var version = this.events
                .Where(e => e.StreamId == record.StreamId && Equals(e.EntityType, record.EntityType))
                .Max(e => e.Version);
            version++;
            record = record with { Version = version };
            var streamIndex = this.streams.IndexOf(stream);
            this.streams[streamIndex] = stream with { Version = version };
        }

        this.events.Add(record);
        return record.Version;
    }

    public async Task<EntityEvent?> GetNextUnprocessedEvent(string processorName, CancellationToken cancellationToken)
    {
        await Task.Yield();
        if (!this.processorPositions.TryGetValue(processorName, out var position))
        {
            this.processorPositions.Add(processorName, -1);
            return null;
        }
        
        var result = this.events
            .Index()
            .Where(e => e.Index > position)
            .OrderBy(e => e.Index)
            .Select(e => e.Item)
            .FirstOrDefault();
        return result?.EventData;
    }

    public async Task MarkEventAsProcessed(EntityEvent entityEvent, string processorName, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var record = this.events
            .Single(e => e.EventData == entityEvent);
        var index = this.events
            .IndexOf(record);
        if (!this.processorPositions.TryAdd(processorName, index))
        {
            this.processorPositions[processorName] = index;
        }
    }

    private record StreamRecord(Guid StreamId, EntityType EntityType, Guid EntityId, ulong Version);

    private record EventRecord(Guid StreamId, EntityType EntityType, DateTime StorageDate, EntityEvent EventData, ulong Version);
}