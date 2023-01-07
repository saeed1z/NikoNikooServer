using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public partial class Status
    {
        public byte Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }
    }
}
