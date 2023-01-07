using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceCenterBaseModel
    {
        public int BaseId { get; set; }
        public string BaseTitle { get; set; }
        public bool? IsChecked { get; set; }

    }
}
