using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class MessageImageModel
    {
        public string UserId { get; set; }
        public string ServiceRequestId { get; set; }
        public string Image { get; set; }
        public string ImageExtension { get; set; }
    }
}
