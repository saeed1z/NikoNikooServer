using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class ModelGalleryListModel
    {
        public IList<ModelGalleryModel> ModelGalleryModel { get; set; }
        public PagerModel PagerModel { get; set; }

    }
}
