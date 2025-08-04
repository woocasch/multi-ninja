namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public sealed class Credentials
{
    public required Guid Id { get; set; }
    
    public required Guid UserId { get; set; }

    public required string UserName { get; set; } = string.Empty;
}