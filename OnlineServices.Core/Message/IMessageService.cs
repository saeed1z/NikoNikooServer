using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IMessageService
    {
        void CreateAsync(Message newMessage);
        Message GetById(long MessageId);
        int GetAllMessageCount(Guid PersonId, Guid? ServiceRequestId = null);
        void UpdateAsync(Message Message);
        Task UpdateAsync(long id);
        Task Delete(long MessageId);
        List<Message> GetAll(string Body = "", Guid? FromPersonId = null, Guid? ToPersonId = null);
        List<Message> GetAllForApp(Guid ToPersonId, Guid? ServiceRequestId = null, DateTime? LastDateTime = null);
        IPagedList<Message> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        IPagedList<Message> GetSupportAll(int page = 0, int pageSize = int.MaxValue);

    }
}
