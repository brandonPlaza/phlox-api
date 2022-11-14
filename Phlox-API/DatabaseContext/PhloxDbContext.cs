namespace Phlox_API.DatabaseContext
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Phlox_API.Models;
    using System;
    using System.Collections.Generic;

    public class PhloxDbContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }

        public string DbPath { get; }

        public PhloxDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "phlox.db");

        }
        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite($"Data Source={DbPath}");
    }
}
