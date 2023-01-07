using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IPersonPackageDetailService
    {
        List<PersonPackageDetail> GetAll();
        void Update(PersonPackageDetail PersonPackageDetail);
        List<PersonPackageDetail> GetAllByPersonPackageId(int PersonPackageId);
        PersonPackageDetail GetByPersonPackageIdAndServiceTypeId(int PersonPackageId, int ServiceTypeId);
        IPagedList<PersonPackageDetail> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
    }
}
