using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineServices.Models
{
    public class ServiceTypeUnitPriceModel
    {
        public int RowNum { get; set; }
        public int Id { get; set; }

        [Display(Name = "نوع خدمت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceTypeId")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        public string ServiceTypeTitle { get; set; }

        [Display(Name = "قیمت خودروی 0 کیلومتر")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

      
        [Display(Name = " تاریخ ثبت ")]
        [Column(TypeName = "datetime")]
        public string CreatedDate { get; set; }
        [Display(Name = "نام ثبت کننده اطلاعات ")]
        public string CreatedUserId { get; set; }
        [Display(Name = " تاریخ بروزرسانی ثبت ")]
        [Column(TypeName = "datetime")]
        public string UpdatedDate { get; set; }
        [Display(Name = " بروزرسانی نام ثبت کننده اطلاعات")]
        public string UpdatedUserId { get; set; }
        public List<SelectListItem> ServiceTypeList { get; set; }
        public ServiceTypeUnitPriceModel()
        {
            this.ServiceTypeList = new List<SelectListItem>();
        }

    }
}
