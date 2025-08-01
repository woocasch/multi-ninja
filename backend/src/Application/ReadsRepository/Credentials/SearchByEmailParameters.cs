namespace MultiNinja.Backend.Application.ReadsRepository.Credentials;

public sealed class SearchByEmailParameters
{
    public SearchByEmailParameters(string email)
    {
        this.Email = email;
    } 

    public string Email { get; }
}