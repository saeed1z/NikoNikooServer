using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Api.Helpers;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using OnlineServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class TicketServices : ITicketServices
    {
        private readonly OnlineServicesDbContext _db;

        private readonly IAspNetUserService _aspNetUser;

        private readonly IPersonService _person;

        public TicketServices(OnlineServicesDbContext db, IAspNetUserService aspNetUser, IPersonService person)
        {
            _db = db;
            _aspNetUser = aspNetUser;
            _person = person;
        }

        public async Task<bool> DeleteAsync(Ticket ticket)
          => await Task.Run(async () =>
          {
              try
              {
                  _db.Tickets.Remove(ticket);
                  return await SaveAsync();
              }
              catch
              {
                  return false;
              }
          });

        public async Task<bool> DeleteAsync(object Id)
            => await Task.FromResult(await DeleteAsync(await FindTicketAsync(Id)));

        public async Task<Ticket> FindTicketAsync(object Id)
            => await Task.Run(async () => await _db.Tickets.FindAsync(Id));

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync()
            => await Task.Run(async () => await _db.Tickets.ToListAsync());

        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(Expression<Func<Ticket, bool>> where)
            => await Task.Run(async () => await _db.Tickets.Where(where).ToListAsync());

        public async Task<GetTicketResult> GetAllTicketsAsync(IHeaderDictionary header)
            => await Task.Run(async () =>
            {
                GetTicketResult result = new GetTicketResult();
                try
                {
                    Person person = await _person.GetPersonFromTokenAsync(header);
                    if (person != null)
                    {
                        IEnumerable<Ticket> tickets = await GetAllTicketsAsync(t => t.PersonId == person.Id);
                        if (tickets != null && tickets.Any())
                        {
                            result.Status = GetTicketStatus.Success;
                            result.Tikets = tickets.Select(t => new TicketModel { Id = t.Id, Title = t.Title, Date = t.StartDate.ToShamsi() });
                            return result;
                        }
                        result.Status = GetTicketStatus.TicketNotFound;
                        return result;
                    }
                    result.Status = GetTicketStatus.UserNotFound;
                    return result;
                }
                catch
                {
                    result.Status = GetTicketStatus.Excetpion;
                    return result;
                }
            });

        public async Task<IEnumerable<TicketViewModel>> GetAllTicketViewModelAsync()
        {
            var tickets = await GetAllTicketsAsync();
            return tickets.Select((ticket) =>
            {
                return new TicketViewModel
                {
                    CrateDate = ticket.StartDate.ToShamsi(),
                    Id = ticket.Id,
                    Person = ticket.Person,
                    Status = (ticket.IsOpen ? "باز" : "بسته"),
                    Title = ticket.Title
                };
            });
        }

        public async Task<bool> InsertAsync(Ticket ticket)
            => await Task.Run(async () =>
            {
                try
                {
                    await _db.Tickets.AddAsync(ticket);
                    return await SaveAsync();
                }
                catch
                {
                    return false;
                }
            });

        public async Task<bool> SaveAsync()
            => await Task.Run(async () =>
            {
                try
                {
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            });

        public async Task<StartTicketStatus> StartTicketAsync(TicketModel ticketModel, IHeaderDictionary header)
            => await Task.Run(async () =>
            {
                Person person = await _person.GetPersonFromTokenAsync(header);
                if (person != null)
                    return await InsertAsync(new Ticket
                    {
                        IsOpen = true,
                        PersonId = person.Id,
                        StartDate = DateTime.Now,
                        Title = ticketModel.Title
                    }) ? StartTicketStatus.Success : StartTicketStatus.Excetpion;

                return StartTicketStatus.UserNotFound;
            });

        public async Task<bool> UpdateAsync(Ticket ticket)
            => await Task.Run(async () =>
            {
                try
                {
                    _db.Tickets.Update(ticket);
                    return await SaveAsync();
                }
                catch
                {
                    return false;
                }
            });
    }
}
