using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels;
using Gym.BusinessLogic.ViewModels.Plan;
using Gym.Data.contexts;
using Gym.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gym.Controllers
{
    [Authorize]
    public class PlanController : Controller
    {
        private readonly IPlanService service;
        public PlanController(IPlanService _service)
        {
            service = _service;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationtoken)
        {
            var viewmodel = await service.GetAllPlansAsync(cancellationtoken);
            return View(viewmodel);
        }

        public async Task<IActionResult> Details(int id,CancellationToken cancellationToken)
        {
            var vm = await service.GetPlanDetailsAsync(id, cancellationToken);

            if (vm == null)
                return NotFound();

            return View(vm);
        }

        public async Task<IActionResult> ActivationControl(int id,CancellationToken cancellationToken) {
            var plan = await service.ActivationControl(id,cancellationToken);
            if (!plan.IsSuccess)
            {
                TempData["Error"] = plan.Error;

            }
            
             return RedirectToAction(nameof(Index));
           
        }

        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var data = await service.GetPlanForEdit(id, cancellationToken);

            if (data == null)
                return NotFound();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlanEditViewModel vm, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return View(vm);

            var result = await service.Edit(id, vm, cancellationToken);

            if (!result.IsSuccess)
            {

                
                return View(vm);
            }

            TempData["Success"] = "Plan updated successfully";
            
            return RedirectToAction(nameof(Index));
        }
    }
}
