using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.AdminTrainer.Controllers;
[Area("AdminTrainer")]
public class FeedBackTrainerController : Controller
{
    private IFeedBackService _freeService;
    public FeedBackTrainerController(IFeedBackService freeService)
    {
        _freeService = freeService;
    }
    [Route("/AdminTrainer/FeedBackTrainer/ShowFeedBack/{page}")]
    [Route("/AdminTrainer/FeedBackTrainer/ShowFeedBack")]
    public IActionResult ShowFeedBack(int page = 1)
    {
        var (totalPage, currentPage) = _freeService.GetPaginationInfo(50, page);
        ViewBag.Feed = _freeService.GetlistPbyPages(page, 50);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
}
