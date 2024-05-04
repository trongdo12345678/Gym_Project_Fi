using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Controllers;
public class UserLayoutController : Controller
{
	private IPackageService _packageService;
	private IMemberService _memberService;
	private IFeedBackService _freeService;
    private IClassPackService _classService;
    public UserLayoutController(IPackageService packageService, IMemberService memberService, IFeedBackService freeService , IClassPackService classService)
	{
		_packageService = packageService;
		_memberService = memberService;
		_freeService = freeService;
        _classService = classService;

    }
	[Route("/UserLayout/Index/{page}")]
	[Route("/UserLayout/Index")]
	public IActionResult Index(string searchtext, int page = 1)
    {
		var (totalPage, currentPage) = _packageService.GetPaginationInfo(6, page, searchtext);
		ViewBag.Pack = _packageService.GetlistPbyPages(page, 6, searchtext);
		ViewBag.TotalPage = totalPage;
		ViewBag.CurrentPage = currentPage;
		ViewBag.Class = _classService.GetClasss();
		if (HttpContext.Session.GetString("LoggedInUser") == null)
		{
			ViewBag.checklogin = "";
		}
		else
		{
			string? usernamelogined = HttpContext.Session.GetString("LoggedInUser");
			ViewBag.checklogin = usernamelogined;
			if (usernamelogined != null)
			{
				ViewBag.memlogined = _memberService.getmemberbyusername(usernamelogined);
			}
		}
		return View();
    }
	public IActionResult About()
	{
		return View();
	}
	public IActionResult Trainer()
	{
		return View();
	}
	public IActionResult Contact()
	{
		return View();
	}
    [Route("/UserLayout/Classes/{page}")]
    [Route("/UserLayout/Classes")]
    public IActionResult Classes(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _classService.GetPaginationInfo(6, page, searchtext);
        ViewBag.Class = _classService.GetlistPbyPages(page, 6, searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
	public IActionResult Blog()
	{
		return View();
	}
	[Route("/UserLayout/ShowAddFeed/{page}")]
	[Route("/UserLayout/ShowAddFeed")]
	public IActionResult ShowAddFeed( int page = 1)
	{
		var (totalPage, currentPage) = _freeService.GetPaginationInfo(5, page);
		ViewBag.Feed = _freeService.GetlistPbyPages(page, 5);
		ViewBag.TotalPage = totalPage;
		ViewBag.CurrentPage = currentPage;
		ViewBag.Trai = _freeService.GetTrai();
		if (HttpContext.Session.GetString("LoggedInUser") == null)
		{
			ViewBag.checklogin = "";
		}
		else
		{
			string? usernamelogined = HttpContext.Session.GetString("LoggedInUser");
			ViewBag.checklogin = usernamelogined;
			if (usernamelogined != null)
			{
				ViewBag.memlogined = _memberService.getmemberbyusername(usernamelogined);
			}
		}
		return View();
	}
	public IActionResult AddFeed(Feedback feedback )
	{
		feedback.DateFeed = DateOnly.FromDateTime(DateTime.Now);
		_freeService.AddFeed(feedback);
		return RedirectToAction("Success");
	}
	public IActionResult Success()
	{
		return View();
	}
	public IActionResult Login()
	{
		return View();
	}
	public IActionResult Register()
	{
		return View();
	}
	public IActionResult DetailsService(int id)
	{

		string? usernamelogined = HttpContext.Session.GetString("LoggedInUser");

		if (usernamelogined != null)
		{
			ViewBag.checklogin = usernamelogined;
			ViewBag.memlogined = _memberService.getmemberbyusername(usernamelogined);
		}
		else
		{
			ViewBag.checklogin = "";
			ViewBag.memlogined = new Member() { };
		}
		ViewBag.Pack = _packageService.GetPack(id);
		return View();
	}
}
