using GymApp.BLL.Contracts;
using GymApp.BLL.Services;
using GymApp.BLL.ViewModels.Session;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymApp.Web.Controllers;

public class SessionsController : Controller
{
    private readonly ISessionService _sessionService;
    private readonly ITrainerService _trainerService;

    public SessionsController(ISessionService sessionService, ITrainerService trainerService)
    {
        _sessionService = sessionService;
        _trainerService = trainerService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sessions = await _sessionService.GetAllSessionsAsync(cancellationToken);
        return View(sessions);
    }

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await PopulateCreateViewDataAsync(cancellationToken);
        return View();
    }

    private async Task PopulateCreateViewDataAsync(CancellationToken cancellationToken)
    {
        var trainers = await _trainerService.GetTrainersAsync(cancellationToken);
        var categories = await _sessionService.GetCategorySelectItemAsync(cancellationToken);
        SelectList TrainerList = new SelectList(trainers, "Id", "Name");
        SelectList CategoryList = new SelectList(categories, "Id", "Name");
        ViewBag.TrainerList = TrainerList;
        ViewBag.CategoryList = CategoryList;
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateSessionViewModel session, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCreateViewDataAsync(cancellationToken);
            return View(session);
        }
        var result = await _sessionService.CreateSessionAsync(session, cancellationToken);
        if (result.IsSuccess)
        {
            TempData["SuccessMessage"] = "Session created successfully.";
            return RedirectToAction("Index");
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to create session.";
            await PopulateCreateViewDataAsync(cancellationToken);
            return View(session);
        }

    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var result = await _sessionService.GetSessionByIdAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error;
            return RedirectToAction("Index");
        }
        return View(result.Data);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var result = await _sessionService.GetSessionToUpdateAsync(id, cancellationToken);
        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Error;
            return RedirectToAction("Index");
        }
        await PopulateCreateViewDataAsync(cancellationToken);
        return View(result.Data);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id ,UpdateSessionViewModel model, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCreateViewDataAsync(cancellationToken);
            return View(model);
        }
        var result = await _sessionService.UpdateSessionAsync(id, model, cancellationToken);
        if (result.IsSuccess)
        {
            TempData["SuccessMessage"] = "Session updated successfully.";
            return RedirectToAction("Index");
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update session.";
            await PopulateCreateViewDataAsync(cancellationToken);
            return View(model);
        }
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _sessionService.DeleteSessionAsync(id, cancellationToken);
        if (result.IsSuccess)
            TempData["SuccessMessage"] = "Session deleted successfully!";
        else
            TempData["ErrorMessage"] = "Failed to delete session.";
        return RedirectToAction(nameof(Index));
    }
}
