using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.AdminTrainer.Controllers;
[Area("AdminTrainer")]
public class AttendanceTrainerController : Controller
{
    private IAttendanceService _attendanceService;
    public AttendanceTrainerController(IAttendanceService attendanceService)
    {
        _attendanceService = attendanceService;
    }
    [Route("/AdminTrainer/AttendanceTrainer/ShowListAttendance/{page}")]
    [Route("/AdminTrainer/AttendanceTrainer/ShowListAttendance")]
    public IActionResult ShowListAttendance(int page = 1)
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
	public IActionResult Success()
	{
		return View();
	}
}
