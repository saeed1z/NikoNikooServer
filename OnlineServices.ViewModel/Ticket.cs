using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineServices.ViewModel
{
  public class TicketViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string CrateDate { get; set; }

        public string Status { get; set; }

        public object Person { get; set; }
    }
}
