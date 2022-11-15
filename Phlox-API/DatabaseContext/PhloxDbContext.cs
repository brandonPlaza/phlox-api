namespace Phlox_API.DatabaseContext
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Phlox_API.Models;
    using System;
    using System.Collections.Generic;

    public class PhloxDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }

        public PhloxDbContext(DbContextOptions<PhloxDbContext> options): base(options)
        {
        }
    }
}
