using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Api.Models
{
    public partial class FireBaseNotifPostModel
    {
        public List<string> registration_ids { get; set; }
        public NotifBodyPostModel notification { get; set; }
        public int priority { get; set; }
        public Dictionary<string, string> data { get; set; }
        public string badge { get; set; }

    }
}
