using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class Brand
    {
        public int Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "عنوان انگلیسی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string EnTitle { get; set; }

        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "کاربر ثبت کننده اطلاعات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid CreatedUserId { get; set; }

        [Display(Name = "تاریخ بروزرسانی اطلاعات")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "کاربر بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }

        [Display(Name = "تصویر برند")]
        [Column(TypeName = "varchar(42)")]
        public string BrandFile { get; set; }

    }
}
