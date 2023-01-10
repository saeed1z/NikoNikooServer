using Microsoft.AspNetCore.Mvc;
using OnlineServices.Api.Helpers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using OnlineServices.Core.Nikoo;

namespace OnlineServices.Api.Controllers
{
    public class PaymentController : Controller
    {
        private SettingMethod _settingMethod;
        private INikooPayment _nikooPayment;
        private IConfiguration _configuration;

        public PaymentController(SettingMethod settingMethod, INikooPayment nikooPayment, IConfiguration configuration)
        {
            _settingMethod = settingMethod;
            _nikooPayment = nikooPayment;
            _configuration = configuration;
        }
        [Route("Payment/Verify")]
        public async Task<IActionResult> Verify(int paymentId)
        {
            if (HttpContext.Request.Query["Status"] != "" &&
                HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                HttpContext.Request.Query["Authority"] != "")
            {
                string authority = HttpContext.Request.Query["Authority"].ToString();

                var cartPayment = await _nikooPayment.GetPaymentById(paymentId);
                if (cartPayment == null)
                {
                    ViewBag.error = "پرداخت با شکست مواجه شد لطفا دوباره اقدام کنید.";
                    return View();
                }

                int status = 0;
                string refId = "";
                if (_settingMethod.IsLocal())
                {
                    var payment = new ZarinpalSandbox.Payment((int)cartPayment.Price);
                    var res = payment.Verification(authority).Result;
                    status = res.Status;
                    refId = res.RefId.ToString();
                }
                else
                {
                    var res = await ZarinPall(authority, (int)cartPayment.Price);
                    status = res.Status;
                    refId = res.RefId.ToString();
                }
                if (status != 100) return NotFound();

                //var submitPay = await _cart.VerifyPay(paymentId, refId);
                if (status == 100)
                {
                    await _nikooPayment.FinishPayment(paymentId, refId);
                    ViewBag.success = "پرداخت شما با موفقیت انجام شد.";
                    ViewBag.Ref = refId;
                    return View();
                }

            }

            ViewBag.error = "پرداخت شما با خطا مواجه شد.";
            return View();
        }
        public async Task<Zarinpal.Models.PaymentVerificationResponse> ZarinPall(string authority, int sum)
        {
            var payment = new Zarinpal.Payment(_configuration["PrivateKey:Merchant"], sum);
            var res = payment.Verification(authority).Result;
            return res;
        }
    }
}
