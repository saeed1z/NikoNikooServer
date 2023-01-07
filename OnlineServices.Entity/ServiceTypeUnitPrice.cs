using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceTypeUnitPrice
    {
        public int Id { get; set; }

        [Display(Name = "نوع خدمت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceTypeId")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [Display(Name = "قیمت واحد سرویس")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        [Display(Name = " تاریخ ثبت ")]
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "نام ثبت کننده اطلاعات ")]
        public Guid? CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی ثبت ")]
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = " بروزرسانی نام ثبت کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }
    }
}
