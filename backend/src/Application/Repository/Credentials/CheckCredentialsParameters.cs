namespace MultiNinja.Backend.Application.Repository.Credentials;

public sealed class CheckCredentialsParameters
{
    public CheckCredentialsParameters(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }

    public string Email { get; set; }

    public string Password { get; set; }
}