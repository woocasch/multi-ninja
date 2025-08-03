using Microsoft.EntityFrameworkCore;

namespace MultiNinja.Backend.Infrastructure.ReadsRepository.EfCore;

public class ReadsContext : DbContext
{
    public DbSet<Credentials> Credentials { get; set; }

    public ReadsContext(DbContextOptions<ReadsContext> options)
        : base(options)
    {
    }
}