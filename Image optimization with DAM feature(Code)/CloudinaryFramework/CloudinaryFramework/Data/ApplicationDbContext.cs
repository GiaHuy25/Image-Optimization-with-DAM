using CloudinaryFramework.Models;
using Microsoft.EntityFrameworkCore;
namespace CloudinaryFramework.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<ImageTransformation> ImageTransformations { get; set; }
        public DbSet<ImageMetadata> ImageMetadata { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Tenant>().HasMany(t => t.Images).WithOne(i => i.Tenant);
            modelBuilder.Entity<Image>().HasMany(i => i.Metadata).WithOne(m => m.Image);
        }
    }
}
