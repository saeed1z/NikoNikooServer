using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Models;
using OnlineServices.Utilities;

namespace OnlineServices.Controllers
{
    public class ServiceRequestController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceTypeUnitPriceService _serviceTypeUnitPriceService;
        private readonly IPersonService _personService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion Fields

        #region Ctor
        public ServiceRequestController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonService_Service personService_Service,
            IServiceRequestService serviceRequestService,
            IServiceTypeService serviceTypeService,
            IServiceTypeUnitPriceService serviceTypeUnitPriceService,
            IPersonService personService,IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._serviceTypeService = serviceTypeService;
            this._serviceTypeUnitPriceService = serviceTypeUnitPriceService;
            this._serviceRequestService = serviceRequestService;
            this._personService = personService;
            this._hostingEnvironment = hostEnvironment;
        }
        #endregion Ctor

        #region Utilities
        #endregion Utilities

        #region Methods
        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/PersonType/Index" });

            return View(nameof(Index));
        }
        public IActionResult LoadServiceRequestList(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/PersonType/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new ServiceRequestListModel();
            var serviceRequestList = new List<ServiceRequestModel>();
            IPagedList<ServiceRequest> serviceRequests = _serviceRequestService.GetPagedAll(page, pageSize);

            if (serviceRequests.Count > 0)
            {
                serviceRequestList = serviceRequests.Select((value, index) => new ServiceRequestModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id.ToString(),
                    PersonId = value.PersonId.ToString(),
                    PersonName = value.Person != null ? value.Person.FirstName + " " + value.Person.LastName : "",
                    RequestDateTime = BaseSettings.Gregorian2HijriSlashedWithTime(value.RequestDateTime),
                    ServiceTypeId = value.ServiceTypeId,
                    ServiceTypeTitle = value.ServiceType != null ? value.ServiceType.Title : "",
                    PersonCarId = value.PersonCarId,
                    SourceStateId = value.SourceStateId,
                    SourceStateTitle = value.SourceState != null ? value.SourceState.Title : "",
                    SourceCityId = value.SourceCityId,
                    SourceCityTitle = value.SourceCity != null ? value.SourceCity.Title : "",
                    SourceAddress = value.SourceAddress,
                    SourceLocation = value.SourceLocation,
                    DestinationStateId = value.DestinationStateId,
                    DestinationStateTitle = value.DestinationState != null ? value.DestinationState.Title : "",
                    DestinationCityId = value.DestinationCityId,
                    DestinationCityTitle = value.DestinationCity != null ? value.DestinationCity.Title : "",
                    DestinationAddress = value.DestinationAddress,
                    DestinationLocation = value.DestinationLocation,
                    Description = value.Description,
                    LastStatusId = value.LastStatusId,
                    LastStatusTitle = value.LastStatus != null ? value.LastStatus.Title : ""
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = serviceRequests.PageSize,
                TotalRecords = serviceRequests.TotalCount,
                PageIndex = serviceRequests.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ServiceRequestListModel
            {
                ServiceRequestModel = serviceRequestList,
                PagerModel = pagerModel
            };
            return View("LoadServiceRequest", model);
        }

        public IActionResult LoadRefrenceUserList(string serviceRequestId = "", int serviceTypeId = 0, int stateId = 0, int cityId = 0, int page = 0)
        {
            if (page > 0)
                page -= 1;
            var pageSize = int.MaxValue;
            //var pageSize = 10;
            int rowNum = page * pageSize + 1;
            var model = new RefrencePersonListModel();

            var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(serviceRequestId));
            if (objServiceRequest != null)
            {
                var expertPersonList = new List<RefrencePersonModel>();
                var employeePersonList = new List<RefrencePersonModel>();
                IPagedList<Person> expertPersons = _personService.GetPagedRefrenceUser((int)PersonTypeEnum.Expert, serviceTypeId, stateId, cityId, page, pageSize);
                IPagedList<Person> employeePersons = _personService.GetPagedRefrenceUser((int)PersonTypeEnum.Employee, serviceTypeId, stateId, cityId, page, pageSize);
                if (expertPersons.Count > 0)
                {
                    expertPersonList = expertPersons.Select((value, index) => new RefrencePersonModel
                    {
                        RowNum = rowNum + index,
                        Id = value.Id.ToString(),
                        PersonTypeId = value.PersonTypeId,
                        PersonTypeTitle = value.PersonType != null ? value.PersonType.Title : "",
                        FirstName = value.FirstName,
                        LastName = value.LastName,
                        EmployeeNo = value.EmployeeNo,
                        MobileNo = value.MobileNo,
                        IsActive = value.IsActive,
                    }).ToList();
                }
                if (employeePersons.Count > 0)
                {
                    employeePersonList = employeePersons.Select((value, index) => new RefrencePersonModel
                    {
                        RowNum = rowNum + index,
                        Id = value.Id.ToString(),
                        PersonTypeId = value.PersonTypeId,
                        PersonTypeTitle = value.PersonType != null ? value.PersonType.Title : "",
                        FirstName = value.FirstName,
                        LastName = value.LastName,
                        EmployeeNo = value.EmployeeNo,
                        MobileNo = value.MobileNo,
                        IsActive = value.IsActive,
                    }).ToList();
                }
                //var expertPagerModel = new PagerModel
                //{
                //    PageSize = expertPersons.PageSize,
                //    TotalRecords = expertPersons.TotalCount,
                //    PageIndex = expertPersons.PageIndex,
                //    ShowTotalSummary = true,
                //    ShowFirst = true,
                //    ShowLast = true,
                //    RouteValues = new RouteValues { page = page }
                //};
                //var employeePagerModel = new PagerModel
                //{
                //    PageSize = employeePersons.PageSize,
                //    TotalRecords = employeePersons.TotalCount,
                //    PageIndex = employeePersons.PageIndex,
                //    ShowTotalSummary = true,
                //    ShowFirst = true,
                //    ShowLast = true,
                //    RouteValues = new RouteValues { page = page }
                //};

                model = new RefrencePersonListModel
                {
                    ExpertPersonModel = expertPersonList,
                    EmployeePersonModel = employeePersonList,
                    ServiceRequest = objServiceRequest
                    //ExpertPagerModel = expertPagerModel,
                    //EmployeePagerModel = employeePagerModel
                };
            }
            return View(nameof(LoadRefrenceUserList), model);
        }

        [HttpPost]
        public JsonResult SubmitRefrence(string ServiceRequestId, string PersonId, int PersonTypeId)
        {
            try
            {
                var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(ServiceRequestId));
                if (PersonTypeId == (int)PersonTypeEnum.Employee)
                {
                    objServiceRequest.EmployeeId = System.Xml.XmlConvert.ToGuid(PersonId);
                    //objServiceRequest.ExpertId = null;
                }
                else if (PersonTypeId == (int)PersonTypeEnum.Expert)
                {
                    objServiceRequest.ExpertId = System.Xml.XmlConvert.ToGuid(PersonId);
                    //objServiceRequest.EmployeeId = null;
                }
                if(objServiceRequest.EmployeeId!=null && objServiceRequest.ExpertId!=null)
                {
                    objServiceRequest.LastStatusId = (int)StatusEnum.WaitingAcceptanceByEmployee;
                    NotificationHandler.SendToUser("درخواست خدمت", objServiceRequest.Person.Notifykey, false, "درخواست شما جهت رسیدگی به فرد مربوطه ارجاع شد", new Dictionary<string, string>());
                }

                objServiceRequest.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                objServiceRequest.UpdatedDate = DateTime.Now;
                _serviceRequestService.UpdateAsync(objServiceRequest, null);

                var objPerson = _personService.GetById(System.Xml.XmlConvert.ToGuid(PersonId));
                
                NotificationHandler.SendToUser("درخواست خدمت", objPerson.Notifykey, true, "یک درخواست جدید به لیست کار های شما اضافه شد", new Dictionary<string, string>());

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.ToString() });
            }
        }


        [HttpPost]
        public JsonResult UploadFile([FromBody] OnlineServices.Models.ServiceRequestFile model)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string MyFile = null;
                Guid MyFileGuid = Guid.NewGuid();
                byte[] fileBinary = Convert.FromBase64String(model.RequestFileBase64);

                if (fileBinary != null && fileBinary.Length > 0)
                {
                    MyFile = MyFileGuid.ToString() + "." + model.RequestFileExtension.ToLower();
                    if (!Directory.Exists(Path.Combine(webRootPath, "Uploads/ServiceRequestFile/")))
                        Directory.CreateDirectory(Path.Combine(webRootPath, "Uploads/ServiceRequestFile/"));
                    var path = Path.Combine(Path.Combine(webRootPath, "Uploads/ServiceRequestFile/"), MyFile);
                    //using (MemoryStream ms = new MemoryStream(fileBinary))
                    //{
                        System.IO.File.WriteAllBytes(path, fileBinary);
                    //}
                }

                

                Entity.ServiceRequestFile serviceRequestFile = new Entity.ServiceRequestFile()
                {
                    ServiceRequestId = Guid.Parse(model.ServiceRequestId),
                    RequestFileExtension = model.RequestFileExtension.ToLower(),
                    RequestFileName = MyFile
                };
                _serviceRequestService.UploadServiceRequestFile(serviceRequestFile);

                return Json(new { status = true, error="" , myFile = MyFile });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, error = ex.ToString(), myFile="" });
            }
        }

        [HttpGet]
        public FileResult DownloadFile(string fileName)
        {
            try
            {
                string fileAddress = $"wwwroot/Uploads/ServiceRequestFile/{fileName}";
                byte[] fileBytes = System.IO.File.ReadAllBytes(fileAddress);
                Stream stream = new MemoryStream(fileBytes);
                
                if (stream == null)
                {
                    return File(new byte[] { 1, 1, 1 }, "application/json");
                }
                FileExtensionContentTypeProvider contentType = new FileExtensionContentTypeProvider();
                contentType.TryGetContentType(fileAddress, out string type);

                return File(stream, type);
               
            }
            catch (Exception)
            {
                return File(new byte[] { 1, 1, 1 }, "application/json");
            }

            //file can be downloaded like with the codes below in js
            //   fetch('http://localhost:54130/ServiceRequest/DownloadFile?fileName=abcd123.jpg', {
            //   method: 'GET',
            //       headers:
            //       {
            //           'responseType': 'blob',
            //       },
            //       //body: JSON.stringify(data) 

            //   }).then(response => response.blob())
            //    .then(res => {
            //    const url = window.URL.createObjectURL(res);
            //    const link = document.createElement('a');
            //    link.href = url;
            //    link.setAttribute('download', 'abcd123.jpg');
            //    link.click();
            //})
        }
        #endregion Methods
    }
}
