using GymApp.BLL.Contracts;
using GymApp.BLL.Services;
using GymApp.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace GymApp.Web.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlansService _services;
        public PlansController(IPlansService service)
        {
            _services = service;
        }


        // Get : baseURL/Plans/Index 
        [HttpGet]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var plans = await _services.PlansAsync(cancellationToken);
            // get all plans from database and pass to view
            return View(plans);
        }
        // Details : baseURL/Plans/Index/id
        [HttpGet]
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken = default)
        {
            //var details = _dbContext.Plans.FirstOrDefault(p => p.Id == id);
            var plan = await _services.GetByIdAsync(id, cancellationToken);
            if (plan == null)
                return RedirectToAction(nameof(Index));
            return View(plan);
        }

        // Edit : baseURL/Plans/Edit/id
        [HttpGet]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken = default)
        {
            var plan = await _services.GetByIdAsync(id, cancellationToken);
            if (plan == null)
                return RedirectToAction(nameof(Index));
            return View(plan);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Plan plan, CancellationToken cancellationToken = default)
        {
            if (id != plan.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _services.UpdateAsync(plan, cancellationToken);
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        // IsActive : baseURL/Plans/IsActive/id
        [HttpPost]  
        public async Task<IActionResult> IsActive(int id , CancellationToken cancellationToken)
        {
            var result = await _services.IsActiveAsync(id, cancellationToken);

            if (result)
                TempData["SuccessMessage"] = "Plan status changed successfully!";
            else
                TempData["ErrorMessage"] = "Failed to change plan status.";

            return RedirectToAction(nameof(Index));
        }

    }
}
