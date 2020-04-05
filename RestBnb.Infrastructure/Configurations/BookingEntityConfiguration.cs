using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestBnb.Core.Entities;
using RestBnb.Core.Enums;
using System;

namespace RestBnb.Infrastructure.Configurations
{
    public class BookingEntityConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("Bookings");

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Bookings)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .Property(x => x.BookingState)
                .HasConversion(
                    v => v.ToString(),
                    v => (BookingState)Enum.Parse(typeof(BookingState), v));
        }
    }
}
