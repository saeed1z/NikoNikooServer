using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineServices.Entity;

namespace OnlineServices.Core
{
    public interface IServiceFactorDetailService
    {
        ServiceFactorDetail GetById(Guid Id);
        List<ServiceFactorDetail> LoadData(Guid ServiceFactorId, int SkipRow = 0, int pageSize = int.MaxValue);
        string Create(ServiceFactorDetail newServiceFactorDetail);
        void Update(ServiceFactorDetail _ServiceFactorDetail);
        void Delete(System.Guid Id);
    }
}
