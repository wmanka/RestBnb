using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RestBnb.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void SetPrecisionForAllPropertiesOfTypeDecimal(this ModelBuilder modelBuilder, byte precision, byte scale)
        {
            foreach (var property in modelBuilder.Model
                .GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType($"decimal({precision}, {scale})");
            }
        }
    }
}
