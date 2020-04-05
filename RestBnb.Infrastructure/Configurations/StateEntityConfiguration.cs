using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestBnb.Core.Entities;

namespace RestBnb.Infrastructure.Configurations
{
    public class StateEntityConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.ToTable("States");
        }
    }
}
