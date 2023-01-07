using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ModelModel
    {
        public int RowNum { get; set; }

        public int Id { get; set; }
        
        [Display(Name = "برند")]
        [Required(ErrorMessage = "لطفا فیلد {0} را انتخاب کنید")]
        public int BrandId { get; set; }
        public string BrandTitle { get; set; }
        public string BrandEnTitle { get; set; }
        public int CarTypeBaseId { get; set; }
        public string CarTypeBaseTitle { get; set; }

        [Display(Name = " عنوان ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = " عنوان انگلیسی ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string EnTitle { get; set; }

        [Display(Name = " توضیحات ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }

        [Display(Name = " وضعیت ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "قیمت خودروی 0 کیلومتر")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public decimal Price { get; set; }


        [Display(Name = "تصویر مدل")]
        public string ModelFile { get; set; }

        [Display(Name = "تصویر مدل")]
        public IFormFile ModelImage { get; set; }

        public List<SelectListItem> BrandList { get; set; }
        public List<SelectListItem> CarTypeList { get; set; }

        public ModelModel()
        {
            this.BrandList = new List<SelectListItem>();
            this.CarTypeList = new List<SelectListItem>();
        }

    }
}
