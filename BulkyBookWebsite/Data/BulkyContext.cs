using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BulkyBookWebsite.Models;

namespace BulkyBookWebsite.Data
{
    public partial class BulkyDbContext : DbContext
    {
        public BulkyDbContext()
        {
        }

        public BulkyDbContext(DbContextOptions<BulkyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Catagory> Catagories { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catagory>(entity =>
            {
                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
