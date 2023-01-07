using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace OnlineServices.Controllers
{
    public class TicketController : Controller
    {

        #region --::Dependency::--

        private readonly ITicketServices _ticket;

        public TicketController(ITicketServices ticket)
        {
            _ticket = ticket;
        }

        #endregion

        public async Task<IActionResult> Index()
        {
            IEnumerable<TicketViewModel> tickets =  await _ticket.GetAllTicketViewModelAsync();
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> Ticket(Guid id)
        {
            return View();
        }
    }
}
