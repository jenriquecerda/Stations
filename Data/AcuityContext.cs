using Acuity.Models;
using Microsoft.EntityFrameworkCore;

namespace Acuity.Data {
    public class AcuityContext : DbContext {
        public AcuityContext(DbContextOptions<AcuityContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Part>().ToTable("Part");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Bom>().ToTable("Bom");
        }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Product> Product { get; set; }

        public DbSet<Bom> Bom { get; set; }
    }
}
