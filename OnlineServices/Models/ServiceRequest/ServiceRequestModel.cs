using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceRequestModel
    {
        public string Id { get; set; }
        public int RowNum { get; set; }
        public string PersonId { get; set; }
        public string PersonName { get; set; }
        public string RequestDateTime { get; set; }
        public int ServiceTypeId { get; set; }
        public string ServiceTypeTitle{ get; set; }
        public int? PersonCarId { get; set; }
        public byte? SourceStateId { get; set; }
        public string SourceStateTitle { get; set; }
        public int? SourceCityId { get; set; }
        public string SourceCityTitle { get; set; }
        public string SourceAddress { get; set; }
        public string SourceLocation { get; set; }
        public byte? DestinationStateId { get; set; }
        public string DestinationStateTitle { get; set; }
        public int? DestinationCityId { get; set; }
        public string DestinationCityTitle { get; set; }
        public string DestinationAddress { get; set; }
        public string DestinationLocation { get; set; }
        public string Description { get; set; }
        public byte? LastStatusId { get; set; }
        public string LastStatusTitle { get; set; }
        public int? BrandId { get; set; }
        public string BrandTitle { get; set; }
        public int? ModelId { get; set; }
        public string ModelTitle { get; set; }
        public string EmployeeId { get; set; }
        public string EmployeeTitle { get; set; }
        public string ExpertId { get; set; }
        public string ExpertTitle { get; set; }
    }
}
