using Microsoft.EntityFrameworkCore;
using RestBnb.Core.Entities;
using System.Linq;

namespace RestBnb.Infrastructure
{
    /// <summary>
    ///     Represent a session with the database and can be used to query and save instances of entities
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<Property>().ToTable("Properties");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<State>().ToTable("States");
            modelBuilder.Entity<City>().ToTable("Cities");

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)))
            {
                property.SetColumnType("decimal(6, 2)");
            }

            modelBuilder.Entity<UserRole>()
            .HasKey(t => new { t.UserId, t.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(pt => pt.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(pt => pt.Role)
                .WithMany(t => t.UserRoles)
                .HasForeignKey(pt => pt.RoleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}