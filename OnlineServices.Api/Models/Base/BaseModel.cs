using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class BaseModel
    {
        public int Id { get; set; }
        public short BaseKindId { get; set; }
        public string BaseKindTitle { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
