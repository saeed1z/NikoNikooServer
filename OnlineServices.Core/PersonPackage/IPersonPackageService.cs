using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPersonPackageService
    {
        PersonPackage GetById(int Id);
        List<PersonPackage> GetAll(Guid PersonId);
        List<PersonPackage> GetAllByPackageTemplateId(Guid PackageTemplateId);
        List<PersonPackage> GetAllByPersonId(Guid PersonId);
        void CreateAsync(PersonPackage newPersonPackage);
        void BuyPackage(PersonPackage newPersonPackage,ICollection<PackageTemplateDetail> packageTemplateDetailList);
        IPagedList<PersonPackage> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
    }
}
