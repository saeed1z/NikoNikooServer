using System;
using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Core
{
    public class TicketModel
    {
        [Display(Name = "عنوان تیکت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = "آیدی")]
        public Guid Id { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }
    }

    public class CommentModel
    {
        [Display(Name = "آیدی")]
        public Guid Id { get; set; }

        [Display(Name = "تیکت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public Guid TicketId { get; set; }

        [Display(Name = "متن")]
        public string Text { get; set; }

        [Display(Name = "فایل")]
        public string Base64 { get; set; }

        public string Extention { get; set; }
    }

    public class CommentsModel
    {
        [Display(Name = "آیدی")]
        public Guid Id { get; set; }

        [Display(Name = "تاریخ")]
        public string Date { get; set; }

        [Display(Name = "متن")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Text { get; set; }

        [Display(Name = "تصویر")]
        public string Image { get; set; }

        [Display(Name = "ارسال کننده")]
        public bool IsSender { get; set; }
    }
}
