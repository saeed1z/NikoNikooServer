using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class Person
    {
        public Guid Id { get; set; }

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

        [Display(Name = " نوع همکار  ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("PersonTypeId")]
        public byte PersonTypeId { get; set; }

        public virtual PersonType PersonType { get; set; }

        [Display(Name = "  نام همکار ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string FirstName { get; set; }

        [Display(Name = " نام خانوادگی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string LastName { get; set; }

        [Display(Name = " کد ملی")]
        [RegularExpression(@"\d{10}", ErrorMessage = "به درستی وارد کنید")]
        public string NationalCode { get; set; }

        [Display(Name = " شماره کارمند")]
        [RegularExpression(@"(\d)*", ErrorMessage = "به درستی وارد کنید")]
        public int? EmployeeNo { get; set; }

        [Display(Name = " استان ")]
        [ForeignKey("State")]
        public byte? StateId { get; set; }

        public virtual State State { get; set; }

        [Display(Name = " شهر ")]
        [ForeignKey("City")]
        public int? CityId { get; set; }

        public virtual City City { get; set; }

        [Display(Name = " موبایل ")]
        public string MobileNo { get; set; }

        [Display(Name = " تلفن ثابت ")]
        public string PhoneNo { get; set; }

        [Display(Name = " آدرس ")]
        public string Address { get; set; }

        [Display(Name = " کد پستی ")]
        [Column(TypeName = "decimal(10, 0)")]
        public decimal? PostCode { get; set; }


        [Display(Name = " نام شركت ")]
        public string CompanyName { get; set; }

        [Display(Name = " شماره ثبت ")]
        public string RegistrationNumber { get; set; }

        [Display(Name = " تاريخ ثبت ")]
        public string RegistrationDate { get; set; }

        [Display(Name = " محل ثبت ")]
        public string RegistrationPlace { get; set; }

        [Display(Name = " كد اقتصادي ")]
        public string EconomicCode { get; set; }

        [Display(Name = " وب سايت ")]
        public string WebsiteUrl { get; set; }

        [Display(Name = " آدرس ايميل ")]
        public string Email { get; set; }

        [Display(Name = " نشاني محل فعاليت ")]
        public string ActivityAddress { get; set; }

        [Display(Name = " نام رابط ")]
        public string InterfaceName { get; set; }

        [Display(Name = " نام خانوادگي رابط ")]
        public string InterfaceFamily { get; set; }

        [Display(Name = " سمت ")]
        public string Post { get; set; }


        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = " طول جغرافیایی ")]
        public double? Longitude { get; set; }

        [Display(Name = " تاریخ پایان همکاری")]
        public DateTime? CooperationEndDate { get; set; }

        [Display(Name = " تاریخ شروع همکاری")]
        public DateTime? CooperationStartDate { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "کاربر ثبت کننده اطلاعات")]
        //[Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid? CreatedUserId { get; set; }

        [Display(Name = "تاریخ بروزرسانی اطلاعات")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "کاربر بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }

        [DefaultValue(true)]
        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "آواتار")]
        public string Avatar { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Notifykey { get; set; }

        public virtual ICollection<PersonService> PersonService { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
