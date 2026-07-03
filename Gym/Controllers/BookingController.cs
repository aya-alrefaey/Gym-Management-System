using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Booking;
using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Controllers
{
    [Authorize]
    public class BookingController:Controller
    {
        private readonly IBookingService service;
        private readonly IMemberService memberService   ;
        private readonly ISessionService sessionService;
        public BookingController(IBookingService _service, IMemberService _memberService, ISessionService _sessionService)
        {
            service = _service;
            memberService = _memberService;
            sessionService = _sessionService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {

            var model = await sessionService.GetAllSessions(cancellationToken);
            return View(model);
        }
        public async Task<IActionResult> UpComingSessions(int id,CancellationToken cancellationToken = default)
        {
            var bookings= await service.GetMembersForSessionAsync(id, cancellationToken);    

            return View(bookings);
        }
        
        public async Task<IActionResult> Create(int id, CancellationToken cancellationToken = default)
        {
            await LoadMembersDropDown(cancellationToken);
            return View();
        }
        private async Task LoadMembersDropDown(CancellationToken cancellationToken)
        {
            var members = await memberService.GetAllAsync(cancellationToken)
                           ?? new List<Member>();
            if (!members.Any())
            {
                TempData["ErrorMessage"] = "No members found.";
            }

            ViewBag.Members = new SelectList(members, "Id", "Name");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookingViewModel vm, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                await LoadMembersDropDown(cancellationToken);
                return View(vm);
            }
            var data = await service.CreateBookingAsync(vm, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Error;
                await LoadMembersDropDown(cancellationToken);
                return View(vm);
            }

            TempData["SuccessMessage"] = "Booking created successfully";

            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cancel(int id, CancellationToken cancellationToken = default)
        {
            var result = await service.CancelBookingAsync(id, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                var booking = await service.GetMembersForSessionAsync(id, cancellationToken);
                return RedirectToAction(nameof(UpComingSessions),booking);
            }
            TempData["SuccessMessage"] = "Booking canceled successfully";
            
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OnGoingSessions(int id, CancellationToken cancellationToken = default)
        {
            var bookings = await service.GetMembersForSessionAsync(id, cancellationToken);

            return View(bookings);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attendance(int id, CancellationToken cancellationToken = default)
        {
            var result = await service.MarkAsAttendedAsync(id, cancellationToken);

            if (!result.IsSuccess)
                TempData["ErrorMessage"] = result.Error;
            else
                TempData["SuccessMessage"] = "Marked as attended successfully";
            return RedirectToAction(nameof(Index));
        }

    }
}
