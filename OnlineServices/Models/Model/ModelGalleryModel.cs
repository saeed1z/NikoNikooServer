using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ModelGalleryModel
    {
        public int RowNum { get; set; }
        public string Id { get; set; }
        [Display(Name = "شناسه مدل")]
        public int ModelId { get; set; }
        [Display(Name = "عنوان مدل")]
        public string ModelTitle { get; set; }

        [Display(Name = "عنوان انگلیسی مدل")]
        public string ModelEnTitle { get; set; }
        public string ModelCarTypeBaseTitle { get; set; }

        [Display(Name = "تصویر")]
        public string ImageName { get; set; }
        [Display(Name = "تصویر")]
        public IFormFile ImageNameImage { get; set; }

        [Display(Name = "عنوان")]
        public string Title { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

    }
}
