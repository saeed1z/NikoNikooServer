using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class PackageTemplateDetail
    {
        public int Id { get; set; }

        [ForeignKey("PackageTemplateId")]
        [Display(Name = "قالب بسته")]
        public Guid PackageTemplateId { get; set; }
        public virtual PackageTemplate PackageTemplate { get; set; }

        [ForeignKey("ServiceTypeId")]
        [Display(Name = "نوع خدمت")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }
        
        [Display(Name = "تعداد مرتبه استفاده از سرویس")]
        public short Quantity { get; set; }
    }
}
