using Acuity.Models;
using Microsoft.EntityFrameworkCore;

namespace Acuity.Data {
    public class AcuityContext : DbContext {
        public AcuityContext(DbContextOptions<AcuityContext> options) : base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Part>().ToTable("Part");
        }

        public DbSet<Part> Parts { get; set; }
    }
}
