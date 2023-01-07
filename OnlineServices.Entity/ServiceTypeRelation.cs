using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceTypeRelation
    {
        public short Id { get; set; }

        [Display(Name = "شناسه نوع سرویس")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceTypeId")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [Display(Name = "شناسه نوع سرویس مرتبط")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("RelatedServiceTypeId")]
        public int RelatedServiceTypeId { get; set; }
        public virtual ServiceType RelatedServiceType { get; set; }

        [Display(Name = "توضیحات")]
        [Column(TypeName = "nvarchar(2000)")]
        public string Description { get; set; }

        [Display(Name = "وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }

        [Display(Name = "ترتیب نمایش")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public short OrderNo { get; set; }

    }
}
