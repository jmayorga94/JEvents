using JEvents.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JEvents.API.Context
{
    public class JEventsContext : DbContext
    {
        public JEventsContext(DbContextOptions<JEventsContext> options) : base(options)
        {

        }
        public DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().Property(x => x.Id).ValueGeneratedOnAdd();
        }

    }
}
