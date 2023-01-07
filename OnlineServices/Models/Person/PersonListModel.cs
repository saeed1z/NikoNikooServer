using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class PersonListModel
    {
        public IList<PersonModel> PersonModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
