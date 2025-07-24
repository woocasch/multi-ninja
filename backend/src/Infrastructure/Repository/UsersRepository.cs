using System.Collections.ObjectModel;
using MultiNinja.Backend.Application.Repository;
using MultiNinja.Backend.Application.Repository.Users;

namespace MultiNinja.Backend.Infrastructure.Repository;

public class UsersRepository : IUsers
{
    private readonly Collection<User> users = [];
    
    public async Task CreateUser(CreateUserParameters parameters, CancellationToken cancellationToken)
    {
        await Task.Yield();
        var user = new User(parameters.UserId, parameters.DisplayName);
        this.users.Add(user);
    }

    private record User(Guid Id, string DisplayName);
}