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
            optionsBuilder.UseSqlServer("Server=tcp:phloxdbserver.database.windows.net,1433;Initial Catalog=Phlox_db;Persist Security Info=False;User ID=voxels;Password=XS%cH429;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<HelpRequest> HelpRequests { get; set; }

    }
}
