namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public class Credentials
{
    public required Guid Id { get; set; }
    
    public required Guid UserId { get; set; }

    public required string UserName { get; set; } = string.Empty;

    public required string Password { get; set; } = string.Empty;
}