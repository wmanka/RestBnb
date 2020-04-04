using Microsoft.EntityFrameworkCore;
using RestBnb.Core.Entities;
using RestBnb.Core.Enums;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRole>().ToTable("UserRoles");
            modelBuilder.Entity<Property>().ToTable("Properties");
            modelBuilder.Entity<Booking>().ToTable("Bookings");
            modelBuilder.Entity<Country>().ToTable("Countries");
            modelBuilder.Entity<State>().ToTable("States");
            modelBuilder.Entity<City>().ToTable("Cities");

            modelBuilder.Entity<Booking>()
                .HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Booking>()
                .Property(x => x.BookingState)
                .HasConversion(
                    v => v.ToString(),
                    v => (BookingState)Enum.Parse(typeof(BookingState), v));

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

            // .IgnoreQueryFilters() to disable
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Property>().HasQueryFilter(p => !p.IsDeleted);
            //modelBuilder.Entity<Booking>().HasQueryFilter(p => !p.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            OnBeforeSaving();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            OnBeforeSaving();

            return base.SaveChanges();
        }

        private void OnBeforeSaving()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.CurrentValues["Created"] = DateTime.UtcNow;
                            entry.CurrentValues["Modified"] = DateTime.UtcNow;
                            entry.CurrentValues["IsDeleted"] = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.CurrentValues["IsDeleted"] = true;
                            entry.CurrentValues["Modified"] = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            entry.CurrentValues["Modified"] = DateTime.UtcNow;
                            break;
                    }
                }
            }
        }
    }
}