using System;
using System.Collections.Generic;
using System.Diagnostics;
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
//using OnlineServices.Core.Interfaces;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class ServiceCenterController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceCenterService _serviceCenterService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IBaseService _baseService;
        private readonly IStateService _stateService;
        private readonly ICityService _cityService;
        #endregion Fields

        #region Ctor
        public ServiceCenterController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IServiceCenterService serviceCenterService,
            IBaseService baseService,
            IStateService stateService,
            ICityService cityService,
            IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._stateService = stateService;
            this._cityService = cityService;
            this._baseService = baseService;
            this._serviceCenterService = serviceCenterService;
            this.webHostEnvironment = hostEnvironment;
        }
        #endregion Ctor

        #region Utilities
        [NonAction]
        protected virtual void PrepareServiceCenterModel(ServiceCenterModel model)
        {
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

            var carwashBaseList = _baseService.GetAll(Convert.ToInt32(BaseKindEnum.TypesOfCarWashServices));
            if (carwashBaseList.Count > 0)
                model.CarwashBaseList = carwashBaseList.Select((value, index) => new SelectListItem
                {
                    Value = value.Id.ToString(),
                    Text = value.Title,
                    Selected = !string.IsNullOrEmpty(model.Id) ? _serviceCenterService.GetServiceCenterDetail(System.Xml.XmlConvert.ToGuid(model.Id), value.Id) != null ? true : false : false
                }).ToList();

            var mechanicBaseList = _baseService.GetAll(Convert.ToInt32(BaseKindEnum.TypesOfRepairServices));
            if (mechanicBaseList.Count > 0)
                model.MechanicBaseList = mechanicBaseList.Select((value, index) => new SelectListItem
                {
                    Value = value.Id.ToString(),
                    Text = value.Title,
                    Selected = !string.IsNullOrEmpty(model.Id) ? _serviceCenterService.GetServiceCenterDetail(System.Xml.XmlConvert.ToGuid(model.Id), value.Id) != null ? true : false : false
                }).ToList();

            var serviceBaseList = _baseService.GetAll(Convert.ToInt32(BaseKindEnum.TypesOfCarServiceCenters));
            if (serviceBaseList.Count > 0)
                model.ServiceBaseList = serviceBaseList.Select((value, index) => new SelectListItem
                {
                    Value = value.Id.ToString(),
                    Text = value.Title,
                    Selected = !string.IsNullOrEmpty(model.Id) ? _serviceCenterService.GetServiceCenterDetail(System.Xml.XmlConvert.ToGuid(model.Id), value.Id) != null ? true : false : false
                }).ToList();

            var accessoryBaseList = _baseService.GetAll(Convert.ToInt32(BaseKindEnum.TypesOfAccessoryCenters));
            if (accessoryBaseList.Count > 0)
                model.AccessoryBaseList = accessoryBaseList.Select((value, index) => new SelectListItem
                {
                    Value = value.Id.ToString(),
                    Text = value.Title,
                    Selected = !string.IsNullOrEmpty(model.Id) ? _serviceCenterService.GetServiceCenterDetail(System.Xml.XmlConvert.ToGuid(model.Id), value.Id) != null ? true : false : false
                }).ToList();
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
                return RedirectToAction("Login", "Account", new { returnUrl = "/ServiceCenter/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new ServiceCenterListModel();
            var serviceCenterList = new List<ServiceCenterModel>();
            IPagedList<ServiceCenter> serviceCenters = _serviceCenterService.GetPagedAll(page, pageSize);

            if (serviceCenters.Count > 0)
            {
                serviceCenterList = serviceCenters.Select((value, index) => new ServiceCenterModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id.ToString(),
                    Title = value.Title,
                    StateId = value.StateId,
                    StateTitle = value.State != null ? value.State.Title : "",
                    CityId = value.CityId,
                    CityTitle = value.City != null ? value.City.Title : "",
                    MobileNo = value.MobileNo,
                    PhoneNo = value.PhoneNo,
                    FirstName = value.FirstName,
                    LastName = value.LastName,
                    IsAccessory = value.IsAccessory,
                    IsCarwash = value.IsCarwash,
                    IsMechanic = value.IsMechanic,
                    IsService = value.IsService,
                    IsActive = value.IsActive,
                }).ToList();
            }

            var pagerModel = new PagerModel
            {
                PageSize = serviceCenters.PageSize,
                TotalRecords = serviceCenters.TotalCount,
                PageIndex = serviceCenters.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ServiceCenterListModel
            {
                ServiceCenterModel = serviceCenterList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(string id = "")
        {
            var model = new ServiceCenterModel();
            ServiceCenter serviceCenter = null;


            if (!string.IsNullOrEmpty(id))
            {
                serviceCenter = _serviceCenterService.GetById(System.Xml.XmlConvert.ToGuid(id));
                if (serviceCenter == null)
                    return NotFound();

                model = new ServiceCenterModel()
                {
                    Id = serviceCenter.Id.ToString(),
                    Title = serviceCenter.Title,
                    FirstName = serviceCenter.FirstName,
                    LastName = serviceCenter.LastName,
                    NationalCode = serviceCenter.NationalCode,
                    StateId = serviceCenter.StateId,
                    CityId = serviceCenter.CityId,
                    MobileNo = serviceCenter.MobileNo,
                    PhoneNo = serviceCenter.PhoneNo,
                    Address = serviceCenter.Address,
                    PostCode = serviceCenter.PostCode,
                    Latitude = serviceCenter.Latitude,
                    Longitude = serviceCenter.Longitude,
                    IsActive = serviceCenter.IsActive,
                    Avatar = serviceCenter.Avatar,
                    Email = serviceCenter.Email,
                    IsCarwash = serviceCenter.IsCarwash,
                    IsMechanic = serviceCenter.IsMechanic,
                    IsService = serviceCenter.IsService,
                    IsAccessory = serviceCenter.IsAccessory
                };
            }
            PrepareServiceCenterModel(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ServiceCenterModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool IsError = false;
            string Message = null;

            string uniqueFileName = null;

            var serviceCenter = new ServiceCenter();
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    serviceCenter = _serviceCenterService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));

                    if (model.AvatarImage != null)
                    {
                        uniqueFileName = UploadedFile(model);

                        if (serviceCenter.Avatar != null)
                        {
                            var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "ServiceCenter", serviceCenter.Avatar);
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);
                        }
                    }

                    serviceCenter.Title = model.Title;
                    serviceCenter.IsActive = model.IsActive;
                    serviceCenter.IsActive = model.IsActive;
                    serviceCenter.Avatar = model.AvatarImage != null ? uniqueFileName : serviceCenter.Avatar;
                    serviceCenter.UpdatedDate = DateTime.Now;
                    serviceCenter.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    if (model.AvatarImage != null)
                        uniqueFileName = UploadedFile(model);

                    serviceCenter = new ServiceCenter()
                    {
                        Title = model.Title,
                        IsActive = model.IsActive,
                        Avatar = uniqueFileName,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (!string.IsNullOrEmpty(model.Id))
                {
                    _serviceCenterService.UpdateAsync(serviceCenter);
                    Message = "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    _serviceCenterService.CreateAsync(serviceCenter);
                    Message = "ذخیره اطلاعات با موفقیت انجام شد";
                }

                TempData["IsError"] = false;
                TempData["Message"] = Message.ToString();
                return RedirectToAction("Edit", new { id = serviceCenter.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                TempData["Message"] = ex.ToString();
                TempData["IsError"] = true;
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceCenterService.Delete(System.Xml.XmlConvert.ToGuid(id));
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(ServiceCenterModel model)
        {
            string uniqueFileName = null;

            if (model.AvatarImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "ServiceCenter");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(model.AvatarImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.AvatarImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion Methods
    }
}
