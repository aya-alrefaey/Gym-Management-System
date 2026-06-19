using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Gym.Models;

namespace Gym.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.HasIndex(x => new {
                x.MemberId,
                x.SessionId
            }).IsUnique();

            builder.HasQueryFilter(b => !b.IsDeleted);

            builder.HasOne(b => b.Member)
    .WithMany(m => m.Bookings)
    .HasForeignKey(b => b.MemberId)
    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
