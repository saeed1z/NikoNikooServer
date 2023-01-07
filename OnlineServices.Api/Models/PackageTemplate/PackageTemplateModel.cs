using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class PackageTemplateModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte? PersonTypeId { get; set; }
        public int? PersonCarId { get; set; }
        public string PersonTypeTitle { get; set; }
        public decimal RealPrice { get; set; }
        public decimal Price { get; set; }
        public short ExpiredDuration { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedUserId { get; set; }
        public bool IsActive { get; set; }
        public List<PackageTemplateDetailModel> PackageTemplateDetailList { get; set; }

        public PackageTemplateModel()
        {
            this.PackageTemplateDetailList = new List<PackageTemplateDetailModel>();
        }

    }
}
