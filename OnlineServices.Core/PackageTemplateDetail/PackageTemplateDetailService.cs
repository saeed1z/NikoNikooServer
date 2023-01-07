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
    public class PackageTemplateDetailService : IPackageTemplateDetailService
    {
        private readonly OnlineServicesDbContext _db;
        public PackageTemplateDetailService(OnlineServicesDbContext db)
        {
            _db = db;
        }
        public List<PackageTemplateDetail> GetAll() => _db.PackageTemplateDetail.ToList();
        public List<PackageTemplateDetail> GetAllByPackageTemplateId(Guid PackageTemplateId) => _db.PackageTemplateDetail.Where(x => x.PackageTemplateId == PackageTemplateId).ToList();
        public PackageTemplateDetail GetByPackageTemplateIdAndServiceTypeId(Guid PackageTemplateId, int ServiceTypeId) =>
            _db.PackageTemplateDetail.Where(x => x.PackageTemplateId == PackageTemplateId && x.ServiceTypeId == ServiceTypeId).FirstOrDefault();
        public IPagedList<PackageTemplateDetail> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.PackageTemplateDetail.ToList();
            return new PagedList<PackageTemplateDetail>(query, page, pageSize);

        }

    }
}
