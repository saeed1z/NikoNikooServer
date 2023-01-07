using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPackageTemplateDetailService
    {
        List<PackageTemplateDetail> GetAll();
        List<PackageTemplateDetail> GetAllByPackageTemplateId(Guid PackageTemplateId);
        PackageTemplateDetail GetByPackageTemplateIdAndServiceTypeId(Guid PackageTemplateId,int ServiceTypeId);
        IPagedList<PackageTemplateDetail> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
    }
}
