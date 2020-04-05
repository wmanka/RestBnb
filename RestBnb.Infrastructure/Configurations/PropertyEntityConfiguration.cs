using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestBnb.Core.Entities;

namespace RestBnb.Infrastructure.Configurations
{
    public class PropertyEntityConfiguration : IEntityTypeConfiguration<Property>
    {
        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.ToTable("Properties");

            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}

