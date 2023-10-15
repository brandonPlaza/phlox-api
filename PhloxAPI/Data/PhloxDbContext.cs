using Microsoft.EntityFrameworkCore;
using PhloxAPI.Models.Entities;

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
      //optionsBuilder.UseSqlite("Filename=database.db");
      //optionsBuilder.UseSqlServer();
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Node> Nodes { get; set; }
    public DbSet<Neighbor> Neighbors { get; set; }
    public DbSet<Cardinality> Cardinalities { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<HelpRequest> HelpRequests { get; set; }
    public DbSet<OutOfService> OutOfService { get; set; }

  }
}
