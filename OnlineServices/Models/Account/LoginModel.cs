using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class LoginModel
    {
        [Display(Name = "شماره موبایل", Prompt = "09xxxxxxxxx")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        //[RegularExpression(@"^(?:(?:(?:\\+?|00)(98))|(0))?((?:90|91|92|93|99)[0-9]{8})$", ErrorMessage = "شماره موبایل نامعتبر است")]
        public string MobileNumber { get; set; }


        [Display(Name = "رمز عبور", Prompt = "رمز عبور")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید بیش از ۶ حرف یا عدد باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Display(Name = " مرا به خاطر بسپار")]
        public bool RememberMe { get; set; }
    }
}
