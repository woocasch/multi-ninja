using Microsoft.EntityFrameworkCore;

namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public sealed class ReadsContext : DbContext
{
    public DbSet<Credentials> Credentials { get; set; }

    public DbSet<User> Users { get; set; }

    public ReadsContext(DbContextOptions<ReadsContext> options)
        : base(options)
    {
    }
}