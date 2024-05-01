using Gym_Project.IService;
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
    public IActionResult ShowListAttendance()
    {
        return View();
    }
}
