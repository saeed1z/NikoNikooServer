using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ModelGallery
    {
        public Guid Id { get; set; }

        [ForeignKey("ModelId")]
        [Display(Name = "مدل")]
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "varchar(42)")]
        public string ImageName { get; set; }

        [Display(Name = "عنوان")]
        [Column(TypeName = "nvarchar(1000)")]
        public string Title { get; set; }

        [Display(Name = "شرح")]
        [Column(TypeName = "nvarchar(max)")]
        public string Description { get; set; }

    }
}
