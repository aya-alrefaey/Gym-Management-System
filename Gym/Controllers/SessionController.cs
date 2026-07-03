using AutoMapper;
using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Session;
using Gym.BusinessLogic.ViewModels.Trainer;
using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gym.Presentation.Controllers
{
    [Authorize]
    public class SessionController : Controller
    {
        private readonly ISessionService service;
        private readonly IService<Category> categoryService;
        private readonly IService<Trainer> trainerService;

        public SessionController(ISessionService _service, IService<Category> _categoryService, IService<Trainer> _trainerService)
        {
            service = _service;
            categoryService = _categoryService;
            trainerService = _trainerService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken=default)
        {
            var model = await service.GetAllSessions(cancellationToken);
            return View(model);

        }
    
        public async Task<IActionResult> Create(CancellationToken cancellationToken = default)
        {
            await LoadDropDowns(cancellationToken);
            return View();

        }
        private async Task LoadDropDowns(CancellationToken cancellationToken)
        {
            var categories = await categoryService.GetAllAsync(cancellationToken)
                              ?? new List<Category>();

            var trainers = await trainerService.GetAllAsync(cancellationToken)
                           ?? new List<Trainer>();
            if (!categories.Any() && !trainers.Any())
            {
                TempData["ErrorMessage"] = "No categories or trainers found.";
            }
            else if (!categories.Any())
            {
                TempData["ErrorMessage"] = "No categories found. Please add categories first.";
            }
            else if (!trainers.Any())
            {
                TempData["ErrorMessage"] = "No trainers found. Please add trainers first.";
            }

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SessionCreateViewModel vm, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropDowns(cancellationToken);
                return View(vm);
            }
            var data = await service.CreateSessionAsync(vm, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Error;
                await LoadDropDowns(cancellationToken);
                return View(vm);
            }

            TempData["SuccessMessage"] = "Session created successfully";

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            var data = await service.GetSessionByIdAsync(id, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Error;
                return RedirectToAction(nameof(Index));
            }
            return View(data.Data);
        }
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            var data = await service.GetDataForEditAsync(id, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Error;
                return RedirectToAction(nameof(Index));
            }

            await LoadTrainerDropDown(cancellationToken);

           

            return View(data.Data);

        }

        private async Task LoadTrainerDropDown(CancellationToken cancellationToken)
        {
           

            var trainers = await trainerService.GetAllAsync(cancellationToken)
                           ?? new List<Trainer>();
            if (!trainers.Any())
            {
                TempData["ErrorMessage"] = "No trainers found.";
            }
            
           

           
            ViewBag.Trainers = new SelectList(trainers, "Id", "Name");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SessionEditViewModel vm, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                await LoadTrainerDropDown(cancellationToken);
                return View(vm);
            }
              
            var result = await service.EditSession(vm, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["ErrorMessage"] = result.Error;
                await LoadTrainerDropDown(cancellationToken);
                return View(vm);
            }
            TempData["SuccessMessage"] = "Session Updated Successfully";
            return RedirectToAction(nameof(Index));

        }

        
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken cancellationToken = default)
        {
            var data = await service.DeleteSession(id, cancellationToken);
            if(!data.IsSuccess)
            {
                TempData["ErrorMessage"] = data.Error;
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Session Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

