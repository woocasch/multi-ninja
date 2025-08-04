using MultiNinja.Backend.Application.ReadsRepository;
using MultiNinja.Backend.Application.ReadsRepository.Users;

namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public sealed class UsersRepository : IUsers
{
    private readonly ReadsContext readsContext;

    public UsersRepository(ReadsContext readsContext)
    {
        this.readsContext = readsContext;
    }

    public async Task CreateUser(CreateUserParameters parameters, CancellationToken cancellationToken)
    {
        var payload = new User()
        {
            UserId = parameters.UserId,
            DisplayName = parameters.DisplayName,
        };
        
        await this.readsContext.Users.AddAsync(payload, cancellationToken);
    }
}