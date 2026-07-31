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

    public async Task <IActionResult> Index()
    {
        // logic to get all members from database and pass to view

        var members = await _memberService.GetAllMembersAsync();
        return View(members);
    }

    //create member
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateMemberViewModel memberViewModel)
    {
        if (!ModelState.IsValid)  
            return View(memberViewModel);

        var result = await _memberService.CreateMemberAsync(memberViewModel);
        if (result)
        {
            return RedirectToAction("Index");
        }

        ModelState.AddModelError(string.Empty, "Email or Phone is already taken.");
        return View(memberViewModel);

    }

}
