using Microsoft.EntityFrameworkCore;

namespace InGreedIoApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext() { }
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

    // Example - TODO: fill it
    // public virtual DbSet<Address> Addresses { get; set; }
}
