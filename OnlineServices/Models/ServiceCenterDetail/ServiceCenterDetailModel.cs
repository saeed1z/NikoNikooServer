using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceCenterDetailModel
    {
        public int RowNum { get; set; }
        public string Id { get; set; }

        [Display(Name = "مرکز خدماتی")]
        public string ServiceCenterId { get; set; }
        public string ServiceCenterTitle { get; set; }

        [Display(Name = "نوع خدمت قابل ارائه")]
        public int ServiceDetailBaseId { get; set; }
        public string ServiceDetailBaseTitle { get; set; }


    }
}
