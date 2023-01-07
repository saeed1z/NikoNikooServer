using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class TechnicalInfoModel
    {
        public int ModelId { get; set; }
        public string ModelTitle { get; set; }
        public string ModelEnTitle { get; set; }
        public string ModelCarTypeTitle { get; set; }
        public bool ModelIsActive { get; set; }
        public string ModelDescription { get; set; }
        public List<TechnicalInfoParametersModel> TechnicalInfoParametersModelList { get; set; }
        public TechnicalInfoModel()
        {
            this.TechnicalInfoParametersModelList = new List<TechnicalInfoParametersModel>();
        }
    }
}
