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
    public class ServiceTypeService : IServiceTypeService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceTypeService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        #region ServiceType
        public List<ServiceType> GetAll() => _db.ServiceType.Where(x => x.IsActive).ToList();
        public IPagedList<ServiceType> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.ServiceType.ToList();
            return new PagedList<ServiceType>(query, page, pageSize);

        }
        #endregion ServiceType

        #region ServiceTypeQuestion
        public IPagedList<ServiceTypeQuestion> GetServiceTypeQuestionPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.ServiceTypeQuestion.ToList();
            return new PagedList<ServiceTypeQuestion>(query, page, pageSize);

        }
        public ServiceTypeQuestion GetServiceTypeQuestionById(int ServiceTypeQuestionId) => _db.ServiceTypeQuestion.Find(ServiceTypeQuestionId);
        public List<ServiceTypeQuestion> GetAllServiceTypeQuestion(int ServiceTypeId) =>
            _db.ServiceTypeQuestion.Where(x => x.IsActive && (x.ServiceTypeId == ServiceTypeId || ServiceTypeId == 0)).OrderBy(x => x.OrderNo).ToList();
        public void CreateServiceTypeQuestion(ServiceTypeQuestion newServiceTypeQuestion)
        {
            try
            {
                _db.ServiceTypeQuestion.Add(newServiceTypeQuestion);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateServiceTypeQuestion(ServiceTypeQuestion ServiceTypeQuestion)
        {
            try
            {
                _db.Update(ServiceTypeQuestion);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task DeleteServiceTypeQuestion(int ServiceTypeQuestionId)
        {
            try
            {
                var serviceTypeQuestion = GetServiceTypeQuestionById(ServiceTypeQuestionId);
                serviceTypeQuestion.IsActive = false;   
                await _db.SaveChangesAsync();

            }
            catch(Exception ex)
            {

            }
        }
        #endregion ServiceTypeQuestion
    }
}
