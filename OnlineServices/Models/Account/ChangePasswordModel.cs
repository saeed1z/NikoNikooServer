using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ChangePasswordModel
    {
        [Display(Name = "رمز عبور", Prompt = "رمز عبور")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید بیش از ۶ حرف یا عدد باشد")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }


        [Display(Name = "رمز عبور", Prompt = "رمز عبور")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید بیش از ۶ حرف یا عدد باشد")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "تکرار رمز عبور", Prompt = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [MinLength(6, ErrorMessage = "رمز عبور باید بیش از ۶ حرف یا عدد باشد")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "با زمز عبور یکسان نمی باشد")]
        public string ReNewPassword { get; set; }
    }
}
