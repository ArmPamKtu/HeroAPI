using System;
using Dto;
using Db.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Db
{
    public class HeroDbContext : IdentityDbContext
    {
        public HeroDbContext(DbContextOptions options) : base(options)
        {
        }

       // public DbSet<User> User { get; set; }
        public DbSet<Feat> Feat { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductVersion> ProductVersion { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        /// <summary>
        ///     Add any Db Configurations here
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          
        }
    }
}
