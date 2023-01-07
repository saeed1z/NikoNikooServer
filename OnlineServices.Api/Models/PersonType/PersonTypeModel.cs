using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class PersonTypeModel
    {
        public byte Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
