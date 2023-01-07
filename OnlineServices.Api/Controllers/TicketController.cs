using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using System;
using System.Threading.Tasks;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        #region --:: Dependency ::--

        private readonly ITicketServices _ticket;

        private readonly ITicketCommentServices _ticketComments;

        public TicketController(ITicketServices ticket, ITicketCommentServices ticketComment)
        {
            _ticket = ticket;
            _ticketComments = ticketComment;
        }

        #endregion

        [HttpPost]
        [Route("StartTicket")]
        public async Task<IActionResult> StartTicket(TicketModel startTicket)
            => await _ticket.StartTicketAsync(startTicket, Request.Headers) switch
            {
                StartTicketStatus.UserNotFound => Ok(new { errorId = 99, errorTitle = "کاربری یافت نشد", result = "" }),
                StartTicketStatus.Excetpion => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" }),
                StartTicketStatus.Success => Ok(new { errorId = 0, errorTitle = "تیکت شما ثبت شد", result = "" }),
                _ => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" })
            };

        [HttpGet]
        [Route("GetTicketList")]
        public async Task<IActionResult> GetTicketList()
        {
            GetTicketResult tickets = await _ticket.GetAllTicketsAsync(Request.Headers);
            return tickets.Status switch
            {
                GetTicketStatus.Success => Ok(new { errorId = 0, errorTitle = "موفق", result = tickets.Tikets }),
                GetTicketStatus.TicketNotFound => Ok(new { errorId = 100, errorTitle = "تیکتی ثبت نشده است", reulst = "" }),
                GetTicketStatus.UserNotFound => Ok(new { errorId = 99, errorTitle = "کاربری یافت نشد", result = "" }),
                GetTicketStatus.Excetpion => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" }),
                _ => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" })
            };
        }

        [HttpPost]
        [Route("SendComment")]
        public async Task<IActionResult> SendComment(CommentModel comment)
        {
            SendCommentResult sendResult = await _ticketComments.SendCommentAsync(comment, Request.Headers);

            return sendResult.Status switch
            {
                CommentStatus.Success => Ok(new { errorId = 0, errorTitle = "موفق", result = sendResult.Comment }),
                CommentStatus.AccessDenied => Ok(new { errorId = 101, errorTitle = "شما مجاز به ثبت پیام نیتسید", result = "" }),
                CommentStatus.TicketNotFound => Ok(new { errorId = 100, errorTitle = "تیکتی یافت است", result = "" }),
                CommentStatus.UserNotFound => Ok(new { errorId = 99, errorTitle = "کاربری یافت نشد", result = "" }),
                CommentStatus.Excetpion => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" }),
                _ => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" }),
            };
        }

        [HttpGet]
        [Route("GetCommentList")]
        public async Task<IActionResult> GetCommentList(Guid ticketId)
        {
            GetCommentListResult result = await _ticketComments.GetTicketComments(ticketId,Request.Headers);

            return result.Status switch
            {
                CommentStatus.Success => Ok(new { errorId = 0, errorTitle = "موفق", result = result.Comments }),
                CommentStatus.AccessDenied => Ok(new { errorId = 101, errorTitle = "شما مجاز به ثبت پیام نیتسید", result = "" }),
                CommentStatus.TicketNotFound => Ok(new { errorId = 100, errorTitle = "تیکتی یافت است", result = "" }),
                CommentStatus.UserNotFound => Ok(new { errorId = 99, errorTitle = "کاربری یافت نشد", result = "" }),
                CommentStatus.Excetpion => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" }),
                _ => Ok(new { errorId = 2, errorTitle = "خطایی رخ داد", result = "" }),
            };
        }

    }
}
