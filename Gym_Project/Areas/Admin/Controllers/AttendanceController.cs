using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class AttendanceController : Controller
{
	private IAttendanceService _attendanceService;
	public AttendanceController(IAttendanceService attendanceService)
	{
		_attendanceService = attendanceService;
	}
    [Route("/Admin/Attendance/ShowListAtt/{page}")]
    [Route("/Admin/Attendance/ShowListAtt")]
    public IActionResult ShowListAtt(int page = 1)
    {

        var (totalPage, currentPage) = _attendanceService.GetPaginationInfo(10, page);
        ViewBag.Att = _attendanceService.GetlistPbyPages(page, 10);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
    public IActionResult ShowAttendance()
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
		ViewBag.Trai = _attendanceService.GetTrai();
		return View();
	}
    [HttpPost]
    public IActionResult AddAttendance(Attendance attendance)
    {
        if (!ModelState.IsValid)
        {
            if (attendance.Status == null)
                ModelState.AddModelError("Status", "Please enter a status.");

            if (attendance.TrainerId == null)
                ModelState.AddModelError("trainer", "Please enter a trainer.");

            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("ShowAttendance");
        }
        else
        {
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
            if (_attendanceService.IsAttendanceExistsForToday(currentDate, attendance.TrainerId))
            {
                TempData["Mess"] = "Attendance for today has already been added.";
                return RedirectToAction("ShowAttendance");
            }

            attendance.Date = currentDate;
            _attendanceService.AddAttendance(attendance);
            return RedirectToAction("Success");
        }
    }
    public IActionResult DropAtt(int id)
    {
        try
        {
            _attendanceService.DropAtt(id);
            return RedirectToAction("ShowListAtt");
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An error occurred while deleting data." });
        }

    }
    [HttpPost]
    public IActionResult DropAll(string ids)
    {
        bool success = false;
        if (!string.IsNullOrEmpty(ids))
        {
            var idArray = ids.Split(',');
            foreach (var id in idArray)
            {
                success = _attendanceService.DropAll(int.Parse(id));
                if (!success)
                {
                    break;
                }
            }
        }
        return Json(new { success });
    }

    public IActionResult Success()
	{
		return View();
	}
}
