using Microsoft.EntityFrameworkCore;
using SanctionScanner.DAL.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanctionScanner.DAL.Model.Context
{
    public class SanctionScannerDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=localhost;Database=SanctionScanner;Trusted_Connection=True");
        }


        public DbSet<Cost> Costs { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }

    }
}
