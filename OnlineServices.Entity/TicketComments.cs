using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineServices.Entity
{
    public class TicketComments
    {
        public TicketComments()
        {

        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "متن پیام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Message { get; set; }

        [Display(Name = "تصویر پیام")]
        public string ImageName { get; set; }

        [Display(Name = "فرستنده پیام")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid FromPersonId { get; set; }

        [Display(Name = "گیرنده پیام")]
        public Guid? ToPersonId { get; set; }

        [ForeignKey("TicketId")]
        [Required]
        public Guid TicketId { get; set; }

        [Display(Name = "خوانده شده؟")]
        public bool IsRead { get; set; }

        [Display(Name = "تاریخ ارسال")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime SentDate { get; set; }

        //Navigation Properties
        public virtual Ticket Ticket { get; set; }
    }
}
