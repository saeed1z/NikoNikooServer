using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineServices.Core
{
    public class CommercialUserRequestServices : ICommercialUserRequestServices
    {
        private readonly OnlineServicesDbContext _db;
        public CommercialUserRequestServices(OnlineServicesDbContext db)
        {
            _db = db;
        }


        public void Create(CommercialUserRequest NewCommercialUserRequest)
        {
            try
            {
                _db.CommercialUserRequest.Add(NewCommercialUserRequest);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(CommercialUserRequest CommercialUserRequest)
        {
            try
            {
                _db.Update(CommercialUserRequest);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CommercialUserRequest GetById(int CommercialUserRequestId) =>
            _db.CommercialUserRequest.Find(CommercialUserRequestId);

        public CommercialUserRequest GetByPersonId(Guid PersonId) => 
            _db.CommercialUserRequest.Where(x => x.PersonId == PersonId).OrderByDescending(t => t.CreatedDate).FirstOrDefault();

        public CommercialUserRequest GetByUnCheckRequest(Guid PersonId) =>
            _db.CommercialUserRequest.FirstOrDefault(x => x.PersonId == PersonId && !x.IsAccepted && !x.IsRejected);

        public IPagedList<CommercialUserRequest> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.CommercialUserRequest.OrderByDescending(x=>x.CreatedDate).ToList();
            return new PagedList<CommercialUserRequest>(query, page, pageSize);

        }
    }
}
