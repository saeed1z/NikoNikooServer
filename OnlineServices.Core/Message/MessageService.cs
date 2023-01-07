using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class MessageService : IMessageService
    {
        private readonly OnlineServicesDbContext _db;
        public MessageService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(Message newMessage)
        {
            try
            {
                _db.Message.Add(newMessage);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(long MessageId)
        {
            var Message = GetById(MessageId);
            _db.Remove(Message);
            await _db.SaveChangesAsync();
        }

        public List<Message> GetAll(string Body = "", Guid? FromPersonId = null, Guid? ToPersonId = null) =>

            _db.Message.Where(x => (x.Body.Contains(Body) || Body == "")
            && (x.FromPersonId == FromPersonId || FromPersonId == null)
            && (x.ToPersonId == ToPersonId || ToPersonId == null)
            ).ToList();

        public List<Message> GetAllForApp(Guid ToPersonId, Guid? ServiceRequestId = null, DateTime? LastDateTime = null) =>

            _db.Message.Where(x => (x.ToPersonId == ToPersonId || x.FromPersonId == ToPersonId)
            && (x.ServiceRequestId == ServiceRequestId || ServiceRequestId == null)
            && (x.CreatedDate > LastDateTime || LastDateTime == null)
            ).OrderByDescending(o=>o.Id).ToList();

        public Message GetById(long MessageId) => _db.Message.Find(MessageId);

        public int GetAllMessageCount(Guid PersonId, Guid? ServiceRequestId = null) => 
            _db.Message.Where(x=> (x.ToPersonId == PersonId || x.FromPersonId == PersonId)
            && (x.ServiceRequestId == ServiceRequestId || ServiceRequestId == null)).Count();

        public IPagedList<Message> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.Message.ToList();
            return new PagedList<Message>(query, page, pageSize);

        }

        public IPagedList<Message> GetSupportAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.Message.ToList();
            return new PagedList<Message>(query, page, pageSize);

        }

        public void UpdateAsync(Message Message)
        {
            try
            {
                _db.Update(Message);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task UpdateAsync(long id)
        {
            throw new NotImplementedException();
        }
    }
}
