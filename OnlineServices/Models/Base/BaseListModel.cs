using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class BaseListModel
    {
        public IList<BaseModel> BaseModel { get; set; }
        public PagerModel PagerModel { get; set; }
        public int BaseKindId { get; set; }
    }
}
