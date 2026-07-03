using Gym.Data.contexts;
using Gym.DataAccess.Data.Identity;
using Gym.DataAccess.DataSeeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gym.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task SeedAllData(GymDbcontext dbcontext, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager,IConfiguration config)
        {
            await UserSeeder.SeedAsync(userManager, roleManager,config);
            await CategorySeeder.SeedCategories(dbcontext);
           await PlanSeeder.SeedPlans(dbcontext);
            await TrainerSeeder.SeedTrainers(dbcontext);
        }
    }
}
