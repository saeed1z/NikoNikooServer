using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineServices.ViewModel
{
    public class ServiceRequestInformation
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string ExpertName { get; set; }
        public string EmployeeName { get; set; }
        public string CustomerMobileNumber{ get; set; }
        public string ExpertMobileNumber { get; set; }
        public string EmployeeMobileNumber { get; set; }
        public string destination { get; set; }
        public string source { get; set; }
        public string plaqueNo {get; set;} 
        public string carModel { get; set; }
        public string carBrand { get; set; }
        public string serviceType { get; set; }
        public string serviceDescription { get; set; }
        public string carDescription { get; set; }
        public IEnumerable<string> details { get; set; }

    }
}
