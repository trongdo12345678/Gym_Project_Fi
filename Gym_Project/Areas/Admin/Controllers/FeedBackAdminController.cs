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
    [Route("/Admin/FeedBackAdmin/ShowFeedBack/{page}")]
    [Route("/Admin/FeedBackAdmin/ShowFeedBack")]
    public IActionResult ShowFeedBack( int page = 1)
    {
        var (totalPage, currentPage) = _freeService.GetPaginationInfo(50, page);
        ViewBag.Feed = _freeService.GetlistPbyPages(page, 50);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
    public IActionResult DropFeed(int id)
    {
        try
        {
            _freeService.DropFeed(id);
            return RedirectToAction("ShowFeedBack");
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An error occurred while deleting data." });
        }
    }


}
