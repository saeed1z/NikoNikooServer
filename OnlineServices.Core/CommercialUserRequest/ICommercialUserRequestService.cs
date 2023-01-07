using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface ICommercialUserRequestServices
    {
        void Create(CommercialUserRequest NewCommercialUserRequest);
        void Update(CommercialUserRequest CommercialUserRequest);
        CommercialUserRequest GetById(int CommercialUserRequestId);
        CommercialUserRequest GetByPersonId(Guid PersonId);
        CommercialUserRequest GetByUnCheckRequest(Guid PersonId);
        IPagedList<CommercialUserRequest> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
    }
}
