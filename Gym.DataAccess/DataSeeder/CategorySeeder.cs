using Gym.Data.contexts;
using Gym.Models;
using Microsoft.EntityFrameworkCore;

namespace Gym.DataSeeder
{
    public static class CategorySeeder
    {
        public  static async Task SeedCategories(GymDbcontext context) {
            if (await context.Categories.AnyAsync())
            {
                return;
            }
            var categories = new List<Category>
            {
                new Category { Name = "Cardio" },
                new Category { Name = "Strength Training" },
                new Category { Name = "Yoga" },
                new Category { Name = "CrossFit" },
                new Category { Name = "Boxing" }
            };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();
        }
    }
}
