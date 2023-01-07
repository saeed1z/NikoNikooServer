using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class BaseModel
    {
        public int RowNum { get; set; }
        public int Id { get; set; }

        [Display(Name = " سرفصل اطلاعات  ")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public short BaseKindId { get; set; }
        public string BaseKindTitle { get; set; }

        [Display(Name = " عنوان")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public string Title { get; set; }

        [Display(Name = " وضعیت")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public bool IsActive { get; set; }

        [Display(Name = " ترتیب نمایش")]
        [Required(ErrorMessage = "لطفا فیلد {0} را پر کنید")]
        public short OrderNo { get; set; }

        [Display(Name = " توضیحات")]
        public string Description { get; set; }

        public List<SelectListItem> BaseKindList { get; set; }
        public BaseModel()
        {
            this.BaseKindList = new List<SelectListItem>();
        }

    }
}
