using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Models
{
    public partial class PersonCarModel
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public string PersonName { get; set; }
        public int? ModelId { get; set; }
        public string ModelTitle { get; set; }
        public string PlaqueNo { get; set; }
        public string ChassisNo { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
