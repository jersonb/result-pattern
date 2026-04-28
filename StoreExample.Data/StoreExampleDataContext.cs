using Microsoft.EntityFrameworkCore;
using StoreExample.Data.Models;

namespace StoreExample.Data;

public class StoreExampleDataContext(DbContextOptions<StoreExampleDataContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(x => x.Sales)
            .WithMany(x => x.Products)
            .UsingEntity<ProductSale>();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Product> Product { get; set; } = default!;
    public DbSet<Customer> Customer { get; set; } = default!;
    public DbSet<Sale> Sale { get; set; } = default!;
    public DbSet<Seller> Seller { get; set; } = default!;
    public DbSet<ProductSale> ProductSale { get; set; } = default!;
}