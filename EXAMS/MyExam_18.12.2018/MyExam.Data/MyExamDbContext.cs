namespace MyExam.Data
{
    using System;
    using DataModels;
    using DataModels.Enums;
    using Microsoft.EntityFrameworkCore;

    public class MyExamDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Cashier)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CashierId);

            modelBuilder.Entity<Receipt>()
                .HasOne(r => r.Cashier)
                .WithMany(c => c.Receipts)
                .HasForeignKey(r => r.CashierId);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion(s => s.ToString(), s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
        }
    }
}