using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Api.Models
{
    public partial class PersonPackageDetailModel
    {
        public int Id { get; set; }
        //public int PersonPackageId { get; set; }
        //public string PersonPackageTitle { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeTitle { get; set; }
        public short Quantity { get; set; }
        public short UsedQuantity { get; set; }
    }
}
