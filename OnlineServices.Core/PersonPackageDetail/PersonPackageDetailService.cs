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
    public class PersonPackageDetailService : IPersonPackageDetailService
    {
        private readonly OnlineServicesDbContext _db;
        public PersonPackageDetailService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void Update(PersonPackageDetail PersonPackageDetail)
        {
            try
            {
                _db.Update(PersonPackageDetail);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<PersonPackageDetail> GetAll() => _db.PersonPackageDetail.ToList();
        public List<PersonPackageDetail> GetAllByPersonPackageId(int PersonPackageId) => _db.PersonPackageDetail.Where(x => x.PersonPackageId == PersonPackageId).ToList();


        public PersonPackageDetail GetByPersonPackageIdAndServiceTypeId(int PersonPackageId, int ServiceTypeId) => 
            _db.PersonPackageDetail.Where(x => x.PersonPackageId == PersonPackageId && x.ServiceTypeId == ServiceTypeId).FirstOrDefault();

        public IPagedList<PersonPackageDetail> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.PersonPackageDetail.ToList();
            return new PagedList<PersonPackageDetail>(query, page, pageSize);

        }

    }
}
