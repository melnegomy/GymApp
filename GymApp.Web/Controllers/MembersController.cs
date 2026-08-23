using GymApp.BLL.Contracts;
using GymApp.BLL.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Controllers;

public class MembersController : Controller
{
    private readonly IMemberService _memberService;
    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {

        // logic to get all members from database and pass to view
        ViewBag.Message = "Members List";
        var members = await _memberService.GetAllMembersAsync(cancellationToken);
        return View(members);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(MemberCreateViewModel memberViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(memberViewModel);

        var result = await _memberService.CreateMemberAsync(memberViewModel, cancellationToken);
        if (result)
            TempData["SuccessMessage"] = "Member created successfully!";
        else
            TempData["ErrorMessage"] = "Failed to create member.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
    {
        var memberDetails = await _memberService.GetMemberDetailsAsync(id, cancellationToken);
        if (memberDetails is not null)
            return View(memberDetails);
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> HealthRecordDetails(int id, CancellationToken cancellationToken)
    {
        var healthRecordDetails = await _memberService.GetMemberHealthRecordDetailsAsync(id, cancellationToken);
        if (healthRecordDetails is not null)
            return View(healthRecordDetails);
        return NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var memberDetails = await _memberService.GetMemberDetailsForEditAsync(id, cancellationToken);
        if (memberDetails is not null)
            return View(memberDetails);
        return NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int id, MemberEditViewModel memberViewModel, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(memberViewModel);
        var result = await _memberService.UpdateMemberAsync(id, memberViewModel, cancellationToken);
        if (result)
            TempData["SuccessMessage"] = "Member updated successfully!";
        else
            TempData["ErrorMessage"] = "Failed to update member.";

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await _memberService.DeleteMemberAsync(id, cancellationToken);
        if (result)
            TempData["SuccessMessage"] = "Member deleted successfully!";
        else
            TempData["ErrorMessage"] = "Failed to delete member.";
        return RedirectToAction(nameof(Index));
    }

}