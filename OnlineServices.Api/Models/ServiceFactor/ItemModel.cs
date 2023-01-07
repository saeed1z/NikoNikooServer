using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ItemModel
    {
        public string ServiceFactorId { get; set; }
        public string ServiceFactorDetailId { get; set; }
        public string ServiceRequestId { get; set; }
        public string ItemTitle { get; set; }
        public byte Quantity { get; set; }
        public decimal UnitFee { get; set; }
        public string SalesShop { get; set; }
        public string SalesShopAddress { get; set; }
        public string SalesShopPhone { get; set; }
        public decimal TotalCost { get; set; }
        public decimal DiscountFee { get; set; }
        public decimal FinalFee { get; set; }
        public bool? IsPaid { get; set; }
    }
}
