using Gym_Project.IService;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class AdminController : Controller
{
    private IAccountService _accountService;
    public AdminController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("LoggedInUser") != null)
        {

            return RedirectToAction("ShowListTrai", "Trainer");
        }
        else
        {

            return RedirectToAction("LoginAdmin");
        }
    }
   // [Route("~/")]
    public IActionResult LoginAdmin()
    {
        string? errorMessage = TempData["ErrorMessage"] as string;
        ViewBag.ErrorMessage = errorMessage;
        return View();
    }
    [HttpPost]
    public IActionResult Login(string username, string password)
    {
        bool isLoggedIn = _accountService.AdminLogin(username, password);
        if (isLoggedIn)
        {
            HttpContext.Session.SetString("LoggedInUser", username);
            return RedirectToAction("ShowListTrai","Trainer");
        }
        else
        {
            TempData["ErrorMessage"] = "Account or password is incorrect.";
            return RedirectToAction("LoginAdmin");
        }
    }
    public IActionResult Logout()
    {
        return RedirectToAction("LoginAdmin");
    }
}
