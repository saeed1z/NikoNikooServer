using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class Model
    {
        public int Id { get; set; }

        [ForeignKey("BrandId")]
        [Display(Name = "برند")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [ForeignKey("CarTypeBaseId")]
        [Display(Name = "نوع خودرو")]
        public int CarTypeBaseId { get; set; }
        public virtual Base CarTypeBase { get; set; }

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

        [Display(Name = "رزرو؟")]
        public bool IsReserved { get; set; }

        [Display(Name = "قیمت خودروی 0 کیلومتر")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }

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

        [Display(Name = "تصویر مدل")]
        [Column(TypeName = "varchar(42)")]
        public string ModelFile { get; set; }

    }
}
