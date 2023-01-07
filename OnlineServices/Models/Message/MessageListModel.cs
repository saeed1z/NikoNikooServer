using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineServices.Models
{
    public class MessageListModel
    {
        public IList<MessageModel> MessageModel { get; set; }
        public PagerModel PagerModel { get; set; }
    }
}
