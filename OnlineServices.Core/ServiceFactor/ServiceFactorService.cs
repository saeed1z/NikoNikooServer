using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineServices.Entity;
using OnlineServices.Persistence;

namespace OnlineServices.Core
{
    public class ServiceFactorService : IServiceFactorService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceFactorService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public ServiceFactor GetById(Guid Id)
            => _db.ServiceFactor.Find(Id);

        public ServiceFactor GetByServiceRequestId(Guid ServiceRequestId)
            => _db.ServiceFactor.FirstOrDefault(x => x.ServiceRequestId == ServiceRequestId);

        public List<ServiceFactor> LoadData(Guid ServiceRequestId, bool? IsPaid, int SkipRow = 0, int pageSize = int.MaxValue)
            => _db.ServiceFactor.Where(x => x.ServiceRequestId == ServiceRequestId && (x.IsPaid == IsPaid || IsPaid == null)).Skip(SkipRow).Take(pageSize).ToList();


        public string Create(ServiceFactor newServiceFactor)
        {
            string result = "";
            try
            {
                _db.ServiceFactor.Add(newServiceFactor);
                _db.SaveChanges();
                result = newServiceFactor.Id.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }

        public void Update(ServiceFactor _ServiceFactor)
        {
            try
            {
                _db.Update(_ServiceFactor);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void Delete(System.Guid Id)
        {
            ServiceFactor _ServiceFactor = this.GetById(Id);
            _db.Remove(_ServiceFactor);
            _db.SaveChanges();
        }
    }
}
