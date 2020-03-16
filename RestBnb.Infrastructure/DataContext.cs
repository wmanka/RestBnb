using Microsoft.EntityFrameworkCore;
using RestBnb.Core.Entities;

namespace RestBnb.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}