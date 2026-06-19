using Gym.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.Configurations
{
    public class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.HasIndex(m => new { m.MemberId, m.StartDate, m.EndDate }).IsUnique();
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "Membership_Date_CK",
                    "[EndDate] > [StartDate]"
                );
            });
            builder.HasQueryFilter(m => !m.IsDeleted);

        }
    }
}
