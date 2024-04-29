using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Controllers;
public class UserLayoutController : Controller
{
	private IPackageService _packageService;
	private IMemberService _memberService;
	public UserLayoutController(IPackageService packageService, IMemberService memberService)
	{
		_packageService = packageService;
		_memberService = memberService;
	}
	[Route("/UserLayout/Index/{page}")]
	[Route("/UserLayout/Index")]
	public IActionResult Index(string searchtext, int page = 1)
    {
		var (totalPage, currentPage) = _packageService.GetPaginationInfo(6, page, searchtext);
		ViewBag.Pack = _packageService.GetlistPbyPages(page, 6, searchtext);
		ViewBag.TotalPage = totalPage;
		ViewBag.CurrentPage = currentPage;
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
	public IActionResult Classes()
	{
		return View();
	}
	public IActionResult Blog()
	{
		return View();
	}
	public IActionResult FeedBack()
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
