using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class CommercialUserRequest
    {
        public int Id { get; set; }
        [Display(Name = "شخص")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("PersonId")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }
        public string CompanyName { get; set; }
        public string RegistrationNumber { get; set; }
        public string RegistrationDate { get; set; }
        public string RegistrationPlace { get; set; }
        public string EconomicCode { get; set; }
        public string WebsiteUrl { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string ActivityAddress { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceFamily { get; set; }
        public string Post { get; set; }
        public bool IsRejected { get; set; }
        public string RejectedReason { get; set; }
        public bool IsAccepted { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "کاربر ثبت کننده اطلاعات")]
        public Guid? CreatedUserId { get; set; }

        [Display(Name = "تاریخ بروزرسانی اطلاعات")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "کاربر بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }
    }
}
