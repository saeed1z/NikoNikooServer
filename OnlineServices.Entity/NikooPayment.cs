using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public class NikooPayment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int CourseId { get; set; }

        [Required]
        public string UserMobile { get; set; }

        [Required]
        public double Price { get; set; }

        public bool IsFinally { get; set; }

        public string? RefId { get; set; }

    }
}
