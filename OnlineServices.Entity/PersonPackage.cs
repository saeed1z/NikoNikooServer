using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class PersonPackage
    {
        public PersonPackage()
        {
            PersonPackageDetail = new HashSet<PersonPackageDetail>();
        }
        public int Id { get; set; }

        [Display(Name = "شخص")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("PersonId")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Display(Name = "قالب بسته")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("PackageTemplateId")]
        public Guid PackageTemplateId { get; set; }
        public virtual PackageTemplate PackageTemplate { get; set; }

        [Display(Name = "شناسه ماشین")]
        public int? PersonCarId { get; set; }

        [Display(Name = "مبلغ پرداخت شده(ریال)")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(12, 0)")]
        public decimal Price { get; set; }

        [Display(Name = "تاریخ و ساعت خرید")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime FactorDate { get; set; }

        [Display(Name = "تاریخ و ساعت انقضای بسته")]
        public DateTime? ExpiredDate { get; set; }

        [Display(Name = "شماره فاکتور")]
        public int? FactorNumber { get; set; }

        [Display(Name = "شماره سند بانک")]
        public string BankDocument { get; set; }

        [Display(Name = "کد رهگیری بانک")]
        public string BankId { get; set; }

        [Display(Name = "تاریخ و ساعت پرداخت")]
        public DateTime? BankDocumentDate { get; set; }

        [Display(Name = "تاریخ ابطال")]
        public DateTime? CanceledDate { get; set; }

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

        public virtual ICollection<PersonPackageDetail> PersonPackageDetail { get; set; }
    }
}
