using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class PackageTemplateModel
    {
        public int RowNum { get; set; }

        public string Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }

        [Display(Name = "نوع شخص")]
        public byte? PersonTypeId { get; set; }
        public string PersonTypeTitle { get; set; }

        [Display(Name = "ارزش واقعی بسته(تومان)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public decimal RealPrice { get; set; }

        [Display(Name = "مبلغ قابل پرداخت(تومان)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public decimal Price { get; set; }

        [Display(Name = "مدت اعتبار بسته (روز)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public short ExpiredDuration { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
        public bool HasAccessToDelete { get; set; }

        public List<SelectListItem> PersonTypeList { get; set; }
        public List<PackageTemplateDetailModel> PackageTemplateDetailList { get; set; }
        public PackageTemplateModel()
        {
            this.PersonTypeList = new List<SelectListItem>();
            this.PackageTemplateDetailList = new List<PackageTemplateDetailModel>();
        }

    }
}
