using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceFactorDetail
    {
        public ServiceFactorDetail()
        {
        }
        public System.Guid Id { get; set; }
        [Display(Name = " شناسه فاکتور سرویس")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceFactorId")]
        public System.Guid ServiceFactorId { get; set; }
        public virtual ServiceFactor ServiceFactor { get; set; }
        [Display(Name = " ردیف")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public byte Row { get; set; }
        [Display(Name = " عنوان کالا")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(100)")]
        public string ItemTitle { get; set; }
        [Display(Name = " تعداد")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public byte Quantity { get; set; }
        [Display(Name = " قیمت واحد")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal UnitFee { get; set; }

    }
}
