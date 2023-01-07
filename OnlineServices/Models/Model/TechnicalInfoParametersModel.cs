using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class TechnicalInfoParametersModel
    {
        public int BaseId { get; set; }
        public string BaseTitle { get; set; }
        public string Value { get; set; }

    }
}
