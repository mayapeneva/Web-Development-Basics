namespace Chushka.Data
{
    using System;
    using Common;
    using DataModels;
    using DataModels.Enum;
    using Microsoft.EntityFrameworkCore;

    public class ChushkaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

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
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            modelBuilder.Entity<Product>()
                .Property(p => p.Type)
                .HasConversion(t => t.ToString(), t => (ProductType)Enum.Parse(typeof(ProductType), t));

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}