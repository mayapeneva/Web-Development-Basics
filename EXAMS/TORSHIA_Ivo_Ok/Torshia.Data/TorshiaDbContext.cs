namespace Torshia.Data
{
    using System;
    using Common;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class TorshiaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Task> Tasks { get; set; }

        public DbSet<UserTask> UserTasks { get; set; }

        public DbSet<TaskSector> TaskSectors { get; set; }

        public DbSet<Report> Reports { get; set; }

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
            modelBuilder.Entity<UserTask>()
                .HasKey(ut => new { ut.UserId, ut.TaskId });

            modelBuilder.Entity<UserTask>()
                .HasOne(ut => ut.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserTask>()
                .HasOne(ut => ut.Task)
                .WithMany(t => t.Participants)
                .HasForeignKey(t => t.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion(r => r.ToString(), r => (Role)Enum.Parse(typeof(Role), r));

            modelBuilder.Entity<Report>()
                .Property(r => r.Status)
                .HasConversion(s => s.ToString(), s => (Status)Enum.Parse(typeof(Status), s));

            modelBuilder.Entity<TaskSector>()
                .Property(ts => ts.Sector)
                .HasConversion(s => s.ToString(), s => (Sector)Enum.Parse(typeof(Sector), s));
        }
    }
}