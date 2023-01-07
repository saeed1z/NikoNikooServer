using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class PersonPackageDetail
    {
        public int Id { get; set; }

        [ForeignKey("PersonPackageId")]
        [Display(Name = "بسته خریداری شده")]
        public int PersonPackageId { get; set; }
        public virtual PersonPackage PersonPackage { get; set; }

        [ForeignKey("ServiceTypeId")]
        [Display(Name = "نوع خدمت")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [Display(Name = "تعداد مرتبه استفاده از سرویس")]
        public short Quantity { get; set; }

        [Display(Name = "تعداد مرتبه استفاده شده از سرویس")]
        public short UsedQuantity { get; set; }

    }
}
