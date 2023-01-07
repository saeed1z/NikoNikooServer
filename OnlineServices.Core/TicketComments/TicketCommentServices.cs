using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;
using System.Net.Http;
using OnlineServices.Api.Helpers;

namespace OnlineServices.Core
{
    public class TicketCommentServices : ITicketCommentServices
    {

        #region :: Dependency ::

        private readonly OnlineServicesDbContext _db;

        private readonly ITicketServices _ticket;

        private readonly IPersonService _person;

        private readonly IConfiguration _configuration;

        public TicketCommentServices(OnlineServicesDbContext db, ITicketServices ticket, IPersonService person, IConfiguration configuration)
        {
            _db = db;
            _ticket = ticket;
            _person = person;
            _configuration = configuration;
        }

        #endregion

        public async Task<bool> DeleteAsync(object id)
          => await Task.FromResult(await DeleteAsync(await FindCommentAsync(id)));

        public async Task<TicketComments> FindCommentAsync(object id)
            => await Task.Run(async () => await _db.TicketComments.FindAsync(id));

        public async Task<IEnumerable<TicketComments>> GetAllCommentsAsync()
            => await Task.Run(async () => await _db.TicketComments.ToListAsync());

        public async Task<IEnumerable<TicketComments>> GetAllCommentsAsync(Expression<Func<TicketComments, bool>> where)
            => await Task.Run(async () => await _db.TicketComments.OrderBy(t=> t.SentDate).Where(where).ToListAsync());

        public async Task<IEnumerable<TicketComments>> GetAllCommentsAsync(Guid ticketId)
            => await Task.FromResult(await GetAllCommentsAsync(c => c.TicketId == ticketId));

        public async Task<bool> DeleteAsync(TicketComments ticketComments)
            => await Task.Run(async () =>
            {
                try
                {
                    _db.TicketComments.Remove(ticketComments);
                    return await SaveAsync();
                }
                catch
                {
                    return false;
                }
            });

        public async Task<bool> InsertAsync(TicketComments ticketComments)
            => await Task.Run(async () =>
            {
                try
                {
                    await _db.TicketComments.AddAsync(ticketComments);
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

        public async Task<bool> UpdateAsync(TicketComments ticketComments)
            => await Task.Run(async () =>
            {
                try
                {
                    _db.TicketComments.Update(ticketComments);
                    return await SaveAsync();
                }
                catch
                {
                    return false;
                }
            });

        public async Task<SendCommentResult> SendCommentAsync(CommentModel comment, IHeaderDictionary header)
            => await Task.Run(async () =>
            {
                SendCommentResult result = new SendCommentResult();
                try
                {
                    Ticket ticket = await _ticket.FindTicketAsync(comment.TicketId);
                    if (ticket != null)
                    {
                        Person person = await _person.GetPersonFromTokenAsync(header);
                        if (person != null)
                        {
                            if (ticket.PersonId == person.Id)
                            {
                                result.Status = await InsertAsync(await CreateCommentAsync(comment, person.Id, ticket.Id))
                                ? CommentStatus.Success : CommentStatus.Excetpion;
                                return result;
                            }
                            result.Status = CommentStatus.AccessDenied;
                            return result;
                        }
                        result.Status = CommentStatus.UserNotFound;
                        return result;
                    }
                    result.Status = CommentStatus.TicketNotFound;
                    return result;
                }
                catch
                {
                    result.Status = CommentStatus.Excetpion;
                    return result;
                }
            });

        private async Task<TicketComments> CreateCommentAsync(CommentModel comment, Guid personId, Guid ticketId)
            => new TicketComments
            {
                FromPersonId = personId,
                Message = string.IsNullOrEmpty(comment.Text) ? " " : comment.Text,
                IsRead = false,
                SentDate = DateTime.Now,
                TicketId = ticketId,
                ImageName = await SaveImageAsync(comment)
            };

        private async Task<string> SaveImageAsync(CommentModel comment)
            => await Task.Run(async () =>
            {
                if (!string.IsNullOrEmpty(comment.Base64))
                {
                    string apiUrl = $"{_configuration.GetValue<string>("HostAddress:Local")}/api/Comments/SaveImage";
                    HttpClient httpClient = new HttpClient();
                    string json = JsonConvert.SerializeObject(comment);
                    using StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                    HttpResponseMessage result = await httpClient.PostAsync(apiUrl, data);
                    ApiResult apiResult = await result.Content.ReadFromJsonAsync<ApiResult>();
                    return apiResult.ImageName;
                }
                return "null.png";
            });

        public async Task<GetCommentListResult> GetTicketComments(Guid ticketId, IHeaderDictionary header)
            => await Task.Run(async () =>
            {
                try
                {
                    Person person = await _person.GetPersonFromTokenAsync(header);
                    if (person != null)
                    {
                        Ticket ticket = await _ticket.FindTicketAsync(ticketId);
                        if (ticket != null)
                        {
                            if (ticket.PersonId == person.Id)
                                return new GetCommentListResult { Status = CommentStatus.Success, Comments = await GetCommentsViewModelAsync(person.Id, ticket.Id) };

                            return new GetCommentListResult { Status = CommentStatus.AccessDenied };
                        }
                        return new GetCommentListResult { Status = CommentStatus.TicketNotFound };
                    }
                    return new GetCommentListResult { Status = CommentStatus.UserNotFound };
                }
                catch
                {
                    return new GetCommentListResult { Status = CommentStatus.Excetpion };
                }
            });

        private async Task<IEnumerable<CommentsModel>> GetCommentsViewModelAsync(Guid personId, Guid ticketId)
            => await Task.Run(() =>
            {
                string apiUrl = $"{_configuration.GetValue<string>("HostAddress:MotaharUrl")}/Images/Tickets/Comments/";
                return GetAllCommentsAsync(ticketId).Result.Select(tc => new CommentsModel
                {
                    Date = tc.SentDate.ToShamsi(),
                    Id = tc.Id,
                    Text = tc.Message,
                    Image = (string.IsNullOrEmpty(tc.ImageName) || tc.ImageName.Trim() == "null.png") ? "" : apiUrl + tc.ImageName,
                    IsSender = tc.FromPersonId == personId
                });
            });

        internal class ApiResult
        {
            public int Status { get; set; }

            public string ImageName { get; set; }
        }
    }
}
