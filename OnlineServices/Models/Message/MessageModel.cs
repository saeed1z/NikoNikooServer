using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class MessageModel
    {
        public int RowNum { get; set; }

        public long Id { get; set; }

        [Display(Name = "سرویس")]
        public string ServiceRequestId { get; set; }

        [Display(Name = "فرستنده پیام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string FromPersonId { get; set; }
        public string FromPersonName { get; set; }

        [Display(Name = "گیرنده پیام")]
        public string ToPersonId { get; set; }
        public string ToPersonName { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Body { get; set; }

        public string CreatedDate { get; set; }

        [Display(Name = "فایل ضبظ شده سرویس")]
        public string ServiceCaptureId { get; set; }

        [Display(Name = "اجازه پاسخ؟")]
        public bool AllowResponse { get; set; }

        [Display(Name = "خوانده شده؟")]
        public bool IsRead { get; set; }


    }
}
