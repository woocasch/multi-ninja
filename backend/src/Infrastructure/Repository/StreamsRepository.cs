using System.Collections.ObjectModel;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Streams;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Infrastructure.Repository;

public class StreamsRepository : IStreams
{
    private readonly Collection<StreamRecord> streams = [];

    private readonly Collection<EventRecord> events = [];

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

    private record StreamRecord(Guid StreamId, EntityType EntityType, Guid EntityId, ulong Version);

    private record EventRecord(Guid StreamId, EntityType EntityType, EntityEvent EventData, ulong Version);
}