using Gym.BusinessLogic.Helpers;
using Gym.BusinessLogic.Services;
using Gym.BusinessLogic.ViewModels.Member;
using Gym.enums;
using Gym.Models;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Gym.Presentation.Controllers
{
    public class MemberController:Controller
    {
        private readonly IMemberService service;
        private readonly IHealthRecordService recordservice;
        public MemberController(IMemberService _service, IHealthRecordService _recordservice)
        {
            service = _service;
            recordservice = _recordservice;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationtoken)
        {
            var data=await service.GetAllMembers(cancellationtoken);
            
            
            return View(data);
        }
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
           
            var model= await service.MemberDetails(id,cancellationToken);
            if(model==null) 
                return NotFound();
            
            return View(model);
        }
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberCreateViewModel vm,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var data = await service.CreateAsync(vm, cancellationToken);
            if (!data.IsSuccess)
            {
                TempData["Error"] = data.Error;
                return View(vm);
            }

            TempData["Success"] = "Member created successfully";

            return RedirectToAction(nameof(Index));
            
        }
        public async Task<IActionResult> HealthRecordDetails(int id,CancellationToken cancellationToken)
        {
            var data=await recordservice.GetHealthRecord(id,cancellationToken);
            if(data is null)
            {
                return NotFound();
            }
            return View(data);
        }

        public async Task<IActionResult> Delete(int id,CancellationToken cancellationToken)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmation(int id, CancellationToken cancellationToken)
        {
            var result = await service.DeleteMember(id, cancellationToken);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Error);
                return View();
            }
            TempData["Success"] = "Member deleted successfully";
            return RedirectToAction(nameof(Index));


        }
        public async Task<IActionResult> Edit(int id,CancellationToken cancellationToken)
        {
            var member = await service.GetMemberForEdit(id, cancellationToken);

            if (member == null)
                return NotFound();

            return View(member);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditMemberViewModel vm, CancellationToken cancellationToken)
        {
             if (!ModelState.IsValid)
        return View(vm);
            var result = await service.EditMember(vm, cancellationToken);
            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Error;
                return View(vm);
            }
            TempData["Success"] = "Member Updated Successfully";
            return RedirectToAction(nameof(Index));


        }

    }
}
