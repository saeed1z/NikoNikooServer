using Microsoft.AspNetCore.Http;
using OnlineServices.Entity;
using OnlineServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface ITicketServices
    {
        Task<IEnumerable<Ticket>> GetAllTicketsAsync();

        Task<IEnumerable<TicketViewModel>> GetAllTicketViewModelAsync();

        Task<IEnumerable<Ticket>> GetAllTicketsAsync(Expression<Func<Ticket, bool>> where);

        Task<GetTicketResult> GetAllTicketsAsync(IHeaderDictionary header);

        Task<StartTicketStatus> StartTicketAsync(TicketModel ticketModel, IHeaderDictionary header);

        Task<Ticket> FindTicketAsync(object Id);

        Task<bool> InsertAsync(Ticket ticket);

        Task<bool> UpdateAsync(Ticket ticket);

        Task<bool> DeleteAsync(Ticket ticket);

        Task<bool> DeleteAsync(object Id);

        Task<bool> SaveAsync();
    }
}
