using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class MessageModel
    {
        public long Id { get; set; }
        public string ServiceRequestId { get; set; }
        public string FromPersonId { get; set; }
        public string FromPersonName { get; set; }
        public string ToPersonId { get; set; }
        public string ToPersonName { get; set; }
        public string Body { get; set; }
        public string CreatedDate { get; set; }
        public string LastDateTime { get; set; }
        public string ServiceCaptureId { get; set; }
        public bool AllowResponse { get; set; }
        public bool IsSender { get; set; }
        public bool IsRead { get; set; }
        public string Image { get; set; }
        public string ImageExtension { get; set; }
    }
}
