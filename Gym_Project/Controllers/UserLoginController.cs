using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Controllers;
public class UserLoginController : Controller
{
    private IAccountService _accountService;
    public UserLoginController(IAccountService accountService)
    {
        _accountService = accountService;
    }
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("LoggedInUser") != null)
        {


            return RedirectToAction("Index", "UserLayout");
        }
        else
        {
            return RedirectToAction("LoginUser");
        }
    }

    [HttpPost]
    public IActionResult LoginUser(string username, string password)
    {
        bool isLoggedIn = _accountService.LoginUser(username, password);
        if (isLoggedIn)
        {
            HttpContext.Session.SetString("LoggedInUser", username);
            return RedirectToAction("Index");
        }
        else
        {
            TempData["ErrorMessage"] = "Account or password is incorrect.";
            return RedirectToAction("LoginUserWeb");
        }
    }
    public IActionResult ShowRegister()
    {
        return View();
    }
	[HttpPost]
    public IActionResult Register(Member member, string confirmPassword)
    {
        if (member.Password != confirmPassword)
        {
            TempData["ErrorMessage"] = "Confirm password does not match the password.";
            TempData["RedirectToRegister"] = true;
            return RedirectToAction("LoginUserWeb");
        }

        bool isRegistered = _accountService.Register(member);
        if (isRegistered)
        {
            return RedirectToAction("LoginUserWeb");
        }
        else
        {
            TempData["UsernameErrorMessage"] = "Username already exists. Please choose another username.";
            TempData["RedirectToRegister"] = true;
            return RedirectToAction("LoginUserWeb");
        }
    }

    //[Route("~/")]
    public IActionResult LoginUserWeb()
    {
        ViewBag.ErrorMessage = TempData["ErrorMessage"] as string ?? "";
        ViewBag.UsernameErrorMessage = TempData["UsernameErrorMessage"] as string ?? "";

        bool redirectToRegister = TempData["RedirectToRegister"] != null ? (bool)TempData["RedirectToRegister"] : false;
        if (redirectToRegister)
        {
            return View("LoginUserWeb");
        }

        return View();
    }

    [HttpPost]
    public IActionResult SaveInfo(string name, string address)
    {
        try
        {
            string loggedInUser = HttpContext.Session.GetString("LoggedInUser");

            if (string.IsNullOrEmpty(loggedInUser))
            {
                TempData["ErrorMessage"] = "User session not found.";
                return RedirectToAction("Error");
            }

            var loggedInUserId = _accountService.GetMemberIdByUsername(loggedInUser);

            if (loggedInUserId != null)
            {

                bool isSaved = _accountService.SaveUserInfo(loggedInUserId.Value, name, address);


                if (isSaved)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to save user information.";
                    return RedirectToAction("Error");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "User not found.";
                return RedirectToAction("Error");
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = "An error occurred while processing the request: " + ex.Message;
            return RedirectToAction("Error");
        }
    }
    [HttpPost] // hàm Logout
    public IActionResult LogOut()
    {
        HttpContext.Session.SetString("LoggedInUser", "");
        return RedirectToAction("index", "Layoutmain");
    }
}
