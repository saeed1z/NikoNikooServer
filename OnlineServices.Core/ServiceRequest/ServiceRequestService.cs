using OnlineServices.Entity;
using OnlineServices.Persistence;
using OnlineServices.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace OnlineServices.Core
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly OnlineServicesDbContext _db;
        public ServiceRequestService(OnlineServicesDbContext db)
        {
            _db = db;
        }

        public void CreateAsync(ServiceRequest newServiceRequest, List<int> SelectedServiceItems)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _db.ServiceRequest.Add(newServiceRequest);
                    if (SelectedServiceItems != null)
                    {
                        foreach (var item in SelectedServiceItems)
                        {
                            var objServiceRequestDetail = new ServiceRequestDetail();
                            objServiceRequestDetail.Id = Guid.NewGuid();
                            objServiceRequestDetail.ServiceRequestId = newServiceRequest.Id;
                            objServiceRequestDetail.ServiceDetailBaseId = item;
                            objServiceRequestDetail.CreatedDate = DateTime.Now;
                            objServiceRequestDetail.CreatedUserId = newServiceRequest.CreatedUserId;
                            _db.ServiceRequestDetail.Add(objServiceRequestDetail);
                        }
                    }
                    _db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }

        }

        public void UpdateAsync(ServiceRequest ServiceRequest, List<int> SelectedServiceItems)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    _db.Update(ServiceRequest);
                    if (SelectedServiceItems != null)
                    {
                        var serviceRequestDetailList = _db.ServiceRequestDetail.Where(x => x.ServiceRequestId == ServiceRequest.Id).ToList();
                        if (serviceRequestDetailList.Count > 0)
                        {
                            foreach (var item in serviceRequestDetailList)
                                _db.ServiceRequestDetail.Remove(item);
                        }

                        foreach (var item in SelectedServiceItems)
                        {
                            var objServiceRequestDetail = new ServiceRequestDetail();
                            objServiceRequestDetail.Id = Guid.NewGuid();
                            objServiceRequestDetail.ServiceRequestId = ServiceRequest.Id;
                            objServiceRequestDetail.ServiceDetailBaseId = item;
                            objServiceRequestDetail.CreatedDate = DateTime.Now;
                            objServiceRequestDetail.CreatedUserId = ServiceRequest.CreatedUserId;
                            _db.ServiceRequestDetail.Add(objServiceRequestDetail);
                        }
                    }
                    _db.SaveChanges();
                    scope.Complete();
                }
                catch (Exception ex)
                {
                    scope.Dispose();
                    throw ex;
                }
            }
        }
        public ServiceRequest GetById(Guid ServiceRequestId) => _db.ServiceRequest.Find(ServiceRequestId);

        public List<ServiceRequest> GetAll(Guid PersonId) =>
            _db.ServiceRequest.Where(x => x.PersonId == PersonId).ToList();

        public List<ServiceRequest> GetAll(Guid PersonId, int ServiceTypeId) =>
            _db.ServiceRequest.Where(x => x.PersonId == PersonId &&
            (x.ServiceTypeId == ServiceTypeId || ServiceTypeId == 0))
            .OrderByDescending(x => x.RequestDateTime).ToList();


        public int GetServiceRequestCount(Guid PersonId) =>
            _db.ServiceRequest.Where(x => x.PersonId == PersonId).Count();

        public List<ServiceRequest> GetAllForEmployee(Guid PersonId, byte LastStatusId = 0) =>
           _db.ServiceRequest.Where(x => (x.ExpertId == PersonId || x.EmployeeId == PersonId)
           && (x.LastStatusId == LastStatusId || LastStatusId == 0)).OrderByDescending(o => o.RequestDateTime).ToList();
        public IPagedList<ServiceRequest> GetPagedAll(int page = 0, int pageSize = int.MaxValue)
        {
            var query = _db.ServiceRequest.OrderByDescending(x => x.RequestDateTime).ToList();
            return new PagedList<ServiceRequest>(query, page, pageSize);
        }

        public List<ServiceRequestDetail> GetAllServiceRequestDetail(Guid ServiceRequestId) =>
            _db.ServiceRequestDetail.Where(x => x.ServiceRequestId == ServiceRequestId).ToList();



        public void CreateServiceRequestSurvey(ServiceRequestSurvey newServiceRequestSurvey)
        {
            try
            {
                _db.ServiceRequestSurvey.Add(newServiceRequestSurvey);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UploadServiceRequestFile(ServiceRequestFile model)
        {
            try
            {
                ServiceRequestFile newServiceRequestFile = new ServiceRequestFile { Id=Guid.NewGuid(), ServiceRequestId = model.ServiceRequestId, RequestFileName = model.RequestFileName, RequestFileExtension = model.RequestFileExtension };
                _db.ServiceRequestFile.Add(newServiceRequestFile);
                _db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public dynamic GetAllServiceRequestFile(string serviceRequestId)
        {
            try
            {
                var filesList =
                 _db.ServiceRequestFile.Where(x => x.ServiceRequestId == System.Xml.XmlConvert.ToGuid(serviceRequestId))
                 .Select(x => new
                 {
                     fileName = x.RequestFileName,
                     fileExtension = x.RequestFileExtension
                 }).ToList();
                    return filesList;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<ServiceRequestInformation> GetRequestInformationById(Guid Id) {

            return await Task.Run(async () =>
            {
                ServiceRequest serviceRequest =  await _db.ServiceRequest.FindAsync(Id);
                if (serviceRequest == null)
                    return null;

                PersonCar personCar = await _db.PersonCar.FindAsync(serviceRequest.PersonCarId);

                Model model = null;
                Brand brand = null;
                if (personCar != null)
                {
                    model = await _db.Model.FindAsync(personCar.ModelId);
                    brand = await _db.Brand.FindAsync(model.BrandId);
                }

                Person person = await _db.Person.FindAsync(serviceRequest.PersonId);
                Person Expert = await _db.Person.FindAsync(serviceRequest.ExpertId);
                Person Employee = await _db.Person.FindAsync(serviceRequest.EmployeeId);
                ServiceType serviceType = await _db.ServiceType.FindAsync(serviceRequest.ServiceTypeId);

                ServiceRequestInformation serviceRequestInformation = new ServiceRequestInformation()
                {
                    Id = serviceRequest.Id,
                    carBrand = brand?.Title??"",
                    carModel = model?.Title??"",
                    CustomerName = person==null ? "" : person.FirstName + " " + person.LastName,
                    ExpertName = Expert == null ? "" : Expert.FirstName + " " + Expert.LastName,
                    EmployeeName = Employee == null ? "" : Employee.FirstName + " " + Employee.LastName,
                    CustomerMobileNumber = person?.MobileNo??"",
                    EmployeeMobileNumber = Employee?.MobileNo??"",
                    ExpertMobileNumber = Expert?.MobileNo??"",
                    plaqueNo = personCar?.PlaqueNo??"",
                    serviceDescription = serviceRequest?.Description??"",
                    carDescription = personCar?.Description??"",
                    serviceType = serviceType?.Title??"",
                    source = serviceRequest.SourceAddress,
                    destination = serviceRequest.DestinationAddress,
                    details = this.GetAllServiceRequestDetail(Id).Select(x => x.ServiceDetailBase.Title).ToList()
                };


                //ServiceRequestInformation serviceRequestInformation = _db.ServiceRequest.Select(x => new ServiceRequestInformation()
                //{
                //    Id = x.Id,
                //    carBrand = x.PersonCar.Model.Brand.Title,
                //    carModel = x.PersonCar.Model.Title,
                //    CustomerName = x.Person.FirstName + " " + x.Person.LastName,
                //    ExpertName = x.Expert.FirstName + " " + x.Expert.LastName,
                //    EmployeeName = x.Employee.FirstName + " " + x.Employee.LastName,
                //    CustomerMobileNumber = x.Person.MobileNo,
                //    EmployeeMobileNumber = x.Employee.MobileNo,
                //    ExpertMobileNumber = x.Expert.MobileNo,
                //    plaqueNo = x.PersonCar.PlaqueNo,
                //    carDescription = x.PersonCar.Description,
                //    serviceDescription = x.Description,
                //    serviceType = x.ServiceType.Title,
                //    source = x.SourceAddress,
                //    destination = x.DestinationAddress,
                //    details = this.GetAllServiceRequestDetail(Id).Select(x => x.ServiceDetailBase.Title).ToList()
                //}).FirstOrDefault(x => x.Id == Id);

                //lazy loading does not work!!!!!!
                //ServiceRequest serviceRequest = _db.ServiceRequest.Find(Id);
                //ServiceRequestInformation serviceRequestInformation = new ServiceRequestInformation()
                //{
                //    Id = serviceRequest.Id,
                //    carBrand = serviceRequest.Brand.Title,
                //    carModel = serviceRequest.Model.Title,
                //    CustomerName = serviceRequest.Person.FirstName + serviceRequest.Person.LastName,
                //    ExpertName = serviceRequest.Expert.FirstName + serviceRequest.Expert.LastName,
                //    EmployeeName = serviceRequest.Employee.FirstName + serviceRequest.Employee.LastName,
                //    CustomerMobileNumber = serviceRequest.Person.MobileNo,
                //    EmployeeMobileNumber = serviceRequest.Employee.MobileNo,
                //    ExpertMobileNumber = serviceRequest.Expert.MobileNo,
                //    plaqueNo = serviceRequest.PersonCar.PlaqueNo,
                //    description = serviceRequest.Description,
                //    source = serviceRequest.SourceAddress,
                //    destination = serviceRequest.DestinationAddress,
                //    details = this.GetAllServiceRequestDetail(Id).ToList()
                //};

                return serviceRequestInformation;
            });
        }

        public bool IsSurveyDuplicate(Guid ServiceRequestId, int ServiceTypeQuestionId)
        {
            var objServiceRequestSurvey = _db.ServiceRequestSurvey.FirstOrDefault(x =>
            (x.ServiceRequestId == ServiceRequestId)
            && (x.ServiceTypeQuestionId == ServiceTypeQuestionId));
            if (objServiceRequestSurvey != null)
                return true;
            else
                return false;
        }
    }
}
