using System;
using Dto;
using Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Db
{
    public class HeroDbContext : DbContext
    {
        public HeroDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }

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
