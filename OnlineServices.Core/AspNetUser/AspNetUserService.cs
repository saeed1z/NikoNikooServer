using Microsoft.AspNetCore.Identity;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class AspNetUserService : IAspNetUserService
    {
        private readonly OnlineServicesDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public AspNetUserService(UserManager<IdentityUser> userManager, 
            OnlineServicesDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        //public async Task<string> RegisterWithMobileAsync(string MobileNo)
        //{
        //    if (_db.Users.Any(x => x.UserName == MobileNo))
        //        return "-1"; //شماره موبایل وارد شده تکراری است

        //    Random random = new Random();
        //    int passRandom = random.Next(100000, 999999);
        //    try
        //    {


        //        //var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //        //var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
        //        //var user = new ApplicationUser() { UserName = Email.Text, Email = Email.Text };
        //        ApplicationUser user = new ApplicationUser()
        //        {
        //            UserName = MobileNo,
        //            PhoneNumber = MobileNo,
        //            Email = MobileNo.ToString() + "@OnlineServices.ir",
        //            EmailConfirmed = false,
        //            PhoneNumberConfirmed = false,
        //        };
        //        IdentityResult result = await _userManager.CreateAsync(user, passRandom.ToString());
                
        //        //await _userManager.GenerateUserTokenAsync(user);

        //        if (result.Succeeded)
        //        {
        //            signInManager.SignIn(user, false, rememberBrowser: false);
        //            //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);

        //        }
        //        else
        //        {
        //            return result.Errors.FirstOrDefault().ToString();
        //        }


        //        return passRandom.ToString();
        //    }
        //    catch(System.Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public async Task<string> ConfirmMobileAsync(string MobileNo, string ConfirmNo)
        {

            if (string.IsNullOrEmpty(MobileNo) || string.IsNullOrEmpty(ConfirmNo))
                return null;

            var user = _db.Users.SingleOrDefault(x => x.UserName == MobileNo);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            var result = await _userManager.CheckPasswordAsync(user, ConfirmNo);

            if (result)
                return null;

            var result2 = await _userManager.ConfirmEmailAsync(user, ConfirmNo);

            if (result2.Succeeded)
            {
            }
            else
            {
                return result2.Errors.FirstOrDefault().ToString();
            }

            // authentication successful
            return user.Id;
        }

        public IdentityUser GetUserByToken(string Token)
        {
            IdentityUser objAspNetUsers = null;
            var objAspNetUsersToken = _db.UserTokens.FirstOrDefault(x => x.Value == Token);
            if(objAspNetUsersToken!=null)
                objAspNetUsers = _db.Users.FirstOrDefault(x => x.Id == objAspNetUsersToken.UserId);

            return objAspNetUsers;
        }
    }
}
