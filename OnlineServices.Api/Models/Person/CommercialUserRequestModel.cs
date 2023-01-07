using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Api.Models
{
    public partial class CommercialUserRequestModel
    {
        public int Id { get; set; }
        public string PersonId { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string RegistrationPlace { get; set; }
        public string EconomicCode { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ActivityAddress { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceFamily { get; set; }
        public string Post { get; set; }
        public bool IsRejected { get; set; }
        public bool IsAccepted { get; set; }
    }
}
