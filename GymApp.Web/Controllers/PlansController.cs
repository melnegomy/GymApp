using GymApp.BLL.Contracts;
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

    }
}
