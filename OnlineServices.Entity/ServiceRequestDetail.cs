using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceRequestDetail
    {
        public Guid Id { get; set; }

        [Display(Name = "شناسه درخواست سرویس")]
        [ForeignKey("ServiceRequestId")]
        public Guid ServiceRequestId { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }

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
