using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceFactor
    {
        public ServiceFactor()
        {
            ServiceFactorDetail = new HashSet<ServiceFactorDetail>();
            Message = new HashSet<Message>();
        }

        public System.Guid Id { get; set; }
        [Display(Name = " شناسه درخواست سرویس")]
        [ForeignKey("ServiceRequestId")]
        public Nullable<System.Guid> ServiceRequestId { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }
        [Display(Name = " تاریخ صدور")]
        [Column(TypeName = "datetime")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public System.DateTime IssueDate { get; set; }
        [Display(Name = " عنوان فروشگاه")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(100)")]
        public string SalesShop { get; set; }
        [Display(Name = " آدرس فروشگاه")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "nvarchar(1000)")]
        public string SalesShopAddress { get; set; }
        [Display(Name = " شماره تماس")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "varchar(15)")]
        public string SalesShopPhone { get; set; }
        [Display(Name = " جمع فاکتور پرداختی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "decimal(18, 0)")]
        public decimal TotalCost { get; set; }
        [Display(Name = " جمع تخفیف")]
        [Column(TypeName = "decimal(18, 0)")]
        public Nullable<decimal> DiscountFee { get; set; }
        [Display(Name = " جمع قابل پرداخت")]
        [Column(TypeName = "decimal(18, 0)")]
        public Nullable<decimal> FinalFee { get; set; }

        [Display(Name = " پرداخت شده")]
        public Nullable<bool> IsPaid { get; set; }

        [Display(Name = "تایید شده")]
        public Nullable<bool> IsAccepted { get; set; }

        public virtual ICollection<ServiceFactorDetail> ServiceFactorDetail { get; set; }
        public virtual ICollection<Message> Message { get; set; }

    }
}
