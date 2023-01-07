using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class BaseKind
    {
        public short Id { get; set; }

        [Display(Name = "شناسه والد صفحات")]
        public short? ParentBaseKindId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public short OrderNo { get; set; }

        [Column(TypeName = "nvarchar(2000)")]
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

    }
}
