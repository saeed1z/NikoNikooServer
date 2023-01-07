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
using System.Transactions;

namespace OnlineServices.Core
{
    public class PackageTemplateService : IPackageTemplateService
    {
        private readonly OnlineServicesDbContext _db;
        public PackageTemplateService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public PackageTemplate GetById(Guid Id) => _db.PackageTemplate.Find(Id);
        public List<PackageTemplate> GetAll() => _db.PackageTemplate.Where(x => x.IsActive == true).ToList();
        public IPagedList<PackageTemplate> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.PackageTemplate.ToList();
            return new PagedList<PackageTemplate>(query, page, pageSize);

        }

        public void CreateAsync(PackageTemplate newPackageTemplate, List<PackageTemplateDetail> packageTemplateDetailList)
        {

            try
            {
                newPackageTemplate.PackageTemplateDetail = packageTemplateDetailList;
                _db.PackageTemplate.Add(newPackageTemplate);
                _db.SaveChanges();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateAsync(PackageTemplate PackageTemplate, List<PackageTemplateDetail> packageTemplateDetailList)
        {
            try
            {
                _db.Update(PackageTemplate);
                var packageDetailList = _db.PackageTemplateDetail.Where(x => x.PackageTemplateId == PackageTemplate.Id).ToList();
                if (packageDetailList.Count > 0)
                {
                    foreach (var item in packageDetailList)
                        _db.PackageTemplateDetail.Remove(item);
                }

                PackageTemplate.PackageTemplateDetail = packageTemplateDetailList;
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Delete(Guid PackageTemplateId)
        {
            var packageTemplate = GetById(PackageTemplateId);
            _db.Remove(packageTemplate);
            await _db.SaveChangesAsync();
        }
    }
}
