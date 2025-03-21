using Microsoft.EntityFrameworkCore;
using Vesma.Data.Models;

namespace Vesma.Data;

public class VesmaDbContext(DbContextOptions<VesmaDbContext> options) : DbContext(options)
{
    public DbSet<Vessel> Vessels { get; set; } = null!;

    public DbSet<IMO> IMOs { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Vessel>(e =>
        {
            e.HasKey(x => x.Id);

            e.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();

            e.Property(x => x.Type)
            .HasMaxLength(255)
            .IsRequired();

            e.HasOne<IMO>(x => x.Imo)
            .WithMany()
            .HasForeignKey(x => x.ImoId);
        });

        builder.Entity<IMO>(e =>
        {
            e.HasKey(x => x.Id);

            e.HasIndex(x => x.Name)
            .IsUnique();

            e.Property(x => x.Name)
            .HasMaxLength(255)
            .IsRequired();
        });
    }
}