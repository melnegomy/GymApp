using GymApp.BLL.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Web.Controllers;

public class SessionsController : Controller
{
    private readonly ISessionService _sessionService;

    public SessionsController(ISessionService sessionService)
    {
        _sessionService = sessionService;
    }

    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var sessions = await _sessionService.GetAllSessionsAsync(cancellationToken);
        return View(sessions);
    }
}
