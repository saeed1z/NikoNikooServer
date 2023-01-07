using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ServiceFactorModel
    {
        public string Id { get; set; }
        public string ServiceRequestId { get; set; }
        public string IssueDate { get; set; }
        public string SalesShop { get; set; }
        public string SalesShopAddress { get; set; }
        public string SalesShopPhone { get; set; }
        public decimal TotalCost { get; set; }
        public Nullable<decimal> DiscountFee { get; set; }
        public Nullable<decimal> FinalFee { get; set; }
    }
}
