using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class BrandModel
    {
        public int RowNum { get; set; }

        public int Id { get; set; }

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

        [Display(Name = "تصویر برند")]
        public string BrandFile { get; set; }

        [Display(Name = "تصویر برند")]
        public IFormFile BrandImage { get; set; }


    }
}
