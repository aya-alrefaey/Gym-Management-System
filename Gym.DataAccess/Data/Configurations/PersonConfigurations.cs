using Gym.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gym.Data.Configurations
{
    public class PersonConfigurations : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(u => u.Email)
                .IsRequired()
                . HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();
            builder.HasIndex(u => u.Phone).IsUnique();
            builder.OwnsOne(u => u.Address, a =>
            {
                a.Property(ad => ad.Street)
                .HasColumnName("Street")
                .HasMaxLength(50);

                a.Property(ad => ad.City)
               .HasColumnName("City")
               .HasMaxLength(50);

                a.Property(ad => ad.BuildingNo)
               .HasColumnName("BuildingNumber");
            });
            builder.HasQueryFilter(c => !c.IsDeleted);
            builder.ToTable(c => {
                c.HasCheckConstraint("Phone_Ck", "LEN(Phone)=11 AND Phone NOT LIKE '%[^0-9]%' AND Phone LIKE '01[0125]%'");
                c.HasCheckConstraint("Email_Ck", "Email Like '%@gmail.com'");
            });

            builder.HasDiscriminator<string>("UserType")
                .HasValue<Member>("Member")
                .HasValue<Trainer>("Trainer");
            builder.HasQueryFilter(u => !u.IsDeleted);

        }
    }
}
