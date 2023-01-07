using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class UserListModel
    {
        public IList<UserModel> UserModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
