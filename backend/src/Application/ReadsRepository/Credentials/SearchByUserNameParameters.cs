namespace MultiNinja.Backend.Application.ReadsRepository.Credentials;

public sealed class SearchByUserNameParameters
{
    public SearchByUserNameParameters(string userName)
    {
        this.UserName = userName;
    } 

    public string UserName { get; }
}