namespace MultiNinja.Backend.Application.Repository.Credentials;

public sealed class SearchByEmailParameters
{
    public SearchByEmailParameters(string email)
    {
        this.Email = email;
    } 

    public string Email { get; }
}