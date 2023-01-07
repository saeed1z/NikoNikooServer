using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public partial class ConfirmMap
    {
        public short Id { get; set; }

        [Display(Name = "شناسه موضوع مرجع")]
        [ForeignKey("SourceBaseKindId")]
        public short? SourceBaseKindId { get; set; }
        public virtual BaseKind SourceBaseKind { get; set; }

        [Display(Name = "شناسه موضوع")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("BaseKindId")]
        public short BaseKindId { get; set; }
        public virtual BaseKind BaseKind { get; set; }

        [Display(Name = "وضعیت اولیه")]
        [ForeignKey("FromStatusId")]
        public byte? FromStatusId { get; set; }
        public virtual Status FromStatus { get; set; }

        [Display(Name = "وضعیت ثانویه")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [ForeignKey("ToStatusId")]
        public byte ToStatusId { get; set; }
        public virtual Status ToStatus { get; set; }

        [Display(Name = "مسیر همزمان")]
        public bool HasConcurrent { get; set; }

        [Display(Name = "وضعیت نهایی؟")]
        public bool IsFinal { get; set; }

    }
}
