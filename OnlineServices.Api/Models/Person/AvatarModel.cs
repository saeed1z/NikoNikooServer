using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Api.Models
{
    public partial class AvatarModel
    {
        public string Image { get; set; }
        public string ImageExtension { get; set; }

    }
}
