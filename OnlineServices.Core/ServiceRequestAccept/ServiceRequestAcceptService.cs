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
    public class ServiceRequestAcceptService : IServiceRequestAcceptService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceRequestAcceptService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void Create(ServiceRequestAccept newServiceRequestAccept)
        {
            try
            {
                _db.ServiceRequestAccept.Add(newServiceRequestAccept);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(ServiceRequestAccept ServiceRequestAccept)
        {
            try
            {
                _db.Update(ServiceRequestAccept);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ServiceRequestAccept GetById(Guid ServiceRequestAcceptId) => _db.ServiceRequestAccept.Find(ServiceRequestAcceptId);

    }
}
