using Gym.Data.contexts;
using Gym.DataAccess.DataSeeder;

namespace Gym.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task SeedAllData(GymDbcontext dbcontext)
        {
           await CategorySeeder.SeedCategories(dbcontext);
           await PlanSeeder.SeedPlans(dbcontext);
            await TrainerSeeder.SeedTrainers(dbcontext);
        }
    }
}
