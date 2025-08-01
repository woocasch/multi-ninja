using MultiNinja.Backend.Application.ReadsRepository.Users;

namespace MultiNinja.Backend.Application.ReadsRepository;

public interface IUsers
{
    Task CreateUser(CreateUserParameters parameters, CancellationToken cancellationToken);
}