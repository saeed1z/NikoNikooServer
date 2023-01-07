using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class PackageTemplateListModel
    {
        public IList<PackageTemplateModel> PackageTemplateModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
