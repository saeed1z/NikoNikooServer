using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Api.Models
{
    public class ModelGalleryModel
    {
        public string Id { get; set; }
        public int ModelId { get; set; }
        public string ModelTitle { get; set; }
        public string ModelEnTitle { get; set; }
        public string ModelCarTypeBaseTitle { get; set; }
        public int BrandId { get; set; }
        public string BrandTitle { get; set; }
        public string BrandEnTitle { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }

    }
}
