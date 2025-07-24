using MultiNinja.Backend.Domain;

namespace MultiNinja.Backend.Application.WriteModelProcessing;

public interface IEventHandler
{
    Task Handle(EntityEvent entityEvent, CancellationToken cancellationToken);
}