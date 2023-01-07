using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class PackageTemplateDetailModel
    {
        public int Id { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeTitle { get; set; }
        public decimal ServiceTypeUnitPrice { get; set; }
        public short Quantity { get; set; }
    }
}
