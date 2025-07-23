using Microsoft.EntityFrameworkCore;

namespace MultiNinja.Backend.Infrastructure.Repository.EfCore;

public class WriteContext : DbContext
{
    public DbSet<Stream> Streams { get; set; }

    public DbSet<Event> Events { get; set; }

    public WriteContext(DbContextOptions<WriteContext> options)
        : base(options)
    {
    }

    public DbSet<ProcessorsProgress> ProcessorProgresses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Stream>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StreamId).IsRequired();
            entity.Property(e => e.EntityType).IsRequired();
            entity.Property(e => e.EntityId).IsRequired();
            entity.HasMany(e => e.Events)
                .WithOne(e => e.Stream)
                .HasForeignKey(e => e.StreamId);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.StreamId).IsRequired();
            entity.Property(e => e.EntityType).IsRequired();
            entity.Property(e => e.EventTime).IsRequired();
            entity.Property(e => e.Version).IsRequired();
        });

        modelBuilder.Entity<ProcessorsProgress>()
            .HasNoKey()
            .HasIndex(e => new { e.ProcessorName }, "Processors_ProcessorName");
    }
}