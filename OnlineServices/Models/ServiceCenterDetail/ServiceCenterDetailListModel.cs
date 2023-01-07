using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceCenterDetailListModel
    {
        public IList<ServiceCenterDetailModel> ServiceCenterDetailModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
