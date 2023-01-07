using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Api.Models;
using OnlineServices.Api.Helpers;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Threading.Tasks;


namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IPersonPackageService _personPackageService;
        private readonly IPersonPackageDetailService _personPackageDetailService;
        private readonly IPersonCarService _personCarService;
        private readonly IModelService _modelService;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IServiceRequestAcceptService _serviceRequestAcceptService;
        private readonly IBaseService _baseService;
        private readonly ISystemSettingService _systemSettingService;
        private readonly IConfiguration _configuration;
        private static string neshanApiToken = "service.I3RCtCrKAd3RiIn2L6kr7VOBw0o88yO9L9guegGk";

        public ServiceRequestController(IModelService modelService,
            IServiceTypeService serviceTypeService,
            IPersonService personService,
            IPersonPackageService personPackageService,
            IPersonPackageDetailService personPackageDetailService,
            IPersonCarService personCarService,
            IServiceRequestService serviceRequestService,
            IAspNetUserService aspNetUserService,
            IBaseService baseService,
            IServiceRequestAcceptService serviceRequestAcceptService,
            ISystemSettingService systemSettingService,
            IConfiguration configuration)
        {
            _modelService = modelService;
            _serviceTypeService = serviceTypeService;
            _personService = personService;
            _personCarService = personCarService;
            _personPackageService = personPackageService;
            _serviceRequestService = serviceRequestService;
            _personPackageDetailService = personPackageDetailService;
            _aspNetUserService = aspNetUserService;
            _serviceRequestAcceptService = serviceRequestAcceptService;
            _baseService = baseService;
            _systemSettingService = systemSettingService;
            _configuration = configuration;
        }

        [HttpPost("ServiceRequestList")]
        public IActionResult ServiceRequestList([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            var retModel = _serviceRequestService.GetAll(
                PersonId: person.Id,
                ServiceTypeId: model.ServiceTypeId
                ).Select(c => new
                {
                    Id = c.Id.ToString(),
                    PersonId = c.PersonId.ToString(),
                    PersonName = person.FirstName + " " + person.LastName,
                    RequestDateTime = BaseSettings.Gregorian2HijriSlashedWithTime(c.RequestDateTime),
                    ServiceTypeId = c.ServiceTypeId,
                    ServiceTypeTitle = c.ServiceType != null ? c.ServiceType.Title : "",
                    PersonCarId = c.PersonCarId,
                    SourceStateId = c.SourceStateId,
                    SourceStateTitle = c.SourceState != null ? c.SourceState.Title : "",
                    SourceCityId = c.SourceCityId,
                    SourceCityTitle = c.SourceCity != null ? c.SourceCity.Title : "",
                    SourceAddress = c.SourceAddress,
                    SourceLocation = c.SourceLocation,
                    DestinationStateId = c.DestinationStateId,
                    DestinationStateTitle = c.DestinationState != null ? c.DestinationState.Title : "",
                    DestinationCityId = c.DestinationCityId,
                    DestinationCityTitle = c.DestinationCity != null ? c.DestinationCity.Title : "",
                    DestinationAddress = c.DestinationAddress,
                    DestinationLocation = c.DestinationLocation,
                    Description = c.Description,
                    LastStatusId = c.LastStatusId,
                    LastStatusTitle = c.LastStatus != null ? c.LastStatus.Title : "",
                    HasPendingFactor = c.ServiceFactor.Any(x => x.IsAccepted != true),
                    HasUnpaidFactor = c.ServiceFactor.Any(x => x.IsPaid != true),
                    ServiceRequestDetail = _serviceRequestService.GetAllServiceRequestDetail(c.Id).Select(sr => new ServiceRequestDetailModel
                    {
                        Title = sr.ServiceDetailBase.Title,
                        Value = sr.ServiceDetailBase.Id
                    }).ToList()
                }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }



        [HttpPost("SaveServiceRequest")]
        public IActionResult SaveServiceRequest([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            PersonPackage objPersonPackage = null;
            if (model.ServiceTypeId != (int)ServiceTypeEnum.TowTruckSeervice)
            {
                if (model.PersonPackageId == 0 || model.PersonPackageId == null)
                    return Ok(new { errorId = 99, errorTitle = "بسته مورد انتخاب نشده است", result = (string)null });

                objPersonPackage = _personPackageService.GetById(model.PersonPackageId);
                if (objPersonPackage == null)
                    return Ok(new { errorId = 99, errorTitle = "بسته مورد انتخاب نشده است", result = (string)null });
            }

            ServiceRequest objServiceRequest;
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));
                    if (objServiceRequest == null)
                        return Ok(new { errorId = 99, errorTitle = "سرویس درخواستی مورد نظر پیدا نشد", result = (string)null });

                    objServiceRequest = new ServiceRequest
                    {
                        ServiceTypeId = model.ServiceTypeId,
                        PersonCarId = model.PersonCarId != 0 && model.PersonCarId != null ? model.PersonCarId : null,
                        Description = model.Description,
                        SourceStateId = model.SourceStateId != 0 && model.SourceStateId != null ? model.SourceStateId : null,
                        SourceCityId = model.SourceCityId != 0 && model.SourceCityId != null ? model.SourceCityId : null,
                        SourceAddress = model.SourceAddress,
                        SourceLocation = model.SourceLocation,
                        DestinationStateId = model.DestinationStateId != 0 && model.DestinationStateId != null ? model.DestinationStateId : null,
                        DestinationCityId = model.DestinationCityId != 0 && model.DestinationCityId != null ? model.DestinationCityId : null,
                        DestinationAddress = model.DestinationAddress,
                        DestinationLocation = model.DestinationLocation,
                        UpdatedDate = DateTime.Now,
                        UpdatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id),
                        LastStatusId = (byte)StatusEnum.RequestServiceByUser,
                        BrandId = model.BrandId != 0 && model.BrandId != null ? model.BrandId : null,
                        ModelId = model.ModelId != 0 && model.ModelId != null ? model.ModelId : null
                    };
                }
                else
                    objServiceRequest = new ServiceRequest()
                    {
                        PersonId = person.Id,
                        RequestDateTime = DateTime.Now,
                        ServiceTypeId = model.ServiceTypeId,
                        PersonCarId = model.PersonCarId != 0 && model.PersonCarId != null ? model.PersonCarId : null,
                        Description = model.Description,
                        SourceStateId = model.SourceStateId != 0 && model.SourceStateId != null ? model.SourceStateId : null,
                        SourceCityId = model.SourceCityId != 0 && model.SourceCityId != null ? model.SourceCityId : null,
                        SourceAddress = model.SourceAddress,
                        SourceLocation = model.SourceLocation,
                        DestinationStateId = model.DestinationStateId != 0 && model.DestinationStateId != null ? model.DestinationStateId : null,
                        DestinationCityId = model.DestinationCityId != 0 && model.DestinationCityId != null ? model.DestinationCityId : null,
                        DestinationAddress = model.DestinationAddress,
                        DestinationLocation = model.DestinationLocation,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id),
                        LastStatusId = (byte)StatusEnum.RequestServiceByUser,
                        BrandId = model.BrandId != 0 && model.BrandId != null ? model.BrandId : null,
                        ModelId = model.ModelId != 0 && model.ModelId != null ? model.ModelId : null
                    };


                bool found = false;
                if (objPersonPackage != null)
                {
                    PersonPackageDetail objPersonPackageDetail = _personPackageDetailService.GetByPersonPackageIdAndServiceTypeId(objPersonPackage.Id, model.ServiceTypeId);
                    if (objPersonPackageDetail == null)
                        return Ok(new { errorId = 99, errorTitle = "بسته مورد نظر پیدا نشد", result = (string)null });

                    if (objPersonPackageDetail.Quantity != 0)
                    {
                        if (objPersonPackageDetail.Quantity > objPersonPackageDetail.UsedQuantity)
                        {
                            if (!string.IsNullOrEmpty(model.Id))
                                _serviceRequestService.UpdateAsync(objServiceRequest, model.SelectedServiceItems);
                            else
                                _serviceRequestService.CreateAsync(objServiceRequest, model.SelectedServiceItems);

                            objPersonPackageDetail.UsedQuantity = (short)(objPersonPackageDetail.UsedQuantity + 1);
                            _personPackageDetailService.Update(objPersonPackageDetail);

                            found = true;
                        }
                    }
                }
                else
                {
                    found = true;
                    if (!string.IsNullOrEmpty(model.Id))
                        _serviceRequestService.UpdateAsync(objServiceRequest, model.SelectedServiceItems);
                    else
                        _serviceRequestService.CreateAsync(objServiceRequest, model.SelectedServiceItems);
                }

                if (found)
                    return Ok(new { errorId = 0, errorTitle = "", result = objServiceRequest.Id });
                else
                    return Ok(new { errorId = 99, errorTitle = "شما هیچ بسته فعالی ندارید", result = (string)null });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = 0 });
            }
        }

        [HttpPost("ChangeRequestStatus")]
        public IActionResult ChangeRequestStatus([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            IdentityUser currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.Id))
                return Ok(new { errorId = 99, errorTitle = "شناسه درخواست معتبر نمی باشد", result = (string)null });

            if (model.LastStatusId == null || model.LastStatusId == 0)
                return Ok(new { errorId = 99, errorTitle = "وضعیت درخواست مشخص نیست", result = (string)null });

            var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));

            if (objServiceRequest == null)
                return Ok(new { errorId = 99, errorTitle = "سرویس درخواستی مورد نظر پیدا نشد", result = (string)null });

            if (objServiceRequest.ExpertId != person.Id)
                return Ok(new { errorId = 99, errorTitle = "درخواست به این کارمند یا کارشناس ارجاع نشده است", result = (string)null });

            if(objServiceRequest.EmployeeId == person.Id)
                return Ok(new { errorId = 99, errorTitle = "راننده نمی تواند وضعیت درخواست را تغییر دهد", result = (string)null });
            
            if (objServiceRequest.LastStatusId == (int)StatusEnum.AcceptanceRequestByEmployee &&
                model.LastStatusId == (int)StatusEnum.AcceptanceRequestByEmployee)
                return Ok(new { errorId = 99, errorTitle = "این درخواست قبلا تایید شده است", result = (string)null });

            if (objServiceRequest.LastStatusId == (int)StatusEnum.CancelRequestByUser)
                return Ok(new { errorId = 99, errorTitle = "این درخواست قبلا توسط کاربر لغو شده", result = (string)null });

            if (objServiceRequest.LastStatusId == (int)StatusEnum.EndOfServiceAndDeliveryToUser)
                return Ok(new { errorId = 99, errorTitle = "این درخواست قبلا به اتمام رسیده", result = (string)null });

            try
            {
                objServiceRequest.LastStatusId = model.LastStatusId;

                if (model.LastStatusId == (int)StatusEnum.AcceptanceRequestByEmployee)
                {
                    ServiceRequestAccept objServiceRequestAccept = new ServiceRequestAccept
                    {
                        AcceptDateTime = DateTime.Now,
                        ServiceRequestId = objServiceRequest.Id,
                        PersonId = objServiceRequest.PersonId,
                        ExpertPersonId = person.Id
                    };
                    _serviceRequestAcceptService.Create(objServiceRequestAccept);
                }

                else if (model.LastStatusId == (int)StatusEnum.CancelRequestByEmployee)
                {
                    if (person.PersonTypeId == (int)PersonTypeEnum.Expert)
                        objServiceRequest.ExpertId = null;
                    if (person.PersonTypeId == (int)PersonTypeEnum.Employee)
                        objServiceRequest.EmployeeId = null;
                }



                _serviceRequestService.UpdateAsync(objServiceRequest, null);

                NotificationHandler.SendToUser("تغییر وضعیت", objServiceRequest.Person.Notifykey, false, "درخواست شما به وضعیت " + objServiceRequest.LastStatus.Title + " تغییر یافت", new Dictionary<string, string>());
                return Ok(new { errorId = 0, errorTitle = "", result = objServiceRequest.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = 0 });
            }

        }

        [HttpPost("EmployeeServiceRequestList")]
        public IActionResult EmployeeServiceRequestList([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            var retModel = _serviceRequestService.GetAllForEmployee(
                PersonId: person.Id,
                LastStatusId: model.LastStatusId.HasValue ? model.LastStatusId.Value : (byte)0
                ).Select(c => new
                {
                    Id = c.Id.ToString(),
                    PersonId = c.PersonId.ToString(),
                    PersonName = person.FirstName + " " + person.LastName,
                    RequestDateTime = BaseSettings.Gregorian2HijriSlashedWithTime(c.RequestDateTime),
                    ServiceTypeId = c.ServiceTypeId,
                    ServiceTypeTitle = c.ServiceType != null ? c.ServiceType.Title : "",
                    PersonCarId = c.PersonCarId,
                    SourceStateId = c.SourceStateId,
                    SourceStateTitle = c.SourceState != null ? c.SourceState.Title : "",
                    SourceCityId = c.SourceCityId,
                    SourceCityTitle = c.SourceCity != null ? c.SourceCity.Title : "",
                    SourceAddress = c.SourceAddress,
                    SourceLocation = c.SourceLocation,
                    DestinationStateId = c.DestinationStateId,
                    DestinationStateTitle = c.DestinationState != null ? c.DestinationState.Title : "",
                    DestinationCityId = c.DestinationCityId,
                    DestinationCityTitle = c.DestinationCity != null ? c.DestinationCity.Title : "",
                    DestinationAddress = c.DestinationAddress,
                    DestinationLocation = c.DestinationLocation,
                    Description = c.Description,
                    LastStatusId = c.LastStatusId,
                    HasPendingFactor = c.ServiceFactor.Any(x => x.IsAccepted != true),
                    HasUnpaidFactor = c.ServiceFactor.Any(x => x.IsPaid != true),
                    LastStatusTitle = c.LastStatus != null ? c.LastStatus.Title : "",
                    
                }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }



        [HttpPost("GetRequestDetails")]
        public async Task<IActionResult> GetRequestDetails([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            var retModel = await _serviceRequestService.GetRequestInformationById(System.Xml.XmlConvert.ToGuid(model.Id));

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }



        [HttpPost("EmployeeList")]
        public IActionResult EmployeeList([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (!person.Latitude.HasValue || !person.Longitude.HasValue)
                return Ok(new { errorId = 99, errorTitle = "موقعیت مکانی کاربر تعریف نشده", result = (string)null });

            if (string.IsNullOrEmpty(model.Id))
                return Ok(new { errorId = 99, errorTitle = "شناسه درخواست معتبر نمی باشد", result = (string)null });

            var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));

            if (objServiceRequest == null)
                return Ok(new { errorId = 99, errorTitle = "سرویس درخواستی مورد نظر پیدا نشد", result = (string)null });

            var retModel = _personService.GetRefrenceUserForApp(
                PersonTypeId: 0,
                serviceTypeId: objServiceRequest.ServiceTypeId,
                Latitude: person.Latitude.Value,
                Longitude: person.Longitude.Value
                ).Select(p => new PersonModel
                {
                    Id = p.Id.ToString(),
                    PersonTypeId = p.PersonTypeId,
                    PersonTypeTitle = p.PersonType.Title,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    NationalCode = p.NationalCode,
                    EmployeeNo = p.EmployeeNo,
                    StateId = p.StateId,
                    StateTitle = p.State != null ? p.State.Title : "",
                    CityId = p.CityId,
                    CityTitle = p.City != null ? p.City.Title : "",
                    MobileNo = p.MobileNo,
                    PhoneNo = p.PhoneNo,
                    Address = p.Address,
                    PostCode = p.PostCode,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                    CooperationStartDate = p.CooperationStartDate.ToString(),
                    CooperationEndDate = p.CooperationEndDate.ToString(),
                    IsActive = p.IsActive,
                    Notifykey = person.Notifykey,
                    Avatar = p.Avatar
                });
            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

        [HttpPost("GetAcceptanceEmployee")]
        public IActionResult GetAcceptanceEmployee([FromBody] ServiceRequestModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.Id))
                return Ok(new { errorId = 99, errorTitle = "شناسه درخواست معتبر نمی باشد", result = (string)null });

            var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));

            if (objServiceRequest.PersonId != person.Id)
                return Ok(new { errorId = 99, errorTitle = "این درخواست مربوط به شما نیست", result = (string)null });


            if (objServiceRequest.LastStatusId == (int)StatusEnum.WaitingAcceptanceByEmployee ||
                objServiceRequest.LastStatusId == (int)StatusEnum.EndOfServiceAndDeliveryToUser ||
                objServiceRequest.LastStatusId == (int)StatusEnum.RequestServiceByUser)
                return Ok(new { errorId = 99, errorTitle = "وضعیت سرویس اجازه این درخواست را نمی دهد", result = (string)null });

            Person objPerson = null;
            if (objServiceRequest.ExpertId != null)
                objPerson = _personService.GetById(
                objServiceRequest.ExpertId.Value
                );
            else if (objServiceRequest.EmployeeId != null)
                objPerson = _personService.GetById(
                objServiceRequest.EmployeeId.Value
                );


            PersonModel retModel = null;
            if (objPerson != null)
            {
                retModel = new PersonModel()
                {
                    Id = objPerson.Id.ToString(),
                    PersonTypeId = objPerson.PersonTypeId,
                    PersonTypeTitle = objPerson.PersonType != null ? objPerson.PersonType.Title : "",
                    FirstName = objPerson.FirstName,
                    LastName = objPerson.LastName,
                    NationalCode = objPerson.NationalCode,
                    EmployeeNo = objPerson.EmployeeNo,
                    //StateId = objPerson.StateId,
                    //CityId = objPerson.CityId,
                    MobileNo = objPerson.MobileNo,
                    //PhoneNo = objPerson.PhoneNo,
                    //Address = objPerson.Address,
                    //PostCode = objPerson.PostCode,
                    Latitude = objPerson.Latitude,
                    Longitude = objPerson.Longitude
                    //CooperationStartDate = objPerson.CooperationStartDate.ToString(),
                    //CooperationEndDate = objPerson.CooperationEndDate.ToString(),
                    //IsActive = objPerson.IsActive,
                    //Notifykey = person.Notifykey,
                    //Avatar = objPerson.Avatar
                };
            }


            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

        [HttpPost("CalcFareBetweenAB")]
        public IActionResult CalcFareBetweenAB([FromBody] FareBetweenModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.PointA))
                return Ok(new { errorId = 99, errorTitle = "مقدار مبدا ضروری است", result = (string)null });

            if (string.IsNullOrEmpty(model.PointB))
                return Ok(new { errorId = 99, errorTitle = "مقدار مقصد ضروری است", result = (string)null });



            try
            {
                string requestUrl = "https://api.neshan.org/v1/distance-matrix?origins=" + model.PointA + "&destinations=" + model.PointB + "&type=car&avoidTrafficZone=false&avoidOddEvenZone=false&alternative=true";
                string responseStream = null;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "Get";
                request.Headers["Api-Key"] = neshanApiToken;
                request.ContentType = "application/json";
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            responseStream = responseReader.ReadToEnd();
                        }
                    }
                }

                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseStream);

                if (result.status.ToString() == "Ok")
                {
                    var neshanDistance = result.rows[0].elements[0].distance.value.ToString();
                    // طول مسافت سفر بر حسب متر
                    var b = Convert.ToDouble(neshanDistance);
                    // طول مسافت سفر بر حسب کیلومتر
                    var distanceKilometer = b / 1000;

                    var neshanDuration = result.rows[0].elements[0].duration.value.ToString();
                    // طول سفر بر حسب ثانیه
                    var a = Convert.ToDouble(neshanDuration);
                    // طول سفر بر حسب دقیقه
                    var distanceMinute = a / 60;

                    var objSystemSetting = _systemSettingService.GetAll();

                    // مبلغ ثابت ورودیه
                    var objStaticPrice = objSystemSetting.FirstOrDefault(x => x.Title == SystemSettingEnum.StaticPrice.ToString());
                    var E = Convert.ToDouble(objStaticPrice.Value);

                    var objAverageSpeed = objSystemSetting.FirstOrDefault(x => x.Title == SystemSettingEnum.AverageSpeed.ToString());
                    var C = Convert.ToDouble(objAverageSpeed.Value); // km/h

                    //تبدیل کیلومتر بر ساعت به متر بر دقیقه
                    // km/h * 16.67

                    //تبدیل متر بر دقیقه به کیلومتر بر ساعت
                    // m/m * 0.060

                    //var averageSpeedMeterPerSecond = (C * 1000)/(60*60);

                    // ضریب قیمت طول زمان سفر
                    var objTimeRatio = objSystemSetting.FirstOrDefault(x => x.Title == SystemSettingEnum.TimeRatio.ToString());
                    var D = Convert.ToDouble(objTimeRatio.Value);

                    //var price = Math.Ceiling(staticPrice +
                    //    (distanceKilometer * timeRatio) *
                    //    (averageSpeedMeterPerMinute / (averageSpeedKilometerPerHour / distanceMinute))
                    //));

                    //var price = Math.Ceiling(staticPrice + ((distanceMeter / 1000) * timeRatio) * (averageSpeedMeterPerMinute / (distanceMeter / distanceMinute)));

                    var price = E + ((b / 1000) * D) * ((C * 1000 / (60 * 60)) / ((b) / (a)));


                    return Ok(new
                    {
                        errorId = 0,
                        errorTitle = "",
                        result = Math.Round(price / 1000) * 1000
                    });
                }
                else if (result.status.ToString() == "NoRoute")
                    return Ok(new { errorId = 99, errorTitle = "هیچ مسیری بین دو نقطه شروع و پایان وجود ندارد.", result = (string)null });
                else
                    return Ok(new { errorId = 99, errorTitle = "نشان دهنده مشکل سمت سرور در هنگام پردازش این element است.ممکن است با تلاش دوباره این مشکل برطرف شود.", result = (string)null });

            }
            catch (System.Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.ToString(), result = (string)null });
            }
        }

        [HttpPost("GetAddress")]
        public IActionResult GetAddress([FromBody] FareBetweenModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.Point))
                return Ok(new { errorId = 99, errorTitle = "موقعیت مکان ضروری است", result = (string)null });

            var point = model.Point.Split(",");

            try
            {
                string requestUrl = "https://api.neshan.org/v2/reverse?lat=" + point[0] + "&lng=" + point[1];
                string responseStream = null;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "Get";
                request.Headers["Api-Key"] = neshanApiToken;
                request.ContentType = "application/json";
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            responseStream = responseReader.ReadToEnd();
                        }
                    }
                }

                dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseStream);


                var address = result.addresses.Count > 1 ? result.addresses[1].formatted.ToString() : result.addresses[0].formatted.ToString();

                var json = new
                {
                    state = result.state.ToString(),
                    city = result.city.ToString(),
                    address = address
                };

                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = json
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.ToString(), result = (string)null });
            }
        }


        [HttpGet("GetAllServiceRequestCount")]
        public IActionResult GetAllServiceRequestCount()
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            try
            {
                int allServiceRequestCount = _serviceRequestService.GetServiceRequestCount(person.Id);
                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = allServiceRequestCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("SubmitSurvey")]
        public IActionResult SubmitSurvey([FromBody] ServiceRequestSurveyModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.ServiceRequestId))
                return BadRequest(new { errorId = 99, errorTitle = "شناسه درخواست نا معتبر", result = (string)null });

            if (model.ServiceTypeQuestionId == null || model.ServiceTypeQuestionId == 0)
                return Ok(new { errorId = 99, errorTitle = "شناسه سوال نا معتبر", result = (string)null });

            if (model.Score == null || model.Score == 0 || model.Score > 5)
                return Ok(new { errorId = 99, errorTitle = "مقدار امتیاز باید بین 1 تا 5 باشد", result = (string)null });

            try
            {
                var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceRequestId));
                if (objServiceRequest == null)
                    return Ok(new { errorId = 99, errorTitle = "درخواستی با این مشخصات پیدا نشد", result = (string)null });

                var objServiceTypeQuestion = _serviceTypeService.GetServiceTypeQuestionById(model.ServiceTypeQuestionId);
                if (objServiceTypeQuestion == null || objServiceTypeQuestion.IsActive == false)
                    return Ok(new { errorId = 99, errorTitle = "نظرسنجی با این مشخصات پیدا نشد", result = (string)null });

                if (_serviceRequestService.IsSurveyDuplicate(objServiceRequest.Id, objServiceTypeQuestion.Id))
                    return Ok(new { errorId = 99, errorTitle = "شما قبلا برای این درخواست در این نظر سنجی شرکت کرده اید", result = (string)null });

                var objServiceRequestSurvey = new ServiceRequestSurvey
                {
                    Id = new Guid(),
                    ServiceRequestId = objServiceRequest.Id,
                    ServiceTypeQuestionId = objServiceTypeQuestion.Id,
                    Score = model.Score,
                    Description = model.Description,
                };
                _serviceRequestService.CreateServiceRequestSurvey(objServiceRequestSurvey);
                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = objServiceRequestSurvey.Id
                });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.ToString(), result = (string)null });
            }
        }



        [HttpGet("ShowEmployeesList")]
        public IActionResult ShowEmployeesList([FromQuery(Name = "serviceTypeId")] int serviceTypeId)
        {
            var token = Request.Headers["Token"];
           
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            return Ok(new {errorId=0, errorTitlt="", result= _personService.GetAllEmployees(serviceTypeId)});
        }


        [HttpPost("AssignEmployeeToRequest")]
        public IActionResult AssignEmployeeToRequest([FromBody] ServiceRequest model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            var serviceRequest = _serviceRequestService.GetById(model.Id);

            if (person.Id != serviceRequest.ExpertId)
            {
                return Ok(new { errorId = 99, errorTitle = "کارشناس مجاز به تعیین راننده برای این سرویس نیست", result = "" });
            }

            if(serviceRequest.LastStatusId != (byte)StatusEnum.AcceptanceRequestByEmployee)
            {
                return Ok(new { errorId = 99, errorTitle = "تعیین راننده تنها در مرحله پذیرش درخواست توسط کارشناس یا کارمند امکان پذیر است", result = "" });
            }

            serviceRequest.EmployeeId = model.EmployeeId;
            _serviceRequestService.UpdateAsync(serviceRequest,null);

            return Ok(new { errorId = 0, errorTitlt = "", result = "راننده، برای خدمت درخواستی در نظر گرفته شد" });
        }




        [HttpPost("UploadFile")]
        public IActionResult UploadFile([FromBody] Models.ServiceRequestFile model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            IdentityUser currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.ServiceRequestId))
                return Ok(new { errorId = 99, errorTitle = "شناسه درخواست معتبر نمی باشد", result = (string)null });


            var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceRequestId));

            if (objServiceRequest == null)
                return Ok(new { errorId = 99, errorTitle = "سرویس درخواستی مورد نظر پیدا نشد", result = (string)null });

            if (objServiceRequest.LastStatusId == (int)StatusEnum.CancelRequestByUser)
                return Ok(new { errorId = 99, errorTitle = "این درخواست قبلا توسط کاربر لغو شده", result = (string)null });

            if (objServiceRequest.LastStatusId == (int)StatusEnum.EndOfServiceAndDeliveryToUser)
                return Ok(new { errorId = 99, errorTitle = "این درخواست قبلا به اتمام رسیده", result = (string)null });

            try
            {
                dynamic result = null;
                if (!string.IsNullOrEmpty(model.RequestFileBase64))
                {
                    string apiUrl = $"{_configuration.GetValue<string>("HostAddress:Local")}/ServiceRequest/UploadFile";
                    var input = new
                    {
                        ServiceRequestId = model.ServiceRequestId,
                        RequestFileBase64 = model.RequestFileBase64.ToString(),
                        RequestFileExtension = model.RequestFileExtension.ToString(),
                    };
                    string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(input);
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
                    httpRequest.ContentType = "application/json";
                    httpRequest.Method = "POST";

                    byte[] bytes = Encoding.UTF8.GetBytes(inputJson);
                    using (Stream stream = httpRequest.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Close();
                    }
                    HttpWebResponse httpResponse;
                    try
                    {
                        httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        //string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        httpResponse = (HttpWebResponse)ex.Response;
                    }
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        string json = (new StreamReader(stream)).ReadToEnd();
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    }
                }

                if (result != null)
                    if (result.status == false)
                        return Ok(new { errorId = 99, errorTitle = result.error, result = (string)null });

                string PanelUrl = _configuration.GetSection("PanelUrl").Value;
                return Ok(new { errorId = 0, errorTitle = "", result = PanelUrl + "Uploads/ServiceRequestFile/" + result.myFile });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }


        [HttpPost("CallApi")]
        public IActionResult CallApi()
        {
            //try
            //{

            //    //a get request which does not have body
            //    var request = (HttpWebRequest)WebRequest.Create("https://panel.charitywatch.ir/api/projects/getprojects");
            //    request.ContentType = "application/json";
            //    request.Method = "GET";
            //    request.Headers.Add("Authorization", "");

            //    var response = (HttpWebResponse)request.GetResponse();

            //    var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            //    return Ok(new { errorId = 0, errorTitle = "", result = Newtonsoft.Json.JsonConvert.DeserializeObject(responseString) });

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            //}

            // a post request with body
            try
            {
                dynamic result = null;

                string apiUrl = "https://mobileofficeautomation.tehran.ir/RestWebApiDemo/api/ShowTitle/ShowFolder";
                var input = new
                {
                    AppId= 1,
                    IMEI= "9bf05ae9d4bdbfc925f6dcf18db1ff6d",
                    OrgCode= 762,
                    UserName= "motaharmobileuser",
                    folId= "891CFB0D-D107-40A8-8B8E-C36F801979B8"
                };
                string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(input);
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
                httpRequest.ContentType = "application/json";
                httpRequest.Method = "POST";
                httpRequest.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIxNzZBRjM0My0zNUExLTQ3NjAtOEMyRS00NUYwNTlGOUVDOEYiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zaWQiOiJDMDE4NzBBQi1GRjAwLTREMDEtQjFCRS05NDEzOTY0MkY0NjEiLCJuYmYiOjE2MjM1MjA4NDgsImV4cCI6MTYyMzU2NDA0OCwiaWF0IjoxNjIzNTIwODQ4LCJpc3MiOiJodHRwOi8vbG9jYWxob3N0L1Jlc3RXZWJBcGkiLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0L1Jlc3RXZWJBcGkifQ.2RqLtElJID9auNV1KG3mlQem-6jYdold9G05Pvvs5Ww");
                byte[] bytes = Encoding.UTF8.GetBytes(inputJson);
                using (Stream stream = httpRequest.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();
                }
                HttpWebResponse httpResponse;
                try
                {
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                catch (WebException ex)
                {
                    //string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                    httpResponse = (HttpWebResponse)ex.Response;
                }
                using (Stream stream = httpResponse.GetResponseStream())
                {
                    string json = (new StreamReader(stream)).ReadToEnd();
                    result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                }
                return Ok(new { errorId = 0, errorTitle = "", result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }

        }


        [HttpGet("RequestFiles")]
        public IActionResult RequestFiles([FromQuery(Name = "serviceRequestId")] string serviceRequestId)
        {
            try
            {
                var FilesList = _serviceRequestService.GetAllServiceRequestFile(serviceRequestId);
                return Ok(new { errorId = 0, errorTitle = "", result = FilesList });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }

        }

    }
}
