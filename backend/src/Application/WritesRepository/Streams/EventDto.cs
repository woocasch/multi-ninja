using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WritesRepository.Streams;

public class EventDto
{
    public EventDto(EntityEventEnvelope envelopedEvent)
    {
        this.EnvelopedEvent = envelopedEvent;
    }

    public EntityEventEnvelope EnvelopedEvent { get; }
}