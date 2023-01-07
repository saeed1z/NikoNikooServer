using System;

namespace OnlineServices.Api.Models
{
    public partial class PersonModel
    {
        public string Id { get; set; }
        public byte PersonTypeId { get; set; }
        public string PersonTypeTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NationalCode { get; set; }
        public int? EmployeeNo { get; set; }
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
        public string CooperationEndDate { get; set; }
        public string CooperationStartDate { get; set; }
        public bool IsActive { get; set; }
        public string Avatar { get; set; }
        public bool HasAvatar { get; set; }
        public string Notifykey { get; set; }
        public string CommercialStatus { get; set; }
        public int CommercialStatusCode { get; set; }

        public string FatherName { get; set; }

        public string BirthDate { get; set; }

        public string Gender { get; set; }

        public string EducationLevel { get; set; }

        public string CityArea { get; set; }

        public string BuildingFloor { get; set; }

        public string BuildingPlate { get; set; }

        public string BuildingUnit { get; set; }

        public bool IsCompelete { get; set; }

        public string Email { get; set; }

    }
}
