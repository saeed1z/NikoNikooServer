using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ServiceFactorDetailModel
    {
        public string Id { get; set; }
        public string ServiceFactorId { get; set; }
        public byte Row { get; set; }
        public string ItemTitle { get; set; }
        public byte Quantity { get; set; }
        public decimal UnitFee { get; set; }
    }
}
