using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public partial class City
    {
        public int Id { get; set; }
        [Display(Name = " نام استان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public byte StateId { get; set; }
        [Display(Name = " نام شهر")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }
        [Display(Name = " توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }
        [Display(Name = " وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }
       [Timestamp]
        public byte[] Version { get; set; }
        [Display(Name = " تاریخ ثبت ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "نام ثبت کننده اطلاعات ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid? CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی ثبت ")]
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = " بروزرسانی نام ثبت کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }

        public virtual State State { get; set; }
    }
}
