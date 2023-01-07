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
    public class PersonPackageService : IPersonPackageService
    {
        private readonly OnlineServicesDbContext _db;
        public PersonPackageService(OnlineServicesDbContext db)
        {
            _db = db;
        }
        public PersonPackage GetById(int Id) => _db.PersonPackage.Find(Id);
        public List<PersonPackage> GetAllByPersonId(Guid PersonId) =>
            _db.PersonPackage.Where(x => x.PersonId == PersonId && x.ExpiredDate > DateTime.Now).OrderBy(x => x.ExpiredDate).ToList();
        public List<PersonPackage> GetAll(Guid PersonId) => _db.PersonPackage.Where(x => x.PersonId == PersonId && (x.ExpiredDate > DateTime.Now)).ToList();
        public List<PersonPackage> GetAllByPackageTemplateId(Guid PackageTemplateId) => _db.PersonPackage.Where(x => x.PackageTemplateId == PackageTemplateId).ToList();
        public IPagedList<PersonPackage> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.PersonPackage.ToList();
            return new PagedList<PersonPackage>(query, page, pageSize);

        }

        public void CreateAsync(PersonPackage newPersonPackage)
        {
            try
            {
                _db.PersonPackage.Add(newPersonPackage);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void BuyPackage(PersonPackage newPersonPackage, ICollection<PackageTemplateDetail> packageTemplateDetailList)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _db.PersonPackage.Add(newPersonPackage);
                    _db.SaveChanges();

                    if (packageTemplateDetailList.Count > 0)
                    {
                        foreach (var item in packageTemplateDetailList)
                        {
                            var objPersonPackageDetail = new PersonPackageDetail();
                            objPersonPackageDetail.PersonPackageId = newPersonPackage.Id;
                            objPersonPackageDetail.Quantity = item.Quantity;
                            objPersonPackageDetail.UsedQuantity = 0;
                            objPersonPackageDetail.ServiceTypeId = item.ServiceTypeId;
                            _db.PersonPackageDetail.Add(objPersonPackageDetail);
                        }
                    }
                    _db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                }
            }
        }

    }
}
