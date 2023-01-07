using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IServiceTypeService
    {
        #region ServiceType
        List<ServiceType> GetAll();
        IPagedList<ServiceType> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        #endregion ServiceType

        #region ServiceTypeQuestion
        IPagedList<ServiceTypeQuestion> GetServiceTypeQuestionPagedAll(int page = 0, int pageSize = int.MaxValue);
        ServiceTypeQuestion GetServiceTypeQuestionById(int ServiceTypeQuestionId);
        List<ServiceTypeQuestion> GetAllServiceTypeQuestion(int ServiceTypeId);
        void CreateServiceTypeQuestion(ServiceTypeQuestion newServiceTypeQuestion);
        void UpdateServiceTypeQuestion(ServiceTypeQuestion ServiceTypeQuestion);
        Task DeleteServiceTypeQuestion(int ServiceTypeQuestionId);
        #endregion ServiceTypeQuestion
    }
}
