using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public class Message
    {
        public long Id { get; set; }

        [ForeignKey("ServiceRequestId")]
        [Display(Name = "سرویس")]
        public Guid? ServiceRequestId { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }

        [ForeignKey("FromPersonId")]
        [Display(Name = "فرستنده پیام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid FromPersonId { get; set; }
        public virtual Person FromPerson { get; set; }

        [ForeignKey("ToPersonId")]
        [Display(Name = "گیرنده پیام")]
        public Guid? ToPersonId { get; set; }
        public virtual Person ToPerson { get; set; }

        [Column(TypeName = "nvarchar(1000)")]
        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Body { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime CreatedDate { get; set; }

        [ForeignKey("ServiceCaptureId")]
        [Display(Name = "فایل ضبظ شده سرویس")]
        public Guid? ServiceCaptureId { get; set; }
        public virtual ServiceCapture ServiceCapture { get; set; }

        [Display(Name = "اجازه پاسخ؟")]
        public bool AllowResponse { get; set; }

        [Display(Name = "خوانده شده؟")]
        public bool IsRead { get; set; }

    }

}
