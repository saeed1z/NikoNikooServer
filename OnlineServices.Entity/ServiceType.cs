using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public partial class ServiceType
    {
        public int Id { get; set; }
        [Display(Name = " نوع سرویس  ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }
        [Display(Name = " توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }


        [Display(Name = " وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }


        [Display(Name = " ذخیره شده")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [DefaultValue(false)]
        public bool IsReserved { get; set; }
        [Timestamp]
        public byte[] Version { get; set; }
        [Display(Name = " تاریخ ثبت ")]

        public DateTime? CreatedDate { get; set; }
        [Display(Name = "نام ثبت کننده اطلاعات ")]

        public Guid? CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی ثبت ")]

        public DateTime? UpdatedDate { get; set; }
        [Display(Name = " بروزرسانی نام ثبت کننده اطلاعات")]

        public Guid? UpdatedUserId { get; set; }

        public byte NeedToCommute { get; set; }

        public virtual ICollection<PersonService> PersonService { get; set; }
    }
}
