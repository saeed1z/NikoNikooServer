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
    public class ServiceCenterService : IServiceCenterService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceCenterService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(ServiceCenter newServiceCenter)
        {
            try
            {
                _db.ServiceCenter.Add(newServiceCenter);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Guid ServiceCenterId)
        {
            var ServiceCenter = GetById(ServiceCenterId);
            _db.Remove(ServiceCenter);
            await _db.SaveChangesAsync();
        }

        public List<ServiceCenter> GetAll(bool? IsCarwash = null, bool? IsMechanic = null, bool? IsService = null, bool? IsAccessory = null) =>

            _db.ServiceCenter.Where(x => (x.IsCarwash == IsCarwash || IsCarwash == null)
            && (x.IsMechanic == IsMechanic || IsMechanic == null)
            && (x.IsService == IsService || IsService == null)
            && (x.IsAccessory == IsAccessory || IsAccessory == null)
            ).ToList();

        public ServiceCenter GetById(Guid ServiceCenterId) => _db.ServiceCenter.Find(ServiceCenterId);

        public IPagedList<ServiceCenter> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.ServiceCenter.ToList();
            return new PagedList<ServiceCenter>(query, page, pageSize);

        }

        public void UpdateAsync(ServiceCenter ServiceCenter)
        {
            try
            {
                _db.Update(ServiceCenter);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task UpdateAsync(int id)
        {
            throw new NotImplementedException();
        }

       
        public ServiceCenterDetail GetServiceCenterDetail(Guid ServiceCenterId, int BaseId) =>
            _db.ServiceCenterDetail.FirstOrDefault(x => x.ServiceCenterId == ServiceCenterId && x.ServiceDetailBaseId == BaseId);

    }
}
