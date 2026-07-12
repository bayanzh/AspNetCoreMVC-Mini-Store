using Microsoft.EntityFrameworkCore;
using Mini_Store.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Mini_Store.Data
{
   using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
}
}