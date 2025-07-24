using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WriteModelProcessing;

public abstract class EventHandlerBase<TEvent> : IEventHandler
    where TEvent : EntityEvent
{
    public async Task Handle(EntityEvent entityEvent, CancellationToken cancellationToken)
    {
        if (entityEvent is not TEvent cast)
        {
            throw new ArgumentException(
                EventHandlerBaseResources.InvalidEventPassed,
                nameof(entityEvent));
        }
        
        await this.Handle(cast, cancellationToken);
    }
    
    protected abstract Task Handle(TEvent entityEvent, CancellationToken cancellationToken);
}