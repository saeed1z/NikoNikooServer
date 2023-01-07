using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Entity
{
    public class Banner
    {
        public Guid Id { get; set; }

        [Display(Name = "شماره ردیف ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public int RowNum { get; set; }

        [Display(Name = "نام فایل")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string BannerFile { get; set; }


        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }


    }
}
