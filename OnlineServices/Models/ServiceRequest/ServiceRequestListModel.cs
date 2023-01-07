using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceRequestListModel
    {
        public IList<ServiceRequestModel> ServiceRequestModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
