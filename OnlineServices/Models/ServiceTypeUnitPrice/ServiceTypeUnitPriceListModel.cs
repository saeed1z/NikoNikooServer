using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceTypeUnitPriceListModel
    {
        public IList<ServiceTypeUnitPriceModel> ServiceTypeUnitPriceModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
