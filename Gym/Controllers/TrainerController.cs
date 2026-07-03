using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Trainer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gym.Presentation.Controllers
{
    [Authorize]
    public class TrainerController : Controller
    {
        private readonly ITrainerService service;
        public TrainerController(ITrainerService _service)
        {
            service = _service;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken = default)
        {
            var data = await service.GetAllTrainers(cancellationToken);
           
            return View(data);
        }

        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            var data = await service.GetTrainerDetails(id, cancellationToken);
            if (data is null) return NotFound();

            return View(data);
        }
        public async Task<IActionResult> Create ( CancellationToken cancellationToken = default)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TrainerCreateViewModel vm,CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await service.CreateTrainerAsync(vm, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["Error"] = data.Error;
                return View(vm);
            }

            TempData["SuccessMessage"] = "Trainer created successfully";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken cancellationToken)
        {
            var result = await service.DeleteTrainer(id, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Error);
                return View();
            }
            TempData["Success"] = "Trainer deleted successfully";
            return RedirectToAction(nameof(Index));


        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var data = await service.GetByIdAsync(id, cancellationToken);
            if (data == null)
                return NotFound();
            var viewmodel = new EditTrainerViewModel
            {
                Id = data.Id,
                Name = data.Name,
                Email = data.Email,
                Phone = data.Phone,
                Specialties=data.Specialties,
                BuildingNo = data.Address.BuildingNo,
                City = data.Address.City,
                Street = data.Address.Street
            };
            return View(viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditTrainerViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(vm);
            var result = await service.EditTrainer(vm, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Error;
                return View(vm);
            }
            TempData["Success"] = "Trainer Updated Successfully";
            return RedirectToAction(nameof(Index));


        }
    }
}
