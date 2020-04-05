using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestBnb.Core.Entities;

namespace RestBnb.Infrastructure.Configurations
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            // .IgnoreQueryFilters() to disable
            builder.HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
