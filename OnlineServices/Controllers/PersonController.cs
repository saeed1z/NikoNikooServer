using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Models;
using SMS = OnlineServices.Utilities.SMS;

namespace OnlineServices.Controllers
{
    public class PersonController : Controller
    {
        #region Fields
        private readonly ILogger _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPersonService _personService;
        private readonly IPersonTypeService _personTypeService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        private readonly IPersonService_Service _personService_Service;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IPersonCarService _personCarService;
        private readonly IPersonPackageService _personPackageService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        #endregion Fields

        #region Ctor
        public PersonController(ILoggerFactory loggerFactory,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonService personService,
            IPersonTypeService personTypeService,
            IServiceTypeService serviceTypeService,
            IStateService stateService,
            ICityService cityService,
            IPersonService_Service personService_Service,
            IServiceRequestService serviceRequestService,
            IWebHostEnvironment hostingEnvironment,
            IPersonCarService personCarService,
            IPersonPackageService personPackageService
            )
        {
            this._logger = loggerFactory.CreateLogger<PersonController>();
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._personService = personService;
            this._hostingEnvironment = hostingEnvironment;
            this._personTypeService = personTypeService;
            this._serviceTypeService = serviceTypeService;
            this._stateService = stateService;
            this._cityService = cityService;
            this._personService_Service = personService_Service;
            this._serviceRequestService = serviceRequestService;
            this._personCarService = personCarService;
            this._personPackageService = personPackageService;
        }
        #endregion Ctor

        #region Utilities
        [NonAction]
        protected virtual void PreparePersonModel(PersonModel model)
        {
            //Fill PersonTypeList
            var personTypeList = _personTypeService.GetAll(null, null, null, true);
            if (personTypeList.Count > 0)
            {
                var firstItem = new SelectListItem()
                {
                    Value = "0",
                    Text = "-- انتخاب کنید --",
                };
                model.PersonTypeList = personTypeList.Where(x => x.Id != 5 && x.Id != 6).Select(
                    pt => new SelectListItem
                    {
                        Text = pt.Title,
                        Value = pt.Id.ToString(),
                        Selected = pt.Id == model.PersonTypeId ? true : false
                    }).ToList();


                model.PersonTypeList.Insert(0, firstItem);
            }

            //Fill StateList
            var stateList = _stateService.GetAll();
            if (stateList.Count > 0)
            {
                var firstItem = new SelectListItem()
                {
                    Value = "0",
                    Text = "-- انتخاب کنید --",
                };
                model.StateList = stateList.Select(
                    s => new SelectListItem
                    {
                        Text = s.Title,
                        Value = s.Id.ToString(),
                        Selected = model.StateId != null ? s.Id == model.StateId ? true : false : false
                    }).ToList();

                model.StateList.Insert(0, firstItem);
            }

            //Fill ServiceTypeList
            var serviceTypeList = _serviceTypeService.GetAll();
            if (serviceTypeList.Count > 0)
            {
                var personServicesList = new List<PersonService>();
                if (!string.IsNullOrEmpty(model.Id))
                    personServicesList = _personService_Service.GetAll(System.Xml.XmlConvert.ToGuid(model.Id));

                model.ServiceTypeList = serviceTypeList.Select(
                    st => new SelectListItem
                    {
                        Text = st.Title,
                        Value = st.Id.ToString(),
                        Selected = personServicesList.Count > 0 ? personServicesList.Where(x => x.ServiceTypeId == st.Id).ToList().Count > 0 ? true : false : false
                    }).ToList();
            }

            //Fill CityList
            //if (!string.IsNullOrEmpty(model.Id))
            {
                if (model.CityId != null)
                {
                    var firstItem = new SelectListItem()
                    {
                        Value = "0",
                        Text = "-- انتخاب کنید --",
                    };
                    model.CityList = _cityService.GetAllCitisForState(model.StateId, model.CityId);
                    model.CityList.Insert(0, firstItem);
                }
            }
        }

        public ActionResult FillCity(int StateId)
        {
            var cities = _cityService.GetAllCitisForState(StateId, null);
            return Json(cities);
        }
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/PersonType/Index" });


            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new PersonListModel();
            var personList = new List<PersonModel>();
            IPagedList<Person> persons = _personService.GetPagedAll(page, pageSize);


