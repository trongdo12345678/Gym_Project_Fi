using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class MemberPackageController : Controller
{
    private IMemberPackageService _memberpackageService;
    public MemberPackageController(IMemberPackageService memberpackageService)
    {
        _memberpackageService = memberpackageService;
    }
    [Route("~/")]
    [Route("/Admin/MemberPackage/ShowlistUserSend/{page}")]
    [Route("/Admin/MemberPackage/ShowlistUserSend")]
    public IActionResult ShowlistUserSend(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _memberpackageService.GetPaginationInfo(5, page, searchtext);
        ViewBag.PackMem = _memberpackageService.GetlistPbyPages(page, 5, searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
	[Route("/Admin/MemberPackage/ShowListMemPack/{page}")]
	[Route("/Admin/MemberPackage/ShowListMemPack")]
	public IActionResult ShowListMemPack(string searchtext, int page = 1)
	{
		var (totalPage, currentPage) = _memberpackageService.GetPaginationInfo(15, page, searchtext);
		ViewBag.PackMemAll = _memberpackageService.GetlistPbyPages(page, 15, searchtext);
		ViewBag.TotalPage = totalPage;
		ViewBag.CurrentPage = currentPage;
		return View();
	}
	public IActionResult ShowConfirm(int id)
    {
        var mess = TempData["Mess"] as string;
        if (mess == "")
        {
            ViewBag.Mess = "";

        }
        else
        {
            ViewBag.Mess = mess;
        }
        var mempack = _memberpackageService.GetMemPack(id);
        ViewBag.Trai = _memberpackageService.GetTrai();
        ViewBag.Class = _memberpackageService.GetClass();
        return View("ShowConfirm", mempack);
    }
    [HttpPost]
    public IActionResult UpdatePack(MemberPackage mempack)
    {
        if (!ModelState.IsValid)
        {
            if (mempack.ClassId == null)
                ModelState.AddModelError("Class", "Please enter a Class.");

            if (mempack.TrainerId == null)
                ModelState.AddModelError("trainer", "Please enter a trainer.");

            if (mempack.EndDate == null)
                ModelState.AddModelError("EndDate", "Please enter a EndDate.");

            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowConfirm");
        }
        else
        {
            mempack.StartDate = DateOnly.FromDateTime(DateTime.Now);
			_memberpackageService.UpdateMemPack(mempack);
            return RedirectToAction("ShowListMemPack");
        }
    }
    public IActionResult ShowUpdateStatus(int id)
    {
        var mess = TempData["Mess"] as string;
        if (mess == "")
        {
            ViewBag.Mess = "";

        }
        else
        {
            ViewBag.Mess = mess;
        }
        var mempack = _memberpackageService.GetMemPack(id);
        return View("ShowUpdateStatus" , mempack);
    }
    [HttpPost]
    public IActionResult UpdateStatus(MemberPackage mempack)
    {
        if (!ModelState.IsValid)
        {
            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowUpdateStatus");
        }
        else
        {

            _memberpackageService.UpdateStatus(mempack);
            return RedirectToAction("ShowListMemPack");
        }
    }
}
