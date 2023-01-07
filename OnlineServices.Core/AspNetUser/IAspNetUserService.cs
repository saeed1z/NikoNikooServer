using Microsoft.AspNetCore.Identity;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IAspNetUserService
    {
        //Task<string> RegisterWithMobileAsync(string MobileNo);
        Task<string> ConfirmMobileAsync(string MobileNo, string ConfirmNo);
        IdentityUser GetUserByToken(string Token);
    }
}
