using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OnlineServices.Entity
{
    public class ServiceRequestFile
    {

        public Guid Id { get; set; }

        [Display(Name = "نام فایل")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string RequestFileName { get; set; }

        [Display(Name = "پسوند فایل")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string RequestFileExtension { get; set; }

        [Display(Name = "شناسه درخواست")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceRequestId")]
        public Guid ServiceRequestId { get; set; }

        public virtual ServiceRequest ServiceRequest { get; set; }

    }
}
