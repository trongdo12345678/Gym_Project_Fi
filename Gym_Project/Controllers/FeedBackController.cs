using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Controllers;
public class FeedBackController : Controller
{
    private IFeedBackService _freeService;
    public FeedBackController(IFeedBackService freeService)
    {
        _freeService = freeService;
    }
    public IActionResult Index()
    {
        return View();
    }
}
