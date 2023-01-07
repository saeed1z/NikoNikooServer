using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class PersonCar
    {
        public int Id { get; set; }

        [Display(Name = "شناسه شخص")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("PersonId")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Display(Name = "مدل")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ModelId")]
        public int ModelId { get; set; }
        public virtual Model Model { get; set; }

        [Display(Name = "شماره پلاک")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string PlaqueNo { get; set; }

        [Display(Name = "شماره شاسی")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string ChassisNo { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }

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

    }
}
