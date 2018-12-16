namespace PANDA.Data
{
    using System;
    using Common;
    using DataModels;
    using DataModels.Enums;
    using Microsoft.EntityFrameworkCore;

    public class PandaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Package> Packages { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Constants.ConnectionString).UseLazyLoadingProxies();
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Recipient)
                .WithMany(r => r.Packages)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            modelBuilder.Entity<Package>()
                .Property(p => p.PackageStatus)
                .HasConversion(p => p.ToString(), p => (PackageStatus)Enum.Parse(typeof(PackageStatus), p));
        }
    }
}