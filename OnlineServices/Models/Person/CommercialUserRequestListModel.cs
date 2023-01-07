using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class CommercialUserRequestListModel
    {
        public IList<CommercialUserRequestModel> CommercialUserRequestModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
