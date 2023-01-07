using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineServices.Models
{
    public class BannerModel
    {

        public Guid Id { get; set; }

        [Display(Name = " عنوان ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "نام فایل")]
        public string BannerFile { get; set; }

        [Display(Name = "ترتیب")]
        public int RowNum { get; set; }

        [Display(Name = "تصویر بنر")]
        public IFormFile BannerImage { get; set; }
    }
}
