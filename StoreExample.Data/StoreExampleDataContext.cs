using Microsoft.EntityFrameworkCore;
using StoreExample.Data.Models;

namespace StoreExample.Data;

public class StoreExampleDataContext(DbContextOptions<StoreExampleDataContext> options) : DbContext(options)
{
    public DbSet<Product> Product { get; set; } = default!;
    public DbSet<Customer> Customer { get; set; } = default!;
    public DbSet<Sale> Sale { get; set; } = default!;
    public DbSet<Seller> Seller { get; set; } = default!;
    public DbSet<ProductSale> ProductSale { get; set; } = default!;
}