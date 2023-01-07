using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class PersonTypeModel
    {
        public int RowNum { get; set; }

        public byte Id { get; set; }

        [Display(Name = " نوع همکار")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }


        [Display(Name = " توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }


        [Display(Name = "دسترسی به پنل مدیریت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool PanelAccess { get; set; }


        [Display(Name = " دسترسی به سرویس های برنامه ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool ServiceAppAccess { get; set; }


        [Display(Name = " دسترسی به برنامه کاربر")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool UserAppAccess { get; set; }


        [Display(Name = " وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }


        [Display(Name = " ذخیره شده")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsReserved { get; set; }
    }
}
