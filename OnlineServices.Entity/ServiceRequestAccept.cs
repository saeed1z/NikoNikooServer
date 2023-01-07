using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceRequestAccept
    {
        public Guid Id { get; set; }

        [Display(Name = "شناسه درخواست سرویس")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceRequestId")]
        public Guid ServiceRequestId { get; set; }
        public virtual ServiceRequest ServiceRequest { get; set; }

        [Display(Name = "تاریخ و زمان پذیرش")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [Column(TypeName = "smalldatetime")]
        public DateTime AcceptDateTime { get; set; }

        [Display(Name = "شناسه کارمند")]
        public Guid? PersonId { get; set; }

        [Display(Name = "شناسه کارشناس")]
        public Guid? ExpertPersonId { get; set; }

    }
}
