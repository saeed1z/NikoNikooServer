using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceTypeListModel
    {
        public IList<ServiceTypeModel> ServiceTypeModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
