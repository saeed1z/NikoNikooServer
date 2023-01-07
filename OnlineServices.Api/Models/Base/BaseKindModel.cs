using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class BaseKindModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool HasChild { get; set; }
    }
}
