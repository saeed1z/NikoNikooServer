using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Api.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using OnlineServices.Api.Helpers;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ICommercialUserRequestServices _commercialUserRequestServices;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<IdentityUser> _signInManager;
        public PersonController(IPersonService personService,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration,
            ICommercialUserRequestServices commercialUserRequestServices,
            IAspNetUserService aspNetUserService)
        {
            this._personService = personService;
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._configuration = configuration;
            this._aspNetUserService = aspNetUserService;
            this._commercialUserRequestServices = commercialUserRequestServices;
        }

        [HttpPost("PersonList")]
        public IActionResult PersonList()
        {
            var model = _personService.GetAll().Select(p => new PersonModel
            {
                Id = p.Id.ToString(),
                PersonTypeId = p.PersonTypeId,
                PersonTypeTitle = p.PersonType != null ? p.PersonType.Title : "",
                FirstName = p.FirstName,
                LastName = p.LastName,
                //NationalCode = p.NationalCode,
                //EmployeeNo = p.EmployeeNo,
                //StateId = p.StateId,
                //CityId = p.CityId,
                MobileNo = p.MobileNo,
                //PhoneNo = p.PhoneNo,
                //Address = p.Address,
                //PostCode = p.PostCode,
                //Latitude = p.Latitude,
                //Longitude = p.Longitude,
                //CooperationStartDate = p.CooperationStartDate.ToString(),
                //CooperationEndDate = p.CooperationEndDate.ToString(),
                IsActive = p.IsActive,
                Notifykey = p.Notifykey,
                HasAvatar = !string.IsNullOrEmpty(p.Avatar),
            });
            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = model
            });
        }

        [HttpPost("GetPersonById")]
        public IActionResult GetPersonById()
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


            PersonModel retModel = null;
            try
            {
                if (person != null)
                {
                    var commercialStatusCode = 0;
                    var commercialStatus = "";
                    //if (person.PersonTypeId == (int)PersonTypeEnum.NormalUser)
                    //{
                    var commercialUserRequest = _commercialUserRequestServices.GetByPersonId(person.Id);
                    if (commercialUserRequest != null)
                    {
                        if (commercialUserRequest.IsAccepted && !commercialUserRequest.IsRejected)
                        {
                            commercialStatus = "درخواست برای کاربر تجاری شما تایید شده است";
                            commercialStatusCode = 1;
                        }
                        else if (!commercialUserRequest.IsAccepted && commercialUserRequest.IsRejected)
                        {
                            commercialStatus = "درخواست برای کاربر تجاری شما رد شده است";
                            commercialStatusCode = 2;
                        }
                        else
                        {
                            commercialStatus = "درخواست برای کاربر تجاری شما در حال بررسی است";
                            commercialStatusCode = 3;
                        }
                    }
                    else
                        commercialStatus = "درخواستی برای کاربر تجاری موجود نمی باشد";
                    //}


                    retModel = new PersonModel
                    {
                        Id = person.Id.ToString(),
                        PersonTypeId = person.PersonTypeId,
                        PersonTypeTitle = person.PersonType != null ? person.PersonType.Title : "",
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        NationalCode = person.NationalCode,
                        EmployeeNo = person.EmployeeNo,
                        StateId = person.StateId,
                        CityId = person.CityId,
                        MobileNo = person.MobileNo,
                        PhoneNo = person.PhoneNo,
                        Address = person.Address,
                        PostCode = person.PostCode,
                        Latitude = person.Latitude,
                        Longitude = person.Longitude,
                        CooperationStartDate = person.CooperationStartDate.ToString(),
                        CooperationEndDate = person.CooperationEndDate.ToString(),
                        IsActive = person.IsActive,
                        Notifykey = person.Notifykey,
                        HasAvatar = !string.IsNullOrEmpty(person.Avatar),
                        CommercialStatus = commercialStatus,
                        CommercialStatusCode = commercialStatusCode,
                        IsCompelete = _personService.IsFullProfileAsync(person).Result,
                        //BirthDate = person.BirthDate.ToShamsi(),
                        Email = person.Email,
                        Gender = person.Gender,
                        BuildingPlate = person.BuildingPlate,
                        FatherName = person.FatherName,
                        EducationLevel = person.EducationLevel,
                        BuildingFloor = person.BuildingFloor,
                        BuildingUnit = person.BuildingUnit,
                        CityArea = person.CityArea,
                        BirthDate = person.BirthDate

                    };

                    return Ok(new
                    {
                        errorId = 0,
                        errorTitle = "",
                        result = retModel
                    });
                }
                else
                {
                    return Ok(new
                    {
                        errorId = 15,
                        errorTitle = "کاربر یافت نشد",
                        result = retModel
                    });
                }

            }
            catch (System.Exception ex)
            {
                return BadRequest(new
                {
                    errorId = 99,
                    errorTitle = ex.ToString(),
                    result = retModel
                });
            }
        }

        [HttpPost("SavePerson")]
        public IActionResult SavePerson([FromBody] PersonModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            Person person = new Person();
            Person newPerson = new Person();
            try
            {
                person = _personService.GetByMobileNo(currentUser.UserName);
                if (person != null)
                {
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.NationalCode = model.NationalCode != null ? model.NationalCode : person.NationalCode;
                    person.EmployeeNo = model.EmployeeNo != null ? model.EmployeeNo : person.EmployeeNo;
                    person.StateId = model.StateId != 0 ? model.StateId : person.StateId;
                    person.CityId = model.CityId != 0 ? model.CityId : person.CityId;
                    person.MobileNo = person.MobileNo;
                    person.PhoneNo = model.PhoneNo != null ? model.PhoneNo : person.PhoneNo;
                    person.Address = model.Address != null ? model.Address : person.Address;
                    person.PostCode = model.PostCode != null ? model.CityId : person.CityId;
                    person.CooperationStartDate = model.CooperationStartDate != null ? BaseSettings.ParseDate(model.CooperationStartDate) : person.CooperationStartDate;
                    person.CooperationEndDate = model.CooperationEndDate != null ? BaseSettings.ParseDate(model.CooperationEndDate) : person.CooperationEndDate;
                    person.IsActive = person.IsActive;
                    person.PersonTypeId = model.PersonTypeId != 0 ? model.PersonTypeId : person.PersonTypeId;
                    person.UpdatedDate = DateTime.Now;
                    person.UpdatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id);
                    person.BirthDate = model.BirthDate;
                    person.StateId = model.StateId;
                    person.CityId = model.CityId;
                    person.FatherName = model.FatherName;
                    person.EducationLevel = model.EducationLevel;
                    person.BuildingFloor = model.BuildingFloor;
                    person.CityArea = model.CityArea;
                    person.BuildingPlate = model.BuildingPlate;
                    person.BuildingUnit = model.BuildingUnit;
                    person.Email = model.Email;
                    person.Gender = model.Gender;
                }
                else
                {
                    newPerson = new Person()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        NationalCode = model.NationalCode != null ? model.NationalCode : null,
                        EmployeeNo = model.EmployeeNo != null ? model.EmployeeNo : null,
                        StateId = model.StateId != 0 ? model.StateId : null,
                        CityId = model.CityId != 0 ? model.CityId : null,
                        MobileNo = currentUser.UserName,
                        PhoneNo = model.PhoneNo ?? null,
                        Address = model.Address ?? null,
                        PostCode = model.PostCode ?? null,
                        CooperationStartDate = model.CooperationStartDate != null ? BaseSettings.ParseDate(model.CooperationStartDate) : null,
                        CooperationEndDate = model.CooperationEndDate != null ? BaseSettings.ParseDate(model.CooperationEndDate) : null,
                        IsActive = true,
                        PersonTypeId = model.PersonTypeId == null || model.PersonTypeId == 0 ? (byte)PersonTypeEnum.NormalUser : model.PersonTypeId,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id),
                        BuildingFloor = model.BuildingFloor,
                        BuildingUnit = model.BuildingUnit,
                        BuildingPlate = model.BuildingPlate,
                        Email = model.Email,
                        CityArea = model.CityArea,
                        EducationLevel = model.EducationLevel,
                        Gender = model.Gender,
                        FatherName = model.FatherName,
                        BirthDate = model.BirthDate

                    };
                }

                if (person != null)
                {
                    _personService.UpdateAsync(person, null);
                    return Ok(new
                    {
                        errorId = 0,
                        errorTitle = "",
                        result = new
                        {
                            personId = person.Id.ToString(),
                            isCompeleted = _personService.IsFullProfileAsync(person).Result
                        }
                    });
                }
                else
                {
                    _personService.CreateAsync(newPerson, null);
                    return Ok(new
                    {
                        errorId = 0,
                        errorTitle = "",
                        result = new
                        {
                            personId = newPerson.Id.ToString(),
                            isCompeleted = _personService.IsFullProfileAsync(newPerson).Result
                        }
                    });
                }

            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("UpdateAvatar")]
        public IActionResult UpdateAvatar([FromBody] AvatarModel model)
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
                dynamic result = null;
                if (!string.IsNullOrEmpty(model.Image))
                {
                    string apiUrl = $"{_configuration.GetValue<string>("HostAddress:Local")}/Person/SavePersonAvatar";
                    var input = new
                    {
                        PersonId = person.Id.ToString(),
                        Image = model.Image.ToString(),
                        ImageExtension = model.ImageExtension.ToString(),
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
                        string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
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
                return Ok(new { errorId = 0, errorTitle = "", result = PanelUrl + "Uploads/Person/" + result.avatar });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("UpdateLocation")]
        public IActionResult UpdateLocation([FromBody] PersonModel model)
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

            if (!model.Latitude.HasValue || !model.Longitude.HasValue ||
                model.Latitude == 0 || model.Longitude == 0)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات موقعیت مکانی کامل نیست", result = (string)null });

            try
            {
                person.Latitude = model.Latitude;
                person.Longitude = model.Longitude;
                _personService.UpdateAsync(person, null);

                return Ok(new { errorId = 0, errorTitle = "", result = person.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("UpdateNotifykey")]
        public IActionResult UpdateNotifykey([FromBody] PersonModel model)
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
                person.Notifykey = model.Notifykey;
                _personService.UpdateAsync(person, null);

                return Ok(new { errorId = 0, errorTitle = "", result = person.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpGet("GetPersonAvatar")]
        public IActionResult GetPersonAvatar()
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
                if (string.IsNullOrEmpty(person.Avatar))
                    return Ok(new { errorId = 99, errorTitle = "شخص عکسی ندارد", result = (string)null });

                string PanelUrl = _configuration.GetSection("PanelUrl").Value;
                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = PanelUrl + "Uploads/Person/" + person.Avatar
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("RequestForCommercial")]
        public IActionResult RequestForCommercial([FromBody] CommercialUserRequestModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            IdentityUser currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            Person person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            var commercialRequest = _commercialUserRequestServices.GetByUnCheckRequest(person.Id);
            if (commercialRequest != null)
                return Ok(new { errorId = 99, errorTitle = "شما قبلا یک درخواست بررسی نشده دارید", result = (string)null });

            try
            {
                var objCommercialUserRequest = new CommercialUserRequest
                {
                    PersonId = person.Id,
                    CompanyName = model.CompanyName,
                    RegistrationNumber = model.RegistrationNumber,
                    RegistrationDate = model.RegistrationDate,
                    RegistrationPlace = model.RegistrationPlace,
                    EconomicCode = model.EconomicCode,
                    WebsiteUrl = model.WebsiteUrl,
                    Email = model.Email,
                    ActivityAddress = model.ActivityAddress,
                    InterfaceName = model.InterfaceName,
                    InterfaceFamily = model.InterfaceFamily,
                    Post = model.Post,
                    IsAccepted = false,
                    IsRejected = false,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id)
                };
                _commercialUserRequestServices.Create(objCommercialUserRequest);

                return Ok(new { errorId = 0, errorTitle = "", result = person.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

    }
}
