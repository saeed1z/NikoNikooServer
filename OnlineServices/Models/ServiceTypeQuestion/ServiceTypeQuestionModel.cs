using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceTypeQuestionModel
    {
        public int RowNum { get; set; }

        public int Id { get; set; }

        [Display(Name = "نوع خدمت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public int ServiceTypeId { get; set; }
        public string ServiceTypeTitle { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public byte OrderNo { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }

        public List<SelectListItem> ServiceTypeList { get; set; }

        public ServiceTypeQuestionModel()
        {
            this.ServiceTypeList = new List<SelectListItem>();
        }


    }
}
