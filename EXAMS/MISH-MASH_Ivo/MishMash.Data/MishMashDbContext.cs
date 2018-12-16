namespace MishMash.Data
{
    using System;
    using Common;
    using DataModels;
    using DataModels.Enums;
    using Microsoft.EntityFrameworkCore;

    public class MishMashDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<UsersChannels> UsersChannels { get; set; }

        public DbSet<Tag> Tags { get; set; }

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
            modelBuilder.Entity<UsersChannels>()
                .HasKey(uc => new { uc.UserId, uc.ChannelId });

            modelBuilder.Entity<Channel>()
                .Property(c => c.Type)
                .HasConversion(t => t.ToString(), t => (ChannelType)Enum.Parse(typeof(ChannelType), t));
        }
    }
}