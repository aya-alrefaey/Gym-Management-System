using Gym.Data.contexts;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataSeeder
{
    public static class PlanSeeder
    {
        public static async Task SeedPlans(GymDbcontext dbcontext)
        {
            if (await dbcontext.Plans.AnyAsync())
            {
                return;
            }
            var plans = new List<Plan>
            {
                new Plan
                {
                    Name = "Basic Plan",
                    Description = "Access to gym equipment during working hours.",
                    DurationDays = 30,
                    Price = 300,
                    IsActive = true
                },
                new Plan
                {
                    Name = "Standard Plan",
                    Description = "Gym access + 3 group sessions per week.",
                    DurationDays = 30,
                    Price = 500,
                    IsActive = true
                },
                new Plan
                {
                    Name = "Premium Plan",
                    Description = "Full access + unlimited sessions + personal trainer support.",
                    DurationDays = 30,
                    Price = 800,
                    IsActive = true
                },
                new Plan
                {
                    Name = "Quarter Plan",
                    Description = "3 months access with discounted price.",
                    DurationDays = 90,
                    Price = 1300,
                    IsActive = false
                },
                new Plan
                {
                    Name = "Half-Year Plan",
                    Description = "6 months full access with priority booking.",
                    DurationDays = 180,
                    Price = 2400,
                    IsActive = true
                },
                new Plan
                {
                    Name = "Annual Plan",
                    Description = "1 year membership with full benefits and best value.",
                    DurationDays = 365,
                    Price = 4500,
                    IsActive = true
                }
            };

            await dbcontext.Plans.AddRangeAsync(plans);
            await dbcontext.SaveChangesAsync();
        }
    }
}
