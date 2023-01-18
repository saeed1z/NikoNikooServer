using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using OnlineServices.Api.Helpers;
using System;
using OnlineServices.Entity;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using System.Security.Policy;
using OnlineServices.Core.Nikoo;

namespace OnlineServices.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NikooShopApiController : ControllerBase
    {
        private SettingMethod _settingMethod;
        private IConfiguration _configuration;
        private INikooPayment _nikooPayment;
        public NikooShopApiController(SettingMethod settingMethod, IConfiguration configuration, INikooPayment nikooPayment)
        {
            _settingMethod = settingMethod;
            _configuration = configuration;
            _nikooPayment = nikooPayment;
        }

        [Route("SubmitShopCart")]
        [HttpPost]
        public async Task<ActionResult> SubmitShopCart(NikooPayment nikooPayment)
        {
            try
            {
                string Key = "nikoopayment";
                var token = HttpContext.Request.Headers["Token"].ToString().ToLower();
                if (token != Key)
                {
                    return NotFound();
                }
                if (nikooPayment == null)
                {
                    return NotFound();
                }
                if (await _nikooPayment.UserHasCourse(nikooPayment) == true)
                {
                    return Ok("کاربر این دوره را قبلا خریداری کرده است.");
                }
                var lastPay = await _nikooPayment.GetAllUserOpnePayment(nikooPayment.UserMobile);
                if (lastPay.Count > 0)
                {
                    await _nikooPayment.DeleteAllUserOpnePayment(lastPay);
                }
                var paymentId = await _nikooPayment.Add(nikooPayment);

                var domain = _configuration["Url:Domain"];
                var address = domain + "Payment/Verify?paymentId=" + paymentId;
                var url = "";
                //if (_settingMethod.IsLocal())
                //{
                //    url = await Local((int)nikooPayment.Price, address, nikooPayment.UserMobile);
                //}
                //else
                //{
                //    url = await ZarinPall((int)nikooPayment.Price, address, nikooPayment.UserMobile);
                //}
                url = await ZarinPall((int)nikooPayment.Price, address, nikooPayment.UserMobile);
                return Ok(url);

            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        [Route("GetUserpay")]
        [HttpGet]
        public async Task<ActionResult> GetUserpay(string mobileNumber)
        {
            try
            {
                if (mobileNumber == null) return NotFound();
                string Key = "getuserpay";
                var token = HttpContext.Request.Headers["Token"].ToString().ToLower();
                if (token != Key)
                {
                    return NotFound();
                }
                var res = await _nikooPayment.GetAllUserClosePayment(mobileNumber);
                return Ok(res);
            }
            catch (Exception e)
            {
                return NotFound();
            }
        }

        private async Task<string> Local(int sum, string address, string mobile)
        {
            var payment = new ZarinpalSandbox.Payment(sum);
            var res = payment.PaymentRequest("خرید دوره اموزشی", address, mobile: mobile);
            if (res.Result.Status == 100)
            {
                return "https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority;
            }
            return "";
        }
        private async Task<string> ZarinPall(int sum, string address, string mobile)
        {
            var payment = new Zarinpal.Payment(_configuration["PrivateKey:Merchant"], sum);
            var res = payment.PaymentRequest("خرید دوره اموزشی", address, mobile: mobile);
            if (res.Result.Status == 100)
            {
                return "https://zarinpal.com/pg/StartPay/" + res.Result.Authority;
            }
            return "";
        }
    }
}
