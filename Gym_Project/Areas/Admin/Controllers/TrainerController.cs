using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class TrainerController : Controller
{
    private ITrainerService _trainerService;
    public TrainerController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }
   
    [Route("/Admin/Trainer/ShowListTrai/{page}")]
    [Route("/Admin/Trainer/ShowListTrai")]
    public IActionResult ShowListTrai(string searchtext, int page = 1)
    {
        var (totalPage, currentPage) = _trainerService.GetPaginationInfo( 5 , page, searchtext);
        ViewBag.Trainer = _trainerService.GetlistPbyPages(page, 5 , searchtext);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
	public IActionResult ShowAddTrai()
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
	public IActionResult AddTrainer(Trainer trai)
	{
		if (!ModelState.IsValid)
		{
			// Kiểm tra từng trường input và chỉ cảnh báo khi trường đó null
			if (string.IsNullOrEmpty(trai.TrainerName))
				ModelState.AddModelError("Name", "Please enter a name.");

			if (trai.Img == null)
				ModelState.AddModelError("Images", "Please enter a Images.");

			if (string.IsNullOrEmpty(trai.Username))
				ModelState.AddModelError("Username", "Please enter a Username.");

			if (string.IsNullOrEmpty(trai.Password))
				ModelState.AddModelError("Password", "Please enter a Password.");

			if (trai.RoleId == null)
				ModelState.AddModelError("Role", "Please enter a Role.");

			if (string.IsNullOrEmpty(trai.Specialization))
				ModelState.AddModelError("Specialization", "Please enter a Specialization.");

			// Thực hiện kiểm tra các trường khác nếu cần

			TempData["Mess"] = "Please do not leave blank boxes";
			return RedirectToAction("ShowAddTrai");
		}
		else
		{

			_trainerService.AddTrainer(trai);
		return RedirectToAction("ShowListtrai");
		} 
	}
	public IActionResult DropTrainer(int id)
	{
	try
		{
			_trainerService.DropTrainer(id);
			return RedirectToAction("ShowListtrai");
		}catch (Exception)
		{
			return Json(new { success = false, message = "An error occurred while deleting data." });
		}
		
	}
	public IActionResult ShowUpdateTrainer(int id)
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
		var trai = _trainerService.GetTrai(id);
		return View("ShowUpdateTrainer", trai);
	}
	[HttpPost]
	public IActionResult UpdateTrai(Trainer trai)
	{
        if (!ModelState.IsValid)
        {
            // Kiểm tra từng trường input và chỉ cảnh báo khi trường đó null
            if (string.IsNullOrEmpty(trai.TrainerName))
                ModelState.AddModelError("Name", "Please enter a name.");

            if (trai.Img == null)
                ModelState.AddModelError("Images", "Please enter a Images.");

            if (string.IsNullOrEmpty(trai.Username))
                ModelState.AddModelError("Username", "Please enter a Username.");

            if (string.IsNullOrEmpty(trai.Password))
                ModelState.AddModelError("Password", "Please enter a Password.");

            if (trai.RoleId == null)
                ModelState.AddModelError("Role", "Please enter a Role.");

            if (string.IsNullOrEmpty(trai.Specialization))
                ModelState.AddModelError("Specialization", "Please enter a Specialization.");

            // Thực hiện kiểm tra các trường khác nếu cần

            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowUpdateTrainer");
        }
        else
        {
            _trainerService.UpdateTrainer(trai);
            return RedirectToAction("ShowListtrai");
        }
    }
}
