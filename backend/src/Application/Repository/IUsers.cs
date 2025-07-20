using MultiNinja.Backend.Application.Repository.Users;

namespace MultiNinja.Backend.Application.Repository;

public interface IUsers
{
    Task CreateUser(CreateUserParameters parameters, CancellationToken cancellationToken);
}