using Microsoft.EntityFrameworkCore;
using RestBnb.Core.Entities;

namespace RestBnb.Infrastructure
{
    /// <summary>
    /// Represent a session with the database and can be used to query and save instances of entities
    /// </summary>
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}