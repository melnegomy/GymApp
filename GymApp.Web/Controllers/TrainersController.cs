using GymApp.BLL.Contracts;
using GymApp.BLL.ViewModels.Trainer;
using GymApp.DAL.Context;
using GymApp.DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Controllers;

public class TrainersController : Controller
{
    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        ViewBag.Title = "Trainers";
        var trainers = await _trainerService.GetTrainersAsync(cancellationToken);
        return View(trainers);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(TrainerCreateViewModel trainerViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(trainerViewModel);
        var result = await _trainerService.CreateTrainerAsync(trainerViewModel, cancellationToken);
        if (result)
            TempData["SuccessMessage"] = "Trainer created successfully!";
        else
            TempData["ErrorMessage"] = "Failed to create trainer.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var trainerDetails = await _trainerService.GetTrainerDetailsAsync(id, cancellationToken);
        if (trainerDetails is not null)
            return View(trainerDetails);
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var trainerDetails = await _trainerService.GetTrainerDetailsForEditAsync(id, cancellationToken);
        if (trainerDetails is not null)
            return View(trainerDetails);
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, TrainerEditViewModel trainerEditViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(trainerEditViewModel);
        var result = await _trainerService.UpdateTrainerAsync(id, trainerEditViewModel, cancellationToken);
        if (result)
            TempData["SuccessMessage"] = "Trainer updated successfully!";
        else
            TempData["ErrorMessage"] = "Failed to update trainer.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var trainer = await _trainerService.GetTrainerDetailsAsync(id, cancellationToken);
        if (trainer is null)
            return NotFound();
        return View(trainer);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteConfirmed(int id, CancellationToken cancellationToken)
    {
        var result = await _trainerService.DeleteTrainerAsync(id, cancellationToken);
        if (result)
            TempData["SuccessMessage"] = "Trainer deleted successfully!";
        else
            TempData["ErrorMessage"] = "Failed to delete trainer.";
        return RedirectToAction(nameof(Index));
    }
}
