using Catalog.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Category> categories { get; set; }
        public DbSet<Product> products { get; set; }
    }
}
