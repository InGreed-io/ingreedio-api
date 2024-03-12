using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InGreedIoApi.Model;

namespace InGreedIoApi.Data;

public class ApiDbContext : IdentityDbContext<User>
{
    public ApiDbContext()
    { }

    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Featuring)
            .WithOne(f => f.Product)
            .HasForeignKey<Featuring>(f => f.ProductId);
    }

    // Example - TODO: fill it
    // public virtual DbSet<Address> Addresses { get; set; }
    public DbSet<Product> Products { get; set; }

    public DbSet<Category> Category { get; set; }

    public DbSet<Featuring> Features { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }

    public DbSet<Review> Reviews { get; set; }
}