using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class ClassPackController : Controller
{
    private IClassPackService _classService;
    public ClassPackController(IClassPackService classService)
    {
        _classService = classService;
    }
    [Route("/Admin/ClassPack/ShowListClass/{page}")]
    [Route("/Admin/ClassPack/ShowListClass")]
    public IActionResult ShowListClass(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _classService.GetPaginationInfo(10, page, searchtext);
        ViewBag.Class = _classService.GetlistPbyPages(page, 10, searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
    public IActionResult ShowAddClass()
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
        return View();
    }
    [HttpPost]
    public IActionResult AddClass(ClassPack clas)
    {
        if (!ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(clas.ClassName))
                ModelState.AddModelError("ClassName", "Please enter a ClassName.");

            if (string.IsNullOrEmpty(clas.AssignmentTime))
                ModelState.AddModelError("AssignmentTime", "Please enter a AssignmentTime.");


            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowAddClass");
        }
        else
        {
            clas.DateClass = DateOnly.FromDateTime(DateTime.Now);
            _classService.AddClass(clas);
            return RedirectToAction("ShowListClass");
        }

    }
    public IActionResult DropClass(int id)
    {
        try
        {
            _classService.DropClass(id);
            return RedirectToAction("ShowListClass");
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An error occurred while deleting data." });
        }

    }

    public IActionResult ShowUpdateClass(int id)
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
        var clas = _classService.GetClass(id);
        return View("ShowUpdateClass", clas);
    }
    [HttpPost]
    public IActionResult UpdateClas(ClassPack clas)
    {
        if (!ModelState.IsValid)
        {
            if (string.IsNullOrEmpty(clas.ClassName))
                ModelState.AddModelError("ClassName", "Please enter a ClassName.");

            if (string.IsNullOrEmpty(clas.AssignmentTime))
                ModelState.AddModelError("AssignmentTime", "Please enter a AssignmentTime.");


            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowUpdateClass");
        }
        else
        {
            clas.DateClass = DateOnly.FromDateTime(DateTime.Now);
            _classService.UpdateClass(clas);
            return RedirectToAction("ShowListClass");
        }
    }
}
