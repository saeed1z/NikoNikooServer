using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class RefrencePersonListModel
    {
        public IList<RefrencePersonModel> ExpertPersonModel { get; set; }
        public IList<RefrencePersonModel> EmployeePersonModel { get; set; }
        public PagerModel ExpertPagerModel { get; set; }
        public PagerModel EmployeePagerModel { get; set; }
        public ServiceRequest ServiceRequest { get; set; }
    }
}
