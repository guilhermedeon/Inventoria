using Inventoria.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventoria.Infra.Data;

public class InventoriaDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=inventoria.db");
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
    public DbSet<MaintenanceSettings> MaintenanceSettings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User
        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .Property(u => u.Name)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        modelBuilder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(u => u.Inventories)
            .WithOne(i => i.User)
            .HasForeignKey(i => i.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Inventory
        modelBuilder.Entity<Inventory>()
            .HasKey(inv => inv.InventoryId);

        modelBuilder.Entity<Inventory>()
            .Property(inv => inv.UserId)
            .IsRequired();

        modelBuilder.Entity<Inventory>()
            .Property(inv => inv.Name)
            .IsRequired();

        modelBuilder.Entity<Inventory>()
            .Property(inv => inv.CreatedAt)
            .IsRequired();

        modelBuilder.Entity<Inventory>()
            .HasMany(inv => inv.Items)
            .WithOne(i => i.Inventory)
            .HasForeignKey(i => i.InventoryId)
            .OnDelete(DeleteBehavior.Cascade);

        // Item
        modelBuilder.Entity<Item>()
            .HasKey(i => i.ItemId);

        modelBuilder.Entity<Item>()
            .Property(i => i.InventoryId)
            .IsRequired();

        modelBuilder.Entity<Item>()
            .Property(i => i.Name)
            .IsRequired();

        modelBuilder.Entity<Item>()
            .Property(i => i.Quantity)
            .IsRequired();

        modelBuilder.Entity<Item>()
            .Property(i => i.IsConsumable)
            .IsRequired();

        modelBuilder.Entity<Item>()
            .Property(i => i.CreatedAt)
            .IsRequired();

        modelBuilder.Entity<Item>()
            .HasMany(i => i.Tags)
            .WithMany(t => t.Items);

        modelBuilder.Entity<Item>()
            .HasMany(i => i.MaintenanceRecords)
            .WithOne(mr => mr.Item)
            .HasForeignKey(mr => mr.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Item>()
            .HasOne(i => i.MaintenanceSettings)
            .WithOne(ms => ms.Item)
            .HasForeignKey<MaintenanceSettings>(ms => ms.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        // Tag
        modelBuilder.Entity<Tag>()
            .HasKey(t => t.TagId);

        modelBuilder.Entity<Tag>()
            .Property(t => t.Name)
            .IsRequired();

        // MaintenanceRecord
        modelBuilder.Entity<MaintenanceRecord>()
            .HasKey(mr => mr.MaintenanceId);

        modelBuilder.Entity<MaintenanceRecord>()
            .Property(mr => mr.ItemId)
            .IsRequired();

        modelBuilder.Entity<MaintenanceRecord>()
            .Property(mr => mr.DatePerformed)
            .IsRequired();

        // MaintenanceSettings
        modelBuilder.Entity<MaintenanceSettings>()
            .HasKey(ms => ms.ItemId);

        modelBuilder.Entity<MaintenanceSettings>()
            .Property(ms => ms.ItemId)
            .IsRequired();
    }

}