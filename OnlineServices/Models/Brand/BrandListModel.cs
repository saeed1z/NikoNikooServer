using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class BrandListModel
    {
        public IList<BrandModel> BrandModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
