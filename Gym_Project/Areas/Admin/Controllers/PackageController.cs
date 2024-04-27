using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class PackageController : Controller
{
    private IPackageService _packageService;
    public PackageController(IPackageService packageService)
    {
        _packageService = packageService;
    }
    //[Route("~/")]
    [Route("/Admin/Package/ShowListPack/{page}")]
	[Route("/Admin/Package/ShowListPack")]
    public IActionResult ShowListPack(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _packageService.GetPaginationInfo(10, page, searchtext);
        ViewBag.Package = _packageService.GetlistPbyPages(page, 10, searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
    public IActionResult ShowAddPack()
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
        ViewBag.Trai = _packageService.GetTrai();
        return View();
    }
    [HttpPost]
    public IActionResult AddPack(Package package)
    {
		if (!ModelState.IsValid)
		{
			if (string.IsNullOrEmpty(package.PackageName))
				ModelState.AddModelError("Name", "Please enter a name.");

			if (package.Img == null)
				ModelState.AddModelError("Images", "Please enter a Images.");

			if (string.IsNullOrEmpty(package.Description))
				ModelState.AddModelError("Description", "Please enter a Description.");

			if (package.Discount == null)
				ModelState.AddModelError("Discount", "Please enter a Discount.");

			if (package.TrainerId == null)
				ModelState.AddModelError("trainer", "Please enter a trainer.");

			if (package.Cost == null)
				ModelState.AddModelError("Cost", "Please enter a Cost.");


			TempData["Mess"] = "Please do not leave blank boxes";
			return RedirectToAction("ShowAddPack");
		}
		else
		{

			_packageService.AddPack(package);
			return RedirectToAction("ShowListPack");
		}
		
    }
    public IActionResult DropPack(int id)
    {
        try
        {
            _packageService.DropPack(id);
            return RedirectToAction("ShowListPack");
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An error occurred while deleting data." });
        }
        
    }

    public IActionResult ShowUpdatePack(int id)
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
		ViewBag.Trai = _packageService.GetTrai();
		var pack = _packageService.GetPack(id);
        return View("ShowUpdatePack", pack);
    }
    [HttpPost]
    public IActionResult UpdatePack(Package package)
    {
        if (!ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(package.PackageName))
                ModelState.AddModelError("Name", "Please enter a name.");

            if (package.Img == null)
                ModelState.AddModelError("Images", "Please enter a Images.");

            if (string.IsNullOrEmpty(package.Description))
                ModelState.AddModelError("Description", "Please enter a Description.");

            if (package.Discount == null)
                ModelState.AddModelError("Discount", "Please enter a Discount.");

            if (package.TrainerId == null)
                ModelState.AddModelError("trainer", "Please enter a trainer.");

            if (package.Cost == null)
                ModelState.AddModelError("Cost", "Please enter a Cost.");


            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowUpdatePack");
        }
        else
        {
            _packageService.UpdatePack(package);
            return RedirectToAction("ShowListPack");
        }
    }
}
