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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Credentials>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .HasColumnType("binary(16)")
                .IsRequired();
            entity.Property(e => e.UserId)
                .HasColumnType("binary(16)")
                .IsRequired();
            entity.Property(e => e.UserName)
                .HasColumnType("varchar(50)")
                .IsRequired();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.UserId)
                .HasColumnType("binary(16)")
                .IsRequired();

            entity.Property(e => e.DisplayName)
                .HasColumnType("varchar(50)")
                .IsRequired();
        });
    }
}