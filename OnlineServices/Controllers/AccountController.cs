using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class AccountController : Controller
    {
        #region Fields
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPersonService _personService;
        #endregion Fields

        #region Ctor
        public AccountController(ILoggerFactory loggerFactory,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonService personService)
        {
            this._logger = loggerFactory.CreateLogger<AccountController>();
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._personService = personService;
        }
        #endregion Ctor

        #region Utilities
        #endregion Utilities

        #region Methods

        #region Login / logout

        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction(nameof(HomeController.Index), "Home");

            ViewData["returnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model, string returnUrl = null)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByNameAsync(model.MobileNumber);
            if (user.UserName != "admin" && user.UserName != "09121268578")
            {
                ModelState.AddModelError(string.Empty, "شماره اجازه دسترسی به پنل مدیریت را ندارید");
                return View(model);
            }
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);
                if (result.Succeeded)
                {
                    if (user.UserName != "admin")
                    {
                        var person = _personService.GetByMobileNo(model.MobileNumber);
                        if (person.PersonType.PanelAccess)
                        {
                            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);

                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "شما مجوز ورود به پنل را ندارید");
                            return View(model);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                            return Redirect(returnUrl);

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    //var identity = new ClaimsIdentity(IdentityConstants.ApplicationScheme);
                    //identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    //identity.AddClaim(new Claim(ClaimTypes.Name, user.PhoneNumber));

                    //await HttpContext.SignInAsync(IdentityConstants.ApplicationScheme,
                    //    new ClaimsPrincipal(identity));


                }
                if (result.IsNotAllowed)
                {
                    ModelState.AddModelError(string.Empty, "شماره موبایل تایید نشده است");
                    return View(model);
                }
                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "اشتباه بیش از حد مجاز. لطفا دو دقیقه صبر کنید");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "شماره موبایل یا رمز عبور اشتباه است");
                    return View(model);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "شماره موبایل یا رمز عبور اشتباه است");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #endregion Login / logout


        public IActionResult ChangePassword()
        {
            var model = new ChangePasswordModel();
            ViewBag.Success = TempData["Success"];
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                if (model.NewPassword != model.ReNewPassword)
                {
                    ModelState.AddModelError(string.Empty, "رمز عبور جدید با تکرار آن یکسان نیست");
                    return View(model);
                }

                var user = _userManager.GetUserAsync(User).Result;
                var result = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);

                if (result.Result.Succeeded)
                {
                    TempData["Success"] = "تغییر رمز عبور با موفقیت انجام شد";
                    ViewBag.Success = TempData["Success"];
                }
                else
                {
                    foreach (var error in result.Result.Errors)
                    {
                        if (error.Code == "PasswordMismatch")
                            ModelState.TryAddModelError(error.Code, "رمز عبور قبلی صحیج نمی باشد");
                    }
                }
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return View(model);
            }
        }

        #endregion Methods
    }
}
