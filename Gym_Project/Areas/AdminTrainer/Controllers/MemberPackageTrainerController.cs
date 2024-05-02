using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.AdminTrainer.Controllers;
[Area("AdminTrainer")]
public class MemberPackageTrainerController : Controller
{
    private IMemberPackageService _memberpackageService;
    public MemberPackageTrainerController(IMemberPackageService memberpackageService)
    {
        _memberpackageService = memberpackageService;
    }
    [Route("/AdminTrainer/MemberPackageTrainer/ShowListMemPack/{page}")]
    [Route("/AdminTrainer/MemberPackageTrainer/ShowListMemPack")]
    public IActionResult ShowListMemPack(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _memberpackageService.GetPaginationInfo(15, page, searchtext);
        ViewBag.PackMemAll = _memberpackageService.GetlistPbyPages(page, 15, searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
}
