using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Api.Models
{
    public partial class NotifBodyPostModel
    {
        public string title { get; set; }
        public string body { get; set; }
    }
}
