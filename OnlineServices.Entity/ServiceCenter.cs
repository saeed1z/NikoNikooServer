using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceCenter
    {
        public Guid Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(50)")]
        public string FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(50)")]
        public string LastName { get; set; }

        [Display(Name = "کد ملی")]
        [Column(TypeName = "varchar(10)")]
        public string NationalCode { get; set; }

        [Display(Name = "استان محل خدمت")]
        [ForeignKey("StateId")]
        public byte? StateId { get; set; }
        public virtual State State { get; set; }

        [Display(Name = "شهر محل خدمت")]
        [ForeignKey("CityId")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }

        [Display(Name = "شماره موبایل")]
        [Column(TypeName = "varchar(12)")]
        public string MobileNo { get; set; }

        [Display(Name = "شماره تماس ثابت")]
        [Column(TypeName = "varchar(12)")]
        public string PhoneNo { get; set; }

        [Display(Name = "آدرس")]
        [Column(TypeName = "nvarchar(200)")]
        public string Address { get; set; }

        [Display(Name = "کد پستی")]
        [Column(TypeName = "decimal(10, 0)")]
        public decimal? PostCode { get; set; }

        [Display(Name = "عرض جغرافیایی")]
        public double? Latitude { get; set; }

        [Display(Name = "طول جغرافیایی")]
        public double? Longitude { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "تصویر کاربر")]
        [Column(TypeName = "varchar(MAX)")]
        public string Avatar { get; set; }

        [Display(Name = "آدرس ایمیل")]
        [Column(TypeName = "varchar(100)")]
        public string Email { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsCarwash { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsMechanic { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsService { get; set; }

        [Display(Name = "")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsAccessory { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "کاربر ثبت کننده اطلاعات")]
        public Guid CreatedUserId { get; set; }

        [Display(Name = "تاریخ بروزرسانی اطلاعات")]
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "کاربر بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }
    }
}
