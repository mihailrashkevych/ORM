using Microsoft.EntityFrameworkCore;
using ORM_work.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ORM_work
{
    class FacultiesContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Faculty> Faculties { get; set; }

        public FacultiesContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source = db.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Faculty>().HasMany(a => a.Groups).WithOne(b => b.Faculty);
            modelBuilder.Entity<Group>().HasMany(a => a.Students).WithOne(b => b.Group);
        }
    }
}
