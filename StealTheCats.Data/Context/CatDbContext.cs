using Microsoft.EntityFrameworkCore;
using StealTheCats.Data.Models;

namespace StealTheCats.Data.Context;

public class CatDbContext : DbContext
{
    public CatDbContext(DbContextOptions<CatDbContext> options) : base(options) { }

    public DbSet<CatEntity> Cats { get; set; }
    public DbSet<TagEntity> Tags { get; set; }
    public DbSet<CatTag> CatTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Cat Entity Configuration
        modelBuilder.Entity<CatEntity>(entity =>
        {
            // Primary Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.CatId)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.Width)
                .IsRequired();

            entity.Property(e => e.Height)
                .IsRequired();

            entity.Property(e => e.Image)
                .IsRequired();

            entity.Property(e => e.Created)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            entity.HasIndex(e => e.CatId)
                .IsUnique();

            entity.ToTable("Cats");
        });

        // Tag Entity Configuration
        modelBuilder.Entity<TagEntity>(entity =>
        {
            // Primary Key
            entity.HasKey(e => e.Id);

            // Properties
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(e => e.Created)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            // Indexes
            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.ToTable("Tags");
        });

        // Many-to-Many Relationship Configuration
        modelBuilder.Entity<CatTag>(entity =>
        {
            entity.HasKey(ct => new { ct.CatId, ct.TagId }); // Composite Key

            entity.HasOne(ct => ct.Cat)
                .WithMany(c => c.CatTags)
                .HasForeignKey(ct => ct.CatId);

            entity.HasOne(ct => ct.Tag)
                .WithMany(t => t.CatTags)
                .HasForeignKey(ct => ct.TagId);

            entity.ToTable("CatTags");
        });
    }
}
