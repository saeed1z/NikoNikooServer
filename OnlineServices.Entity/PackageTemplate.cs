using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class PackageTemplate
    {
        public PackageTemplate()
        {
            PersonPackage = new HashSet<PersonPackage>();
            PackageTemplateDetail = new HashSet<PackageTemplateDetail>();
        }

        public Guid Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "شرح")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }

        [ForeignKey("PersonType")]
        [Display(Name = "نوع شخص")]
        public byte? PersonTypeId { get; set; }
        public virtual PersonType PersonType { get; set; }

        [Display(Name = "ارزش واقعی بسته(تومان)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(12, 0)")]
        public decimal RealPrice { get; set; }

        [Display(Name = "مبلغ قابل پرداخت(تومان)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(12, 0)")]
        public decimal Price { get; set; }

        [Display(Name = "مدت اعتبار بسته (روز)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public short ExpiredDuration { get; set; }

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

        public virtual ICollection<PersonPackage> PersonPackage { get; set; }
        public virtual ICollection<PackageTemplateDetail> PackageTemplateDetail { get; set; }

    }
}
