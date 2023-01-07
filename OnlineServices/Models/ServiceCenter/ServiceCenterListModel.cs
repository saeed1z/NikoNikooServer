using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceCenterListModel
    {
        public IList<ServiceCenterModel> ServiceCenterModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
