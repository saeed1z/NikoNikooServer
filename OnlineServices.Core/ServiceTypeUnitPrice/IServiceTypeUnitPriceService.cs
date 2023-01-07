using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IServiceTypeUnitPriceService
    {
        ServiceTypeUnitPrice GetById(int ServiceTypeUnitPriceId);
        ServiceTypeUnitPrice GetByServiceTypeId(int ServiceTypeId);
        void UpdateAsync(ServiceTypeUnitPrice ServiceTypeUnitPrice);
        void CreateAsync(ServiceTypeUnitPrice newServiceTypeUnitPrice);
        Task Delete(int ServiceTypeUnitPriceId);
        List<ServiceTypeUnitPrice> GetAll();
        IPagedList<ServiceTypeUnitPrice> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
    }
}
