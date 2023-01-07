using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceCenterDetail
    {
        public Guid Id { get; set; }

        [Display(Name = "شناسه مرکز خدماتی")]
        [ForeignKey("ServiceCenterId")]
        public Guid ServiceCenterId { get; set; }
        public virtual ServiceCenter ServiceCenter { get; set; }

        [Display(Name = "نوع خدمت قابل ارائه")]
        [ForeignKey("ServiceDetailBaseId")]
        public int ServiceDetailBaseId { get; set; }
        public virtual Base ServiceDetailBase { get; set; }

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