            if (persons.Count > 0)
            {
                personList = persons.Select((value, index) => new PersonModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id.ToString(),
                    PersonTypeTitle = value.PersonType != null ? value.PersonType.Title : "",
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    EmployeeNo = value.EmployeeNo,
                    MobileNo = value.MobileNo,
                    IsActive = value.IsActive
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = persons.PageSize,
                TotalRecords = persons.TotalCount,
                PageIndex = persons.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new PersonListModel
            {
                PersonModel = personList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }
        public IActionResult Users(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/PersonType/Index" });


            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new UserListModel();
            var userList = new List<UserModel>();
            IPagedList<Person> persons = _personService.GetPagedAllUsers(page, pageSize);


            if (persons.Count > 0)
            {
                userList = persons.Select((value, index) => new UserModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id.ToString(),
                    PersonTypeTitle = value.PersonType != null ? value.PersonType.Title : "",
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    MobileNo = value.MobileNo,
                    StateTitle = value.State != null ? value.State.Title : "",
                    CityTitle = value.City != null ? value.City.Title : "",
                    IsActive = value.IsActive
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = persons.PageSize,
                TotalRecords = persons.TotalCount,
                PageIndex = persons.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new UserListModel
            {
                UserModel = userList,
                PagerModel = pagerModel
            };
            return View("Users", model);
        }
        public IActionResult Edit(string id)
        {
            var model = new PersonModel();
            Person person = null;

            if (!string.IsNullOrEmpty(id))
            {
                person = _personService.GetById(System.Xml.XmlConvert.ToGuid(id));
                if (person == null)
                    return NotFound();

                model = new PersonModel()
                {
                    Id = person.Id.ToString(),
                    PersonTypeId = person.PersonTypeId,
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
                    CooperationStartDate = person.CooperationStartDate != null ? BaseSettings.Gregorian2HijriSlashed(person.CooperationStartDate.Value).ToString() : "",
                    CooperationEndDate = person.CooperationEndDate != null ? BaseSettings.Gregorian2HijriSlashed(person.CooperationEndDate.Value).ToString() : "",
                    IsActive = person.IsActive,
                };
            }
            PreparePersonModel(model);
            return View(model);
        }
        public IActionResult UserDetail(string id)
        {
            var model = new UserModel();
            Person person = null;

            if (!string.IsNullOrEmpty(id))
            {
                person = _personService.GetById(System.Xml.XmlConvert.ToGuid(id));
                if (person == null)
                    return NotFound();

                model = new UserModel()
                {
                    Id = person.Id.ToString(),
                    PersonTypeId = person.PersonTypeId,
                    PersonTypeTitle = person.PersonType != null ? person.PersonType.Title : "",
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    NationalCode = person.NationalCode,
                    EmployeeNo = person.EmployeeNo,
                    StateId = person.StateId,
                    StateTitle = person.State != null ? person.State.Title : "",
                    CityId = person.CityId,
                    CityTitle = person.City != null ? person.City.Title : "",
                    MobileNo = person.MobileNo,
                    PhoneNo = person.PhoneNo,
                    Address = person.Address,
                    PostCode = person.PostCode,
                    Latitude = person.Latitude,
                    Longitude = person.Longitude,
                    CooperationStartDate = person.CooperationStartDate != null ? BaseSettings.Gregorian2HijriSlashed(person.CooperationStartDate.Value).ToString() : "",
                    CooperationEndDate = person.CooperationEndDate != null ? BaseSettings.Gregorian2HijriSlashed(person.CooperationEndDate.Value).ToString() : "",
                    IsActive = person.IsActive,
                    BirthDate = person.BirthDate,
                    BuildingFloor = person.BuildingFloor,
                    BuildingPlate = person.BuildingPlate,
                    BuildingUnit = person.BuildingUnit,
                    CityArea = person.CityArea,
                    EducationLevel = person.EducationLevel,
                    FatherName = person.FatherName,
                    Gender = person.Gender,
                    PersonCarList = _personCarService.GetAll(person.Id).Select(x => new PersonCarModel
                    {
                        ModelTitle = x.Model != null ? x.Model.Title : "",
                        PlaqueNo = x.PlaqueNo,
                        ChassisNo = x.ChassisNo,
                        Description = x.Description,
                    }).ToList(),
                    PersonPackageList = _personPackageService.GetAll(person.Id).Select(x => new PersonPackageModel
                    {
                        PackageTemplateTitle = x.PackageTemplate != null ? x.PackageTemplate.Title : "",
                        CreatedDate = x.CreatedDate != null ? BaseSettings.Gregorian2HijriSlashed(x.CreatedDate) : "",
                        PackageTemplateIsActive = x.PackageTemplate != null ? x.PackageTemplate.IsActive : false,
                        PackageTemplateDescription = x.PackageTemplate != null ? x.PackageTemplate.Description : "",
                        Price = x.Price,
                    }).ToList(),
                    ServiceRequestList = _serviceRequestService.GetAll(person.Id).Select(x => new ServiceRequestModel
                    {
                        ServiceTypeTitle = x.ServiceType != null ? x.ServiceType.Title : "",
                        RequestDateTime = x.RequestDateTime != null ? BaseSettings.Gregorian2HijriSlashed(x.RequestDateTime) : "",
                        Description = x.Description,
                    }).ToList()
                };
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PersonModel model)
        {
            if (!ModelState.IsValid)
            {
                PreparePersonModel(model);
                return View("Edit", model);
            }

            model.MobileNo = BaseSettings.ConvertDigitsToLatin(model.MobileNo);
            if (model.MobileNo.Length < 11)
            {
                ModelState.AddModelError(string.Empty, "شماره موبایل نامعتبر می باشد");
                PreparePersonModel(model);
                return View("Edit", model);
            }

            var normMobileNo = BaseSettings.NormilizeMobileNo(model.MobileNo);
            if (normMobileNo == "" || normMobileNo.Length < 11)
            {
                ModelState.AddModelError(string.Empty, "شماره موبایل نامعتبر می باشد");
                PreparePersonModel(model);
                return View("Edit", model);
            }

            var person = new Person();
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    person = _personService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));
                    person.FirstName = model.FirstName;
                    person.LastName = model.LastName;
                    person.NationalCode = model.NationalCode;
                    person.EmployeeNo = model.EmployeeNo;
                    person.StateId = model.StateId;
                    person.CityId = model.CityId;
                    person.MobileNo = model.MobileNo;
                    person.PhoneNo = model.PhoneNo;
                    person.Address = model.Address;
                    person.PostCode = model.PostCode;
                    person.CooperationStartDate = model.CooperationStartDate != null ? BaseSettings.ParseDate(model.CooperationStartDate) : null;
                    person.CooperationEndDate = model.CooperationEndDate != null ? BaseSettings.ParseDate(model.CooperationEndDate) : null;
                    person.IsActive = model.IsActive;
                    person.PersonTypeId = model.PersonTypeId;
                    //person.PersonService = model.PersonServiceIds.Count > 0 ? model.PersonServiceIds.Select(ps => new PersonService { ServiceTypeId = ps }).ToList() : null;
                    person.CreatedDate = DateTime.Now;
                    person.CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    var user = _userManager.FindByNameAsync(normMobileNo);
                    if (user.Result != null)
                    {
                        ModelState.AddModelError(string.Empty, "شماره موبایل تکراری می باشد");
                        PreparePersonModel(model);
                        return View("Edit", model);
                    }

                    person = new Person()
                    {
                        Id = Guid.NewGuid(),
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        NationalCode = model.NationalCode,
                        EmployeeNo = model.EmployeeNo,
                        StateId = model.StateId,
                        CityId = model.CityId,
                        MobileNo = model.MobileNo,
                        PhoneNo = model.PhoneNo,
                        Address = model.Address,
                        PostCode = model.PostCode,
                        CooperationStartDate = model.CooperationStartDate != null ? BaseSettings.ParseDate(model.CooperationStartDate) : null,
                        CooperationEndDate = model.CooperationEndDate != null ? BaseSettings.ParseDate(model.CooperationEndDate) : null,
                        IsActive = model.IsActive,
                        PersonTypeId = model.PersonTypeId,
                        //PersonService = model.PersonServiceIds.Count > 0 ? model.PersonServiceIds.Select(ps => new PersonService { ServiceTypeId = ps }).ToList() : null,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (!string.IsNullOrEmpty(model.Id))
                    _personService.UpdateAsync(person, model.PersonServiceIds);
                else
                {
                    _personService.CreateAsync(person, model.PersonServiceIds);

                    var newUser = new ApplicationUser()
                    {
                        UserName = normMobileNo,
                        PhoneNumber = normMobileNo,
                        Email = normMobileNo + "@OnlineServices.ir",
                        EmailConfirmed = false,
                        PhoneNumberConfirmed = false,
                    };
                    //var newUser = new ApplicationUser()
                    //{
                    //    UserName = "admin",
                    //    PhoneNumber = "99999999999",
                    //    Email = "admin" + "@OnlineServices.ir",
                    //    EmailConfirmed = true,
                    //    PhoneNumberConfirmed = true,
                    //};
                    var randomPass = BaseSettings.RandomText(6);
                    var result = _userManager.CreateAsync(newUser, randomPass);
                    SMS.Send(normMobileNo, randomPass);
                }

                PreparePersonModel(model);
                return RedirectToAction("Edit", new { id = person.Id.ToString() });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return View(model);
            }

        }
        public async Task<IActionResult> Delete(string id)
        {
            await _personService.Delete(System.Xml.XmlConvert.ToGuid(id));
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public JsonResult SavePersonAvatar([FromBody] PersonAvatarModel model)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string ImageFile = null;
                Guid ImageFileGuid = Guid.NewGuid();
                byte[] fileBinary = Convert.FromBase64String(model.Image);

                if (fileBinary != null && fileBinary.Length > 0)
                {
                    ImageFile = ImageFileGuid.ToString() + "." + model.ImageExtension.ToLower();
                    if (!Directory.Exists(Path.Combine(webRootPath, "Uploads/Person/")))
                        Directory.CreateDirectory(Path.Combine(webRootPath, "Uploads/Person/"));
                    var path = Path.Combine(Path.Combine(webRootPath, "Uploads/Person/"), ImageFile);
                    using (MemoryStream ms = new MemoryStream(fileBinary))
                    {
                        System.IO.File.WriteAllBytes(path, fileBinary);
                    }
                }

                var objPerson = _personService.GetById(System.Xml.XmlConvert.ToGuid(model.PersonId));
                objPerson.Avatar = ImageFileGuid.ToString() + "." + model.ImageExtension.ToLower();
                _personService.Update(objPerson);
                return Json(new { status = true, Avatar = ImageFileGuid.ToString() + "." + model.ImageExtension.ToLower() });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, error = ex.ToString() });
            }
        }

        #endregion Methods
    }
}
