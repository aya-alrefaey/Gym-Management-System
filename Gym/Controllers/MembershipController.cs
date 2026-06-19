using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Membership;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Controllers
{
    public class MembershipController : Controller
    {
        private readonly IMembershipService service;
        private readonly IPlanService planService;
        private readonly IMemberService memberService;
        public MembershipController(IMembershipService _service, IPlanService _planService, IMemberService _memberService)
        {
            service = _service;
            planService = _planService;
            memberService = _memberService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var data = await service.GetAllMembershipsAsync(cancellationToken);
            return View(data);
        }
        public async Task<IActionResult> Create (CancellationToken cancellationToken = default)
        {
            await LoadDropDowns(cancellationToken);
            return View();
        }
        private async Task LoadDropDowns(CancellationToken cancellationToken)
        {
            var members = await memberService.GetAllAsync(cancellationToken)
                              ?? new List<Member>();

            var plans = await planService.GetAllAsync(cancellationToken)
                           ?? new List<Plan>();
            if (!members.Any() && !plans.Any())
            {
                TempData["ErrorMessage"] = "No members or plans found.";
            }
            else if (!members.Any())
            {
                TempData["ErrorMessage"] = "No members found. Please add members first.";
            }
            else if (!plans.Any())
            {
                TempData["ErrorMessage"] = "No plans found. Please add plans first.";
            }

            ViewBag.Members = new SelectList(members, "Id", "Name");
            ViewBag.Plans = new SelectList(plans, "Id", "Name");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MembershipCreateViewModel vm, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns(cancellationToken);
                return View(vm);
            }
            var data = await service.CreateMembershipAsync(vm, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Error;
                await LoadDropDowns(cancellationToken);
                return View(vm);
            }

            TempData["SuccessMessage"] = "Membership created successfully";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken = default) {
            var result = await service.CancelMembershipAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Membership canceled successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
