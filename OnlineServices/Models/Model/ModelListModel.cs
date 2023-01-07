using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ModelListModel
    {
        public IList<ModelModel> ModelModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
