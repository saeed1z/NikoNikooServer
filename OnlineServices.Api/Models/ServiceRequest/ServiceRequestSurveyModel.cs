using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ServiceRequestSurveyModel
    {
        public string Id { get; set; }
        public string ServiceRequestId { get; set; }
        public int ServiceTypeQuestionId { get; set; }
        public int ServiceTypeQuestionTitle { get; set; }
        public byte Score { get; set; }
        public string Description { get; set; }

    }
}
