namespace MultiNinja.Backend.Application.ReadsRepository.Credentials;

public sealed class SearchByEmailParameters
{
    public SearchByEmailParameters(string userName)
    {
        this.UserName = userName;
    } 

    public string UserName { get; }
}