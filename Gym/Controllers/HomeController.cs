using Gym.BusinessLogic.Services;
using Gym.Models;
using Gym.BusinessLogic.ViewModels.Home;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gym.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService memberService;
        private readonly IService<Trainer> trainerService;
        private readonly ISessionService sessionService;

        public HomeController(
            IMemberService memberService,
            IService<Trainer> trainerService,
            ISessionService sessionService)
        {
            this.memberService = memberService;
            this.trainerService = trainerService;
            this.sessionService = sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var members = await memberService.GetAllAsync(cancellationToken);
            var trainers = await trainerService.GetAllAsync(cancellationToken);
            var sessions = await sessionService.GetAllSessions(cancellationToken);

            var model = new HomeStatsViewModel
            {
                TotalMembers = members.Count,
                ActiveMembers = members.Count(m => m.Memberships.Any(x => x.EndDate > DateTime.Now)),
                TrainersCount = trainers.Count,

                UpcomingSessions = sessions.Count(s => s.Status == "Upcoming"),
                OngoingSessions = sessions.Count(s => s.Status == "Ongoing"),
                CompletedSessions = sessions.Count(s => s.Status == "Finished")
            };

            return View(model);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
