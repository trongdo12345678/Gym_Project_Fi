using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Gym_Project.Controllers;
public class MemberPackageController : Controller
{
	private IMemberPackageService _memberpackageService;
	public MemberPackageController(IMemberPackageService memberpackageService)
	{
		_memberpackageService = memberpackageService;
	}
	public IActionResult ShowViewMemPack(int id, int Idmen)
	{
		ViewBag.PackageId = id;

		ViewBag.MemberId = Idmen;
		return View();
	}
	[HttpPost]
    public IActionResult AddMemPack(MemberPackage packmem, string packageid, string memberid)
    {
		TempData["projectiddonate"] = packageid;
		TempData["memberiddonate"] = memberid;
		packmem.PackageId = Convert.ToInt32(TempData["projectiddonate"]);
		packmem.MemberId = Convert.ToInt32(TempData["memberiddonate"]);
		_memberpackageService.UserSendMemberPackage(packmem);
        return RedirectToAction("Success");
    }
	public IActionResult Success()
	{
		return View();	
	}
}
