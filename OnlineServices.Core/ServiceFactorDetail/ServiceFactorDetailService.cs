using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineServices.Entity;
using OnlineServices.Persistence;

namespace OnlineServices.Core
{
    public class ServiceFactorDetailService : IServiceFactorDetailService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceFactorDetailService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public ServiceFactorDetail GetById(Guid Id)
            => _db.ServiceFactorDetail.Find(Id);

        public List<ServiceFactorDetail> LoadData(Guid ServiceFactorId, int SkipRow = 0, int pageSize = int.MaxValue)
            => _db.ServiceFactorDetail.Where(x => x.ServiceFactorId == ServiceFactorId).Skip(SkipRow).Take(pageSize).OrderBy(x => x.Row).ToList();

        public string Create(ServiceFactorDetail newServiceFactorDetail)
        {
            string result = "";
            try
            {
                _db.ServiceFactorDetail.Add(newServiceFactorDetail);
                _db.SaveChanges();
                result = newServiceFactorDetail.Id.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return result;
        }

        public void Update(ServiceFactorDetail _ServiceFactorDetail)
        {
            try
            {
                _db.Update(_ServiceFactorDetail);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void Delete(System.Guid Id)
        {
            ServiceFactorDetail _ServiceFactorDetail = this.GetById(Id);
            _db.Remove(_ServiceFactorDetail);
            _db.SaveChanges();
        }
    }
}
