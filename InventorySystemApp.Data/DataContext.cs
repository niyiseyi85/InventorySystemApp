using InventorySystemApp.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySystemApp.Data
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Inventory> Inventory { get; set; } 
    public virtual DbSet<ProductInventory> ProductInventory { get; set; }
    //public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<ProductTransaction> ProductTransactions { get; set; }
    public virtual DbSet<InventoryTransaction> InventoryTransactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<ProductInventory>()
        .HasKey(pi => new { pi.ProductId, pi.InventoryId });

      modelBuilder.Entity<Product>()
        .HasMany(p => p.ProductInventories)
        .WithOne(p => p.Product)
        .HasForeignKey(pk => pk.ProductId);

      modelBuilder.Entity<Inventory>()
        .HasMany(I => I.ProductInventories)
        .WithOne(I => I.Inventory)
        .HasForeignKey(fk => fk.InventoryId);
    }
  }
}
