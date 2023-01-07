using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ServiceRequest
    {
        public ServiceRequest()
        {
            
             ServiceRequestFile = new HashSet<ServiceRequestFile>();
            
        }
        public Guid Id { get; set; }

        [Display(Name = "شخص")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("PersonId")]
        public Guid PersonId { get; set; }
        public virtual Person Person { get; set; }

        [Display(Name = "تاریخ و ساعت درخواست")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime RequestDateTime { get; set; }

        [Display(Name = "نوع خدمت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ServiceTypeId")]
        public int ServiceTypeId { get; set; }
        public virtual ServiceType ServiceType { get; set; }

        [Display(Name = "خودرو")]
        [ForeignKey("PersonCarId")]
        public int? PersonCarId { get; set; }
        public virtual PersonCar PersonCar { get; set; }

        [Display(Name = "استان مبدا")]
        [ForeignKey("SourceStateId")]
        public byte? SourceStateId { get; set; }
        public virtual State SourceState { get; set; }

        [Display(Name = "شهر مبدا")]
        [ForeignKey("SourceCityId")]
        public int? SourceCityId { get; set; }
        public virtual City SourceCity { get; set; }

        [Display(Name = "آدرس مبدا")]
        public string SourceAddress { get; set; }

        [Display(Name = "موقعیت مبدا")]
        public string SourceLocation { get; set; }

        [Display(Name = "استان مقصد")]
        [ForeignKey("DestinationStateId")]
        public byte? DestinationStateId { get; set; }
        public virtual State DestinationState { get; set; }

        [Display(Name = "شهر مقصد")]
        [ForeignKey("DestinationCityId")]
        public int? DestinationCityId { get; set; }
        public virtual City DestinationCity { get; set; }

        [Display(Name = "آدرس مقصد")]
        public string DestinationAddress { get; set; }

        [Display(Name = "موقعیت مقصد")]
        public string DestinationLocation { get; set; }

        [Display(Name = "موقعیت مقصد")]
        public string Description { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        [Display(Name = "تاریخ ثبت اطلاعات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "کاربر ثبت کننده اطلاعات")]
        //[Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid CreatedUserId { get; set; }

        [Display(Name = "تاریخ بروزرسانی اطلاعات")]
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "کاربر بروزرسانی کننده اطلاعات")]
        public Guid? UpdatedUserId { get; set; }

        [Display(Name = "آخرین وضعیت")]
        [ForeignKey("LastStatusId")]
        public byte? LastStatusId { get; set; }
        public virtual Status LastStatus { get; set; }

        [Display(Name = "برند")]
        [ForeignKey("BrandId")]
        public int? BrandId { get; set; }
        public virtual Brand Brand { get; set; }

        [Display(Name = "مدل")]
        [ForeignKey("ModelId")]
        public int? ModelId { get; set; }
        public virtual Model Model { get; set; }

        [Display(Name = "شناسه کارمند")]
        [ForeignKey("EmployeeId")]
        public Guid? EmployeeId { get; set; }
        public virtual Person Employee { get; set; }

        [Display(Name = "شناسه کارشناس")]
        [ForeignKey("ExpertId")]
        public Guid? ExpertId { get; set; }
        public virtual Person Expert { get; set; }


        [Display(Name = "شناسه والد درخواست")]
        [ForeignKey("ParentServiceRequestId")]
        public Guid? ParentServiceRequestId { get; set; }
        public virtual ServiceRequest ParentServiceRequest { get; set; }

        public virtual ICollection<ServiceRequestFile> ServiceRequestFile { get; set; }

        public virtual ICollection<ServiceFactor> ServiceFactor { get; set; }
    }
}
