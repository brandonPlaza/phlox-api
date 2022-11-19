using Microsoft.EntityFrameworkCore;
using PhloxAPI.Models;

namespace PhloxAPI.Data
{
    public class PhloxDbContext : DbContext
    {
        public PhloxDbContext(DbContextOptions<PhloxDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=phloxDb;Trusted_Connection=True;TrustServerCertificate=true");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Building> Buildings { get; set; }

    }
}
