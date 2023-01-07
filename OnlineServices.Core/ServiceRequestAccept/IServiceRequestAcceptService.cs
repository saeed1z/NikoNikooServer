using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IServiceRequestAcceptService
    {
        void Create(ServiceRequestAccept newServiceRequestAccept);
        void Update(ServiceRequestAccept ServiceRequestAccept);
        ServiceRequestAccept GetById(Guid ServiceRequestAcceptId);
    }
}
