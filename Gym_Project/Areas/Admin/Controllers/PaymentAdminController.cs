using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gym_Project.Areas.Admin.Controllers;
[Area("Admin")]
public class PaymentAdminController : Controller
{
    private IPaymentService _payService;
    public PaymentAdminController(IPaymentService payService)
    {
        _payService = payService;
    }
    [Route("/Admin/PaymentAdmin/ShowListPay/{page}")]
    [Route("/Admin/PaymentAdmin/ShowListPay")]
    public IActionResult ShowListPay(int page = 1)
    {
        var (totalPage, currentPage) = _payService.GetPaginationInfo(10, page);
        ViewBag.Pay = _payService.GetlistPbyPages(page, 10);
        ViewBag.TotalPage = totalPage;
        ViewBag.CurrentPage = currentPage;
        return View();
    }
    public IActionResult AddPay()
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
        ViewBag.Pack = _payService.GetPack();
        return View();
    }
    [HttpPost]
    public ActionResult AddPayadmin(Payment dona)
    {
        if (!ModelState.IsValid)
        {
            // Kiểm tra từng trường input và chỉ cảnh báo khi trường đó null
            if (dona.Amount == 0)
                ModelState.AddModelError("Amount", "Please enter a valid amount.");

            // Thực hiện kiểm tra các trường khác nếu cần

            TempData["Mess"] = "Please do not leave blank boxes";
            return RedirectToAction("AddPay");
        }
        else
        {
            dona.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
            _payService.AddPay(dona);
            return RedirectToAction("ShowListPay");
        }
    }
}
