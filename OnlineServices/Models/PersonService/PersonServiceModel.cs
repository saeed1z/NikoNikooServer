using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Models
{
    public partial class PersonServiceModel
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public int ServiceTypeId { get; set; }

    }
}
