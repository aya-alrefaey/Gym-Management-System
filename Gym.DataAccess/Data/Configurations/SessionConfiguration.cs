using Gym.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.Configurations
{
    public class SessionConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable(S =>
            {
                S.HasCheckConstraint("Capacity_CK","Capacity between 1 and 25");
                S.HasCheckConstraint("Date_CK", "EndDate >= StartDate");

            });

            builder.HasQueryFilter(s => !s.IsDeleted);
        }
    }
}
