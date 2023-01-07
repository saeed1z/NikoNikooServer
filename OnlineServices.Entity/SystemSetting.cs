using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class SystemSetting
    {
        public int Id { get; set; }

        [Display(Name = "کد")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(50)")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        [Display(Name = "مقدار")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(100)")]
        public string Value { get; set; }
    }
}
