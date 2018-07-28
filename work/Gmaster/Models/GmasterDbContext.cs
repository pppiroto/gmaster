using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmaster.Models
{
    public class GmasterDbContext : DbContext
    {
        public GmasterDbContext(DbContextOptions<GmasterDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tables primary key
            modelBuilder.Entity<Tables>()
                .HasKey(c => new { c.tabschema, c.tabname });

            // Columns primary key
            modelBuilder.Entity<Columns>()
                .HasKey(c => new { c.tabschema, c.tabname, c.colname });
        }
        public virtual DbSet<Tables> Talbes { get; set; }
        public virtual DbSet<Columns> Columns { get; set; }
    }
}
