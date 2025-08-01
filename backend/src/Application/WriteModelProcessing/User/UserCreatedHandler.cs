using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Application.ReadsRepository.Users;
using MultiNinja.Backend.Domain.Users;

namespace MultiNinja.Backend.Application.WriteModelProcessing.User;

public class UserCreatedHandler : EventHandlerBase<UserCreated>
{
    private readonly IUsers users;

    public UserCreatedHandler(IUsers users)
    {
        this.users = users;
    }

    protected override async Task Handle(UserCreated entityEvent, CancellationToken cancellationToken)
    {
        var parameters = new CreateUserParameters(
            entityEvent.UserId,
            entityEvent.DisplayName);
        await this.users.CreateUser(parameters, cancellationToken);
    }
}