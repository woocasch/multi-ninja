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

    private readonly WriteContext context;

    public StreamsRepository(WriteContext context)
    {
        this.context = context;
    }

    public async Task CreateStream(CreateStreamParameters parameters, CancellationToken cancellationToken)
    {
        var payload = new Stream()
        {
            StreamId = parameters.StreamId,
            EntityType = parameters.EntityType.Name,
            EntityId = parameters.EntityId,
        };
        await this.context.Streams.AddAsync(payload, cancellationToken);
        await this.context.SaveChangesAsync(cancellationToken);
    }

    public async Task<ulong> AddEvent(AddEventParameters parameters, CancellationToken cancellationToken)
    {
        var streams = await this.context.Streams.Include(s => s.Events)
            .Where(s => s.StreamId == parameters.StreamId)
            .Where(s => s.EntityType == parameters.EntityType.Name)
            .ToListAsync(cancellationToken);
        if (streams.Count == 0)
        {
            return 0;
        }
        
        var stream = streams[0];
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
        await this.context.SaveChangesAsync(cancellationToken);
        return eventVersion;
    }

    public async Task<EntityEventEnvelope?> GetNextUnprocessedEvent(string processorName, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var processor = this.context.ProcessorProgresses
            .FirstOrDefault(p => p.ProcessorName == processorName);
        ulong lastProcessedId = 0;
        if (processor is not null)
        {
            lastProcessedId = processor.LastProcessedPosition;
        }

        var @event = await this.context.Events
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

    public async Task MarkEventAsProcessed(EntityEventEnvelope entityEvent, string processorName, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var @event = this.context.Events
            .Where(e => e.StreamId == entityEvent.StreamId)
            .Where(e => e.EntityType == entityEvent.EntityType.Name)
            .Where(e => e.Version == entityEvent.Version)
            .OrderBy(e => e.Position)
            .FirstOrDefault();
        if (@event is null)
        {
            return;
        }

        var processor = this.context.ProcessorProgresses
            .FirstOrDefault(p => p.ProcessorName == processorName);
        if (processor is null)
        {
            processor = new ProcessorsProgress()
            {
                ProcessorName = processorName,
                LastProcessedPosition = @event.Position,
            };
            await this.context.ProcessorProgresses.AddAsync(processor, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            processor.LastProcessedPosition = @event.Position;
            await this.context.SaveChangesAsync(cancellationToken);
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