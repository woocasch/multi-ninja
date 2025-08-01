using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Streams;
using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Infrastructure.Repository.EfCore;

public class StreamsRepository : IStreams
{
    private static readonly JsonSerializerOptions SerializationOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    private readonly WriteContext writeContext;

    public StreamsRepository(WriteContext writeContext)
    {
        this.writeContext = writeContext;
    }

    public async Task CreateStream(CreateStreamParameters parameters, CancellationToken cancellationToken)
    {
        var payload = new Stream()
        {
            StreamId = parameters.StreamId,
            EntityType = parameters.EntityType.Name,
            EntityId = parameters.EntityId,
        };

        await this.writeContext.Streams.AddAsync(payload, cancellationToken);
    }

    public async Task<ulong> AddEvent(AddEventParameters parameters, CancellationToken cancellationToken)
    {
        var stream = await this.writeContext.Streams.Include(s => s.Events)
            .Where(s => s.StreamId == parameters.StreamId)
            .Where(s => s.EntityType == parameters.EntityType.Name)
            .FirstOrDefaultAsync(cancellationToken);
        if (stream is null)
        {
            stream = this.writeContext.ChangeTracker
                .Entries<Stream>()
                .Where(s => s.Entity.StreamId == parameters.StreamId)
                .Select(s => s.Entity)
                .FirstOrDefault();
            if (stream is null)
            {
                return 0;
            }
        }

        var eventData = Serialize(parameters.EventData);
        ulong eventVersion = 1;
        if (stream.Events.Count > 0)
        {
            eventVersion = stream.Events.Max(e => e.Version) + 1;
        }

        var payload = new Event()
        {
            Stream = stream,
            EntityType = parameters.EntityType.Name,
            EventTime = parameters.StorageDate,
            TypeName = eventData.TypeName,
            SerializedEvent = eventData.SerializedEvent,
            Version = eventVersion,
        };
        stream.Events.Add(payload);
        await this.writeContext.Events.AddAsync(payload, cancellationToken);
        return eventVersion;
    }

    public async Task<EntityEventEnvelope?> GetNextUnprocessedEvent(string processorName,
        CancellationToken cancellationToken)
    {
        var processor = this.writeContext.ProcessorProgresses
            .FirstOrDefault(p => p.ProcessorName == processorName);
        ulong lastProcessedId = 0;
        if (processor is not null)
        {
            lastProcessedId = processor.LastProcessedPosition;
        }

        var @event = await this.writeContext.Events
            .Where(e => e.Position > lastProcessedId)
            .OrderBy(e => e.Position)
            .FirstOrDefaultAsync(cancellationToken);
        if (@event is null)
        {
            return null;
        }

        var eventData = Deserialize(@event.TypeName, @event.SerializedEvent);
        return new(
            @event.StreamId,
            EntityType.FromName(@event.EntityType) ?? new EntityType(-1, "--"),
            @event.EventTime,
            eventData,
            @event.Version);
    }

    public async Task MarkEventAsProcessed(EntityEventEnvelope entityEvent, string processorName,
        CancellationToken cancellationToken)
    {
        var @event = this.writeContext.Events
            .Where(e => e.StreamId == entityEvent.StreamId)
            .Where(e => e.EntityType == entityEvent.EntityType.Name)
            .Where(e => e.Version == entityEvent.Version)
            .OrderBy(e => e.Position)
            .FirstOrDefault();
        if (@event is null)
        {
            return;
        }

        var processor = this.writeContext.ProcessorProgresses
            .FirstOrDefault(p => p.ProcessorName == processorName);
        if (processor is null)
        {
            processor = new ProcessorsProgress()
            {
                ProcessorName = processorName,
                LastProcessedPosition = @event.Position,
            };
            await this.writeContext.ProcessorProgresses.AddAsync(processor, cancellationToken);
        }
        else
        {
            processor.LastProcessedPosition = @event.Position;
            this.writeContext.ProcessorProgresses.Update(processor);
        }
    }

    private static (string TypeName, string SerializedEvent) Serialize(EntityEvent entityEvent)
    {
        var typeName = entityEvent.GetType().AssemblyQualifiedName!;
        var serializedEvent = JsonSerializer.Serialize(entityEvent, entityEvent.GetType(), SerializationOptions);
        return (TypeName: typeName, SerializedEvent: serializedEvent);
    }

    private static EntityEvent Deserialize(string typeName, string serializedEvent)
    {
        var type = Type.GetType(typeName)!;
        var result = JsonSerializer.Deserialize(serializedEvent, type, SerializationOptions);
        if (result is not EntityEvent cast)
        {
            throw new InvalidOperationException("Cannot deserialize");
        }

        return cast;
    }
}