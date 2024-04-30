using Gym_Project.IService;
using Gym_Project.Models;
using Microsoft.AspNetCore.Mvc;
using ProGCoder_MomoAPI.Models.Order;
using ProGCoder_MomoAPI.Services;

namespace Gym_Project.Controllers;
public class PaymentController : Controller
{
    private IMomoService _momoService;
    private IPaymentService _payService;
    public PaymentController(IMomoService momoService, IPaymentService payService)
    {
        _payService = payService;
        _momoService = momoService;
    }

    //[Route("~/")]
    public IActionResult Index(int id, int Idmen)
    {
        ViewBag.PackageId = id;

        ViewBag.MemberId = Idmen;

        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreatePaymentUrl(OrderInfoModel model, string packageid, string memberid)
    {
        TempData["projectiddonate"] = packageid;
        TempData["memberiddonate"] = memberid;
        var response = await _momoService.CreatePaymentAsync(model);
        return Redirect(response.PayUrl);
    }
    [HttpGet]
    public IActionResult PaymentCallBack(Payment dona)
    {

        var response = _momoService.PaymentExecuteAsync(HttpContext.Request.Query);
        dona.PaymentDate = DateOnly.FromDateTime(DateTime.Now);
        dona.PackageId = Convert.ToInt32(TempData["projectiddonate"]);
        dona.MemberId = Convert.ToInt32(TempData["memberiddonate"]);
        dona.PaymentMethod = "MOMO";
        dona.Amount = Convert.ToDecimal(response.Amount);
        _payService.AddPay(dona);

        return View(response);
    }
}
