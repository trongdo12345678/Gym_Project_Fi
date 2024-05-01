using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class FeedBackAdminController : Controller
{
    private IFeedBackService _freeService;
    public FeedBackAdminController(IFeedBackService freeService)
    {
        _freeService = freeService;
    }
    public IActionResult ShowFeedBack()
    {
        return View();
    }
}
