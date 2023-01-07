using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public class ServiceRequestSurvey
    {
        public Guid Id { get; set; }

        [Display(Name = " شناسه درخواست")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceRequestId")]
        public Guid ServiceRequestId { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }

        [Display(Name = " شناسه نظرسنجی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceTypeQuestionId")]
        public int ServiceTypeQuestionId { get; set; }
        public virtual ServiceTypeQuestion ServiceTypeQuestion { get; set; }

        [Display(Name = " امتیاز بین 1 تا 5")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public byte Score { get; set; }

        [Display(Name = " توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(255)")]
        public string Description { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }
        [Display(Name = " تاریخ ثبت ")]
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
        [Display(Name = "شناسه ثبت کننده اطلاعات ")]
        public Guid? CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی اطلاعات ")]
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Display(Name = " شناسه بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }

    }
}
