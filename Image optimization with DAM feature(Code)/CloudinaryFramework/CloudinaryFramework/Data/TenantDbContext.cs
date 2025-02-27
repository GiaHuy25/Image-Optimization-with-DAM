using Microsoft.EntityFrameworkCore;
using CloudinaryFramework.Models;
namespace CloudinaryFramework.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
    }
}
