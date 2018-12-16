namespace MyExam.Data
{
    using System;
    using System.Linq;
    using Common;
    using DataModel;
    using DataModel.Enums;
    using Microsoft.EntityFrameworkCore;

    public class MyExamDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Constants.ConnectionString).UseLazyLoadingProxies();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            modelBuilder.Entity<Report>()
                .Property(r => r.Status)
                .HasConversion(rs => rs.ToString(), rs => (ReportStatus)Enum.Parse(typeof(ReportStatus), rs));

            modelBuilder.Entity<TaskSector>()
                .Property(ts => ts.Sector)
                .HasConversion(s => s.ToString(), s => (Sector)Enum.Parse(typeof(Sector), s));
        }
    }
}