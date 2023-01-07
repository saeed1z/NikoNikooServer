using OnlineServices.Entity;
using System.Collections.Generic;

namespace OnlineServices.Core
{
    public class GetTicketResult
    {
        public IEnumerable<TicketModel> Tikets { get; set; }

        public GetTicketStatus Status { get; set; }
    }

    public class SendCommentResult
    {
        public CommentModel Comment { get; set; }

        public CommentStatus Status { get; set; }
    }

    public class GetCommentListResult
    {
        public IEnumerable<CommentsModel> Comments { get; set; }

        public CommentStatus Status { get; set; }
    }

    public enum GetTicketStatus
    {
        UserNotFound,
        TicketNotFound,
        Success,
        Excetpion
    }

    public enum StartTicketStatus
    {
        UserNotFound,
        Success,
        Excetpion
    }

    public enum CommentStatus
    {
        UserNotFound,
        TicketNotFound,
        AccessDenied,
        Success,
        Excetpion
    }
}
