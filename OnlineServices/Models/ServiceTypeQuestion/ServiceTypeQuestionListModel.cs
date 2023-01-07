using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceTypeQuestionListModel
    {
        public IList<ServiceTypeQuestionModel> ServiceTypeQuestionModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
