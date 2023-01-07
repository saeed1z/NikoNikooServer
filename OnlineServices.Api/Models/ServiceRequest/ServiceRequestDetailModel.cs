using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ServiceRequestDetailModel
    {
        public string Title { get; set; }
        public int Value { get; set; }
    }
}
