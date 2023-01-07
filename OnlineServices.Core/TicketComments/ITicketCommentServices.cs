using Microsoft.AspNetCore.Http;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface ITicketCommentServices
    {
        Task<IEnumerable<TicketComments>> GetAllCommentsAsync();

        Task<IEnumerable<TicketComments>> GetAllCommentsAsync(Expression<Func<TicketComments,bool>> where);

        Task<IEnumerable<TicketComments>> GetAllCommentsAsync(Guid ticketId);

        Task<TicketComments> FindCommentAsync(object id);

        Task<SendCommentResult> SendCommentAsync(CommentModel comment,IHeaderDictionary header);

        Task<GetCommentListResult> GetTicketComments(Guid ticketId, IHeaderDictionary header);

        Task<bool> InsertAsync(TicketComments ticketComments);

        Task<bool> UpdateAsync(TicketComments ticketComments);

        Task<bool> DeleteAsync(TicketComments ticketComments);

        Task<bool> DeleteAsync(object id);

        Task<bool> SaveAsync();

    }
}
