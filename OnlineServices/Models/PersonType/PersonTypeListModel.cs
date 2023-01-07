using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class PersonTypeListModel
    {
        public IList<PersonTypeModel> PersonTypeModel { get; set; }
        public PagerModel PagerModel { get; set; }

    }
}
