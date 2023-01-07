using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public class ServiceTypeQuestion
    {
        public int Id { get; set; }

        [Display(Name = " نوع خدمت  ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceTypeId")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [Display(Name = " عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Display(Name = " توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Description { get; set; }

        [Display(Name = " ترتیب نمایش")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [DefaultValue(99)]
        public byte OrderNo { get; set; }

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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "شناسه ثبت کننده اطلاعات")]
        public Guid? CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی اطلاعات ")]
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = " شناسه بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }

    }

}
