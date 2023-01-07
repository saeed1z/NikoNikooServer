using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Models
{
    public partial class UserModel
    {
        public int RowNum { get; set; }
        public string Id { get; set; }

        [Display(Name = " نوع همکار  ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را انتخاب کنید")]
        public byte PersonTypeId { get; set; }
        public string PersonTypeTitle { get; set; }

        [Display(Name = " نام ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string FirstName { get; set; }

        [Display(Name = " نام خانوادگی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string LastName { get; set; }

        [Display(Name = " کدملی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [RegularExpression(@"\d{10}", ErrorMessage = "به درستی وارد کنید")]
        public string NationalCode { get; set; }

        [Display(Name = " شماره کارمند")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [RegularExpression(@"(\d)*", ErrorMessage = "به درستی وارد کنید")]
        public int? EmployeeNo { get; set; }

        [Display(Name = " استان ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را انتخاب کنید")]
        public byte? StateId { get; set; }
        public string StateTitle { get; set; }

        [Display(Name = " شهر ")]   
        [Required(ErrorMessage = "لطفا فیلد {0} را انتخاب کنید")]
        public int? CityId { get; set; }
        public string CityTitle { get; set; }

        [Display(Name = " موبایل ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string MobileNo { get; set; }

        [Display(Name = " تلفن ثابت ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string PhoneNo { get; set; }

        [Display(Name = " آدرس ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Address { get; set; }

        [Display(Name = " کد پستی ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [RegularExpression(@"\d{10}", ErrorMessage = "به درستی وارد کنید")]
        public decimal? PostCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [Display(Name = " تاریخ پایان همکاری")]   
        public string CooperationEndDate { get; set; }

        [Display(Name = " تاریخ شروع همکاری")]
        public string CooperationStartDate { get; set; }
        public string LastServiceRequestDate { get; set; }

        [Display(Name = "نام پدر")]
        public string FatherName { get; set; }

        [Display(Name = "تاريخ تولد")]
        public string BirthDate { get; set; }

        [Display(Name = "جنسیت")]
        public string Gender { get; set; }

        [Display(Name = "تاريخ تولد")]
        public string EducationLevel { get; set; }

        [Display(Name = "منطقه شهري")]
        public string CityArea { get; set; }

        [Display(Name = "طبقه")]
        public string BuildingFloor { get; set; }

        [Display(Name = "پلاک")]
        public string BuildingPlate { get; set; }

        [Display(Name = "واحد")]
        public string BuildingUnit { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        public List<SelectListItem> PersonTypeList { get; set; }
        public List<SelectListItem> StateList { get; set; }
        public List<SelectListItem> CityList { get; set; }
        public List<SelectListItem> ServiceTypeList { get; set; }
        public List<int> PersonServiceIds { get; set; }
        public List<PersonCarModel> PersonCarList { get; set; }
        public List<PersonPackageModel> PersonPackageList { get; set; }
        public List<ServiceRequestModel> ServiceRequestList { get; set; }

        public UserModel()
        {
            this.PersonTypeList = new List<SelectListItem>();
            this.StateList = new List<SelectListItem>();
            this.CityList = new List<SelectListItem>();
            this.ServiceTypeList = new List<SelectListItem>();
            this.PersonCarList = new List<PersonCarModel>();
            this.PersonPackageList = new List<PersonPackageModel>();
            this.ServiceRequestList = new List<ServiceRequestModel>();
        }

    }
}
