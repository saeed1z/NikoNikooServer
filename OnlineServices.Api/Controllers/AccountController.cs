using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Api.Helpers;
using OnlineServices.Core;
using OnlineServices.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using OnlineServices.Api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{


    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IPersonService _personService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ITokenManagerService _tokenManagerService;
        private readonly IConfiguration _config;

        public AccountController(
            IAspNetUserService aspNetUserService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ITokenManagerService _tokenManagerService,
            IPersonService personService,
            IConfiguration config)
        {
            this._aspNetUserService = aspNetUserService;
            this._personService = personService;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._tokenManagerService = _tokenManagerService;
            this._config = config;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var randomPass = BaseSettings.RandomText(6);

                model.MobileNo = BaseSettings.ConvertDigitsToLatin(model.MobileNo);
                if (model.MobileNo.Length < 11)
                    return Ok(new { errorId = 1, errorTitle = "شماره موبایل نامعتبر می باشد", result = (string)null });

                var normMobileNo = BaseSettings.NormilizeMobileNo(model.MobileNo);
                if (normMobileNo == "" || normMobileNo.Length < 11)
                    return Ok(new { errorId = 1, errorTitle = "شماره موبایل نامعتبر می باشد", result = (string)null });


                if (model.PersonTypeId == 3)
                {
                    var person = _personService.GetByMobileNo(normMobileNo);
                    if (person == null)
                        return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

                    if (person.PersonTypeId != (int)PersonTypeEnum.Employee && person.PersonTypeId != (int)PersonTypeEnum.Expert)
                        return Ok(new { errorId = 99, errorTitle = "شما مجاز به استفاده از این اپلیکیشن نمی باشید", result = (string)null });
                }

                var user = await _userManager.FindByNameAsync(normMobileNo);
                if (user != null)
                {
                    await _userManager.RemovePasswordAsync(user);
                    await _userManager.AddPasswordAsync(user, randomPass);
                    SMS.Send(normMobileNo, randomPass);
                    await _signInManager.SignInAsync(user, false);
                    return Ok(new { errorId = 0, errorTitle = "", result = (string)null });
                }
                else
                {
                    var newUser = new ApplicationUser()
                    {
                        UserName = normMobileNo,
                        PhoneNumber = normMobileNo,
                        Email = normMobileNo + "@OnlineServices.ir",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                    };
                    var result = await _userManager.CreateAsync(newUser, randomPass);
                    if (result.Succeeded)
                    {
                        SMS.Send(normMobileNo, randomPass);
                        await _signInManager.SignInAsync(newUser, false);
                        return Ok(new { errorId = 0, errorTitle = "", result = (string)null });
                    }
                    else
                        return Ok(new { errorId = 2, errorTitle = "خطا در ثبت نام کاربر", result = (string)null });
                }
            }
            catch (AppException ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [AllowAnonymous]
        [HttpPost("ConfirmMobileNo")]
        public async Task<IActionResult> ConfirmMobileNo([FromBody] ConfirmMobileNoModel model)
        {
            string token = null;

            model.MobileNo = BaseSettings.ConvertDigitsToLatin(model.MobileNo);
            model.ConfirmNo = BaseSettings.ConvertDigitsToLatin(model.ConfirmNo);

            if (model.MobileNo.Length < 11)
                return Ok(new { errorId = 1, errorTitle = "شماره موبایل نامعتبر می باشد", result = (string)null });

            var normMobileNo = BaseSettings.NormilizeMobileNo(model.MobileNo);
            if (normMobileNo == "" || normMobileNo.Length < 11)
                return Ok(new { errorId = 1, errorTitle = "شماره موبایل نامعتبر می باشد", result = (string)null });

            var user = await _userManager.FindByNameAsync(normMobileNo);

            if (user == null)
                return Ok(new { errorId = 3, errorTitle = "کاربری با این مشخصات پیدا نشد", result = (string)null });

            if (!await _userManager.CheckPasswordAsync(user, model.ConfirmNo))
            {
                return Ok(new { errorId = 4, errorTitle = "کد تایید وارد شده صحیح نمی باشد", result = (string)null });
                //return StatusCode(403, new { errorId = 4, errorTitle = "کد تایید وارد شده صحیح نمی باشد", result = (string)null });
            }
            try
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.ConfirmNo, true, true);

                if (result.Succeeded)
                {
                    token = GenerateJWTToken(user);
                    //token = await _userManager.GenerateUserTokenAsync(user, "OnlineService", "RefreshToken");
                    await _userManager.SetAuthenticationTokenAsync(user, "OnlineService", "RefreshToken", token);

                    return Ok(new { errorId = 0, errorTitle = "", result = token });
                }
                if (result.IsNotAllowed)
                {
                    user.PhoneNumberConfirmed = true;
                    user.EmailConfirmed = true;
                    var updateResult = await _userManager.UpdateAsync(user);
                    if (updateResult.Succeeded)
                    {
                        token = GenerateJWTToken(user);
                        //token = await _userManager.GenerateUserTokenAsync(user, "OnlineService", "RefreshToken");
                        await _userManager.SetAuthenticationTokenAsync(user, "OnlineService", "RefreshToken", token);

                        return Ok(new { errorId = 0, errorTitle = "", result = token });
                    }
                    else
                        return Ok(new { errorId = 5, errorTitle = "فعالسازی با مشکل مواجه شد", result = (string)null });
                }
                else
                    return Ok(new { errorId = 6, errorTitle = "اطلاعات وارد شده صحیح نمی باشد", result = (string)null });
            }
            catch (AppException ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("UsersList")]
        public IActionResult UsersList()
        {
            var model = new List<RegisteredUserModel>();
            var userList = _userManager.Users;
            foreach (var item in userList)
            {
                var modelItem = new RegisteredUserModel
                {
                    UserId = item.Id,
                    Username = item.UserName,
                    MobileNumber = item.PhoneNumber,
                    MobileNumberConfirmed = item.PhoneNumberConfirmed
                };
                var person = _personService.GetByMobileNo(item.UserName);
                if (person != null)
                {
                    modelItem.PersonId = person.Id.ToString();
                    modelItem.FirstName = person.FirstName;
                    modelItem.LastName = person.LastName;
                    modelItem.PhoneNo = person.PhoneNo;
                    modelItem.Address = person.Address;
                    modelItem.NationalCode = person.NationalCode;
                    modelItem.EmployeeNo = person.EmployeeNo;
                }
                model.Add(modelItem);
            }

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = model
            });
        }

        [AllowAnonymous]
        [HttpPost("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _tokenManagerService.DeactivateCurrentAsync();
            return Ok(new { errorId = 0, errorTitle = "", result = (string)null });
        }

        public string GenerateJWTToken(IdentityUser userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.UserName),
                new Claim("UserName", userInfo.UserName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
