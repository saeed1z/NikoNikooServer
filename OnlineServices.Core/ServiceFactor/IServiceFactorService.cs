using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineServices.Entity;

namespace OnlineServices.Core
{
    public interface IServiceFactorService
    {
        ServiceFactor GetById(Guid Id);
        ServiceFactor GetByServiceRequestId(Guid ServiceRequestId);
        List<ServiceFactor> LoadData(Guid ServiceRequestId, bool? IsPaid, int SkipRow = 0, int pageSize = int.MaxValue);
        string Create(ServiceFactor newServiceFactor);
        void Update(ServiceFactor _ServiceFactor);
        void Delete(System.Guid Id);
    }
}
