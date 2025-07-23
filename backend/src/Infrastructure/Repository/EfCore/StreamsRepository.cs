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
        var streams = await this.context.Streams
            .Where(s => s.StreamId == parameters.StreamId)
            .Where(s => s.EntityType == parameters.EntityType.Name)
            .ToListAsync(cancellationToken);
        if (streams.Count == 0)
        {
            return 0;
        }
        
        var stream = streams[0];
        var eventData = Serialize(parameters.EventData);
        var payload = new Event()
        {
            Stream = stream,
            EntityType = parameters.EntityType.Name,
            EventTime = parameters.StorageDate,
            TypeName = eventData.TypeName,
            SerializedEvent = eventData.SerializedEvent,
            Version = parameters.EntityVersion,
        };
        stream.Events.Add(payload);
        await this.context.SaveChangesAsync(cancellationToken);
        return parameters.EntityVersion;
    }

    public async Task<EntityEvent?> GetNextUnprocessedEvent(string processorName, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var processor = this.context.ProcessorProgresses
            .FirstOrDefault(p => p.ProcessorName == processorName);
        ulong lastProcessedId = 0;
        if (processor is not null)
        {
            lastProcessedId = processor.LastProcessedEventId;
        }

        var @event = this.context.Events
            .Where(e => e.Id > lastProcessedId)
            .OrderBy(e => e.Id)
            .FirstOrDefault();
        if (@event is null)
        {
            return null;
        }

        return Deserialize(@event.TypeName, @event.SerializedEvent);
    }

    public async Task MarkEventAsProcessed(EntityEvent entityEvent, string processorName, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var @event = this.context.Events
            .Where(e => e.Stream.StreamId == entityEvent.StreamId)
            .Where(e => e.EntityType == entityEvent.EntityType.Name)
            .Where(e => e.Version == entityEvent.Version)
            .OrderBy(e => e.Id)
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
                LastProcessedEventId = @event.Id,
            };
            await this.context.ProcessorProgresses.AddAsync(processor, cancellationToken);
            await this.context.SaveChangesAsync(cancellationToken);
        }
        else
        {
            processor.LastProcessedEventId = @event.Id;
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