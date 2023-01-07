using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public class ServiceCapture
    {
        public Guid Id { get; set; }

        [ForeignKey("ServiceRequestId")]
        [Display(Name = "سرویس")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid ServiceRequestId { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }

        [ForeignKey("FileTypeBaseId")]
        [Display(Name = "نوع فایل")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public int FileTypeBaseId { get; set; }
        public virtual Base FileTypeBase { get; set; }

        [Column(TypeName = "varchar(5)")]
        [Display(Name = "پسوند فایل")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Extension { get; set; }

        [Display(Name = "کاربر ثبت کننده اطلاعات")]
        public Guid CreatedUserId { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "حذف از سرور")]
        public bool IsDeletedFromServer { get; set; }

    }
}
