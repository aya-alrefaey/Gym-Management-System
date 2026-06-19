using Gym.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.Configurations
{
    public class PlanConfiguration:IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {


            builder.Property(p => p.Name)
                          .IsRequired()
                          .HasMaxLength(50);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.DurationDays)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

          
            builder.ToTable(t =>
            {
                t.HasCheckConstraint(
                    "CK_Plan_Duration",
                    "[DurationDays] BETWEEN 1 AND 365"
                );

               
            });

 
            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.HasIndex(p => p.IsActive);
            builder.HasQueryFilter(p => !p.IsDeleted );
        }
    }
}