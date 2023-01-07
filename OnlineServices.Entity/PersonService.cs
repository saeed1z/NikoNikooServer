using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public partial class PersonService
    {
        public int Id { get; set; }
        [Display(Name = "  نام همکار ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid PersonId { get; set; }
        [Display(Name = "  نام نوع سرویس ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public int ServiceTypeId { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        [Display(Name = " تاریخ ثبت ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]

        public DateTime CreatedDate { get; set; }
        [Display(Name = "نام ثبت کننده اطلاعات ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]

        public Guid CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی ثبت ")]

        public DateTime? UpdatedDate { get; set; }
        [Display(Name = " بروزرسانی نام ثبت کننده اطلاعات")]

        public Guid? UpdatedUserId { get; set; }

        public virtual Person Person { get; set; }
        public virtual ServiceType ServiceType { get; set; }
    }
}
