using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineServices.Entity;
using OnlineServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineServices.Core
{
    public interface IServiceRequestService
    {
        void CreateAsync(ServiceRequest newServiceRequest,List<int> SelectedServiceItems);
        void UpdateAsync(ServiceRequest ServiceRequest, List<int> SelectedServiceItems);
        ServiceRequest GetById(Guid ServiceRequestId);
        List<ServiceRequest> GetAll(Guid PersonId);
        List<ServiceRequest> GetAll(Guid PersonId, int ServiceTypeId);
        List<ServiceRequest> GetAllForEmployee(Guid PersonId, byte LastStatusId = 0);
        IPagedList<ServiceRequest> GetPagedAll(int page = 0, int pageSize = int.MaxValue);
        List<ServiceRequestDetail> GetAllServiceRequestDetail(Guid ServiceRequestId);
        int GetServiceRequestCount(Guid PersonId);
        void CreateServiceRequestSurvey(ServiceRequestSurvey newServiceRequestSurvey);
        void UploadServiceRequestFile(ServiceRequestFile model);
        dynamic GetAllServiceRequestFile(string serviceRequestId);
        bool IsSurveyDuplicate(Guid ServiceRequestId, int ServiceTypeQuestionId);
        Task<ServiceRequestInformation> GetRequestInformationById(Guid serviceRequestId);
        
    }
}
