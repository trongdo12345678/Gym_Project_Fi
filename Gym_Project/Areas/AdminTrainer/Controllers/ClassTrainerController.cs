using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.AdminTrainer.Controllers;
[Area("AdminTrainer")]
public class ClassTrainerController : Controller
{

    private IClassPackService _classService;
    public ClassTrainerController(IClassPackService classService)
    {
        _classService = classService;
    }
    [Route("/AdminTrainer/ClassTrainer/ShowListClass/{page}")]
    [Route("/AdminTrainer/ClassTrainer/ShowListClass")]
    public IActionResult ShowListClass(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _classService.GetPaginationInfo(10, page, searchtext);
        ViewBag.Class = _classService.GetlistPbyPages(page, 10, searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
}
