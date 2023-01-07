using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public class Ticket
    {
        public Ticket()
        {

        }

        [Key]
        public Guid Id { get; set; }

        [Display(Name = "عنوان تیکت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid PersonId { get; set; }

        [Display(Name = "وضعیت تیکت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsOpen { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public DateTime StartDate { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<TicketComments> TicketCommecnts { get; set; }
    }
}
