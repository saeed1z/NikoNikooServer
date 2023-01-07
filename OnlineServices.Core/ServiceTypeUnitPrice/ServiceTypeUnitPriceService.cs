using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineServices.Entity;
using OnlineServices.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public class ServiceTypeUnitPriceService : IServiceTypeUnitPriceService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceTypeUnitPriceService(OnlineServicesDbContext db)
        {
            _db = db;
        }
        public ServiceTypeUnitPrice GetById(int ServiceTypeUnitPriceId) => _db.ServiceTypeUnitPrice.Find(ServiceTypeUnitPriceId);
        public ServiceTypeUnitPrice GetByServiceTypeId(int ServiceTypeId) =>
            _db.ServiceTypeUnitPrice.Where(x => x.ServiceTypeId == ServiceTypeId)
            .OrderByDescending(x => x.CreatedDate).FirstOrDefault();
        public List<ServiceTypeUnitPrice> GetAll() => _db.ServiceTypeUnitPrice.ToList();
        public IPagedList<ServiceTypeUnitPrice> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.ServiceTypeUnitPrice.ToList();
            return new PagedList<ServiceTypeUnitPrice>(query, page, pageSize);

        }
        public void UpdateAsync(ServiceTypeUnitPrice ServiceTypeUnitPrice)
        {
            try
            {
                _db.Update(ServiceTypeUnitPrice);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CreateAsync(ServiceTypeUnitPrice newServiceTypeUnitPrice)
        {
            try
            {
                _db.ServiceTypeUnitPrice.Add(newServiceTypeUnitPrice);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(int ServiceTypeUnitPriceId)
        {
            var serviceTypeUnitPrice = GetById(ServiceTypeUnitPriceId);
            _db.Remove(serviceTypeUnitPrice);
            await _db.SaveChangesAsync();
        }

    }
}
