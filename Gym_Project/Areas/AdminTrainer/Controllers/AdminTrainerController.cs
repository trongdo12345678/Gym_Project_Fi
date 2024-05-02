using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.AdminTrainer.Controllers;
[Area("AdminTrainer")]
public class AdminTrainerController : Controller
{
    private IAccountService _accountService;
    public AdminTrainerController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("LoggedInUser") != null)
        {

            return RedirectToAction("ShowListAttendance", "AttendanceTrainer");
        }
        else
        {

            return RedirectToAction("LoginAdminTrainer");
        }
    }
    //[Route("~/")]
    public IActionResult LoginAdminTrainer()
    {
        string? errorMessage = TempData["ErrorMessage"] as string;
        ViewBag.ErrorMessage = errorMessage;
        return View();
    }
    [HttpPost]
    public IActionResult LoginTrainer(string username, string password)
    {
        bool isLoggedIn = _accountService.LoginTrainer(username, password);
        if (isLoggedIn)
        {
            HttpContext.Session.SetString("LoggedInUser", username);
            return RedirectToAction("ShowListAttendance", "AttendanceTrainer");
        }
        else
        {
            TempData["ErrorMessage"] = "Account or password is incorrect.";
            return RedirectToAction("LoginAdminTrainer");
        }
    }
    public IActionResult Logout()
    {
        return RedirectToAction("LoginAdminTrainer");
    }
}
