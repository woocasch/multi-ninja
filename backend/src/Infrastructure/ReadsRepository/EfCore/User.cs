namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public sealed class User
{
    public Guid UserId { get; set; }

    public string DisplayName { get; set; } = string.Empty;
}