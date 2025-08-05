using Microsoft.EntityFrameworkCore;

namespace MultiNinja.Backend.Infrastructure.WritesRepository.EfCore;

public sealed class WriteContext : DbContext
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
            entity.HasKey(e => e.StreamId);
            entity.Property(e => e.StreamId)
                .HasColumnType("binary(16)")
                .IsRequired();
            entity.Property(e => e.EntityType)
                .IsRequired();
            entity.Property(e => e.EntityId)
                .HasColumnType("binary(16)")
                .IsRequired();
            entity.HasMany(e => e.Events)
                .WithOne(e => e.Stream)
                .HasForeignKey(e => e.StreamId);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId);
            entity.Property(e => e.EventId)
                .HasColumnType("binary(16)")
                .IsRequired();
            entity.Property(e => e.StreamId)
                .HasColumnType("binary(16)")
                .IsRequired();
            entity.Property(e => e.EntityType).IsRequired();
            entity.Property(e => e.EventTime).IsRequired();
            entity.Property(e => e.Version).IsRequired();
            entity.Property(e => e.Position).IsRequired()
                .ValueGeneratedOnAdd();
            entity
                .HasAlternateKey(e => e.Position);
        });

        modelBuilder.Entity<ProcessorsProgress>(entity => { entity.HasKey(e => e.ProcessorName); });
    }
}