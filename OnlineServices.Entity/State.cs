using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OnlineServices.Entity
{
    public partial class State
    {
        public State()
        {
            City = new HashSet<City>();
            Person = new HashSet<Person>();
        }

        public byte Id { get; set; }
        [Display(Name = " نام استان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }
        [Display(Name = " توضیحات")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Description { get; set; }
        [Display(Name = " وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        [DefaultValue(true)]
        public bool IsActive { get; set; }

        public virtual ICollection<City> City { get; set; }
        public virtual ICollection<Person> Person { get; set; }
    }
}
