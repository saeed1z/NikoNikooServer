using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineServices.Api.Models
{
    public class ServiceRequestFile
    {
        [Required(ErrorMessage = "پیشوند فایل اجباری است")]
        public string RequestFileExtension { get; set; }

        [Required(ErrorMessage = "فایل نمی تواند خالی باشد")]
        public string RequestFileBase64 { get; set; }

        public string ServiceRequestId { get; set; }

    }
}
