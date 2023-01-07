using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ModelModel
    {
        public int Id { get; set; }
        public int CarTypeBaseId { get; set; }
        public string CarTypeBaseTitle { get; set; }
        public int BrandId { get; set; }
        public string BrandTitle { get; set; }
        public string BrandEnTitle { get; set; }
        public string Title { get; set; }
        public string EnTitle { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsReserved { get; set; }
        
    }
}
