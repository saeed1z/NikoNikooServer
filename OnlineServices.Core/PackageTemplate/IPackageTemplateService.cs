using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPackageTemplateService
    {
        PackageTemplate GetById(Guid Id);
        List<PackageTemplate> GetAll();
        IPagedList<PackageTemplate> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        void CreateAsync(PackageTemplate newPackageTemplate, List<PackageTemplateDetail> packageTemplateDetailList);
        void UpdateAsync(PackageTemplate PackageTemplate, List<PackageTemplateDetail> packageTemplateDetailList);
        Task Delete(Guid PackageTemplateId);
    }
}
