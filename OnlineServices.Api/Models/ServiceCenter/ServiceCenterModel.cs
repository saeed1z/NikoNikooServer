using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ServiceCenterModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public byte? StateId { get; set; }
        public string StateTitle { get; set; }
        public int? CityId { get; set; }
        public string CityTitle { get; set; }
        public string MobileNo { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public decimal? PostCode { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsActive { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public bool? IsCarwash { get; set; }
        public bool? IsMechanic { get; set; }
        public bool? IsService { get; set; }
        public bool? IsAccessory { get; set; }

        public List<BaseModel> ServiceItemsList { get; set; }

    }
}
