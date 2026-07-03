using Gym.DataAccess.Data.Identity;
using Gym.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gym.Data.contexts
{
    public class GymDbcontext : IdentityDbContext<ApplicationUser, ApplicationRole,string>
    {
        public GymDbcontext(DbContextOptions<GymDbcontext> options)
           : base(options)
        {

        }

        public DbSet<Person> Persons { get; set; }

        public DbSet<HealthRecord> HealthRecords { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Membership> Memberships { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Session> Sessions { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Trainer> Trainers { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GymDbcontext).Assembly);
        }
    }
}
