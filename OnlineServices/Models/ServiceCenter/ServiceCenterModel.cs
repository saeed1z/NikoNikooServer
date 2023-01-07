using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceCenterModel
    {
        public int RowNum { get; set; }

        public string Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string LastName { get; set; }

        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }

        [Display(Name = "استان محل خدمت")]
        public byte? StateId { get; set; }
        public string StateTitle { get; set; }

        [Display(Name = "شهر محل خدمت")]
        public int? CityId { get; set; }
        public string CityTitle { get; set; }

        [Display(Name = "شماره موبایل")]
        public string MobileNo { get; set; }

        [Display(Name = "شماره تماس ثابت")]
        public string PhoneNo { get; set; }

        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [Display(Name = "کد پستی")]
        public decimal? PostCode { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double? Longitude { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "تصویر کاربر")]
        public string Avatar { get; set; }

        [Display(Name = "تصویر کاربر")]
        public IFormFile AvatarImage { get; set; }

        [Display(Name = "آدرس ایمیل")]
        public string Email { get; set; }

        [Display(Name = "")]
        public bool? IsCarwash { get; set; }

        [Display(Name = "")]
        public bool? IsMechanic { get; set; }

        [Display(Name = "")]
        public bool? IsService { get; set; }

        [Display(Name = "")]
        public bool? IsAccessory { get; set; }

        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CityList { get; set; }

        public List<SelectListItem> CarwashBaseList { get; set; }
        public List<SelectListItem> MechanicBaseList { get; set; }
        public List<SelectListItem> ServiceBaseList { get; set; }
        public List<SelectListItem> AccessoryBaseList { get; set; }

        public ServiceCenterModel()
        {
            this.StateList = new List<SelectListItem>();
            this.CityList = new List<SelectListItem>();
            this.CarwashBaseList = new List<SelectListItem>();
            this.MechanicBaseList = new List<SelectListItem>();
            this.ServiceBaseList = new List<SelectListItem>();
            this.AccessoryBaseList = new List<SelectListItem>();
        }

    }
}
