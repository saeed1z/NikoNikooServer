using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Models
{
    public partial class PersonPackageModel
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public string PersonTitle { get; set; }
        public string PackageTemplateId { get; set; }
        public string PackageTemplateTitle { get; set; }
        public string PackageTemplateDescription { get; set; }
        public bool PackageTemplateIsActive { get; set; }
        public decimal Price { get; set; }
        public string FactorDate { get; set; }
        public string ExpiredDate { get; set; }
        public int? FactorNumber { get; set; }
        public string BankDocument { get; set; }
        public string BankId { get; set; }
        public string BankDocumentDate { get; set; }
        public string CanceledDate { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedUserId { get; set; }
        public string UpdatedDate { get; set; }
        public string UpdatedUserId { get; set; }
        public List<PersonPackageDetailModel> PersonPackageDetailList { get; set; }

        public PersonPackageModel()
        {
            this.PersonPackageDetailList = new List<PersonPackageDetailModel>();
        }
    }
}
