using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IServiceCenterService
    {
        void CreateAsync(ServiceCenter newServiceCenter);
        ServiceCenter GetById(Guid ServiceCenterId);
        void UpdateAsync(ServiceCenter ServiceCenter);
        Task UpdateAsync(int id);
        Task Delete(Guid ServiceCenterId);
        List<ServiceCenter> GetAll(bool? IsCarwash = null, bool? IsMechanic = null, bool? IsService = null, bool? IsAccessory = null);
        IPagedList<ServiceCenter> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        ServiceCenterDetail GetServiceCenterDetail(Guid ServiceCenterId, int BaseId);
    }
}
