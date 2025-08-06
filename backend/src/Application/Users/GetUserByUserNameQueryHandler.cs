using MultiNinja.Backend.Application.ReadsRepository;

namespace MultiNinja.Backend.Application.Users;

public class GetUserByUserNameQueryHandler : QueryHandlerBase<GetUserByUserNameQuery, GetUserResult>
{
    private readonly ICredentials credentials;

    private readonly IUsers users;

    public GetUserByUserNameQueryHandler(ICredentials credentials, IUsers users)
    {
        this.credentials = credentials;
        this.users = users;
    }

    protected override async Task<GetUserResult> Fetch(GetUserByUserNameQuery query, CancellationToken cancellationToken)
    {
        var credentialsData = await this.credentials.SearchByUserName(new(query.UserName), cancellationToken);
        if (credentialsData is null)
        {
            return new(null);
        }

        var userData = await this.users.GetUserById(new(credentialsData.UserId), cancellationToken);
        if (userData is null)
        {
            return new(null);
        }

        return new(new GetUserResult.UserData(userData.Id, userData.DisplayName));
    }
}