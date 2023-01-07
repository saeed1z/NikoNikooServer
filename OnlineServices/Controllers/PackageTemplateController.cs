using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
    public class PackageTemplateController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPackageTemplateService _packageTemplateService;
        private readonly IPackageTemplateDetailService _packageTemplateDetailService;
        private readonly IPersonTypeService _personTypeService;
        private readonly IPersonPackageService _personPackageService;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceTypeUnitPriceService _serviceTypeUnitPriceService;
        #endregion Fields

        #region Ctor
        public PackageTemplateController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPackageTemplateService packageTemplateService,
            IPersonTypeService personTypeService,
            IPersonPackageService personPackageService,
            IServiceTypeService serviceTypeService,
            IServiceTypeUnitPriceService serviceTypeUnitPriceService,
            IPackageTemplateDetailService packageTemplateDetailService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._packageTemplateService = packageTemplateService;
            this._personTypeService = personTypeService;
            this._personPackageService = personPackageService;
            this._serviceTypeService = serviceTypeService;
            this._serviceTypeUnitPriceService = serviceTypeUnitPriceService;
            this._packageTemplateDetailService = packageTemplateDetailService;
        }
        #endregion Ctor

        #region Utilities

        [NonAction]
        protected virtual void PreparePackageTemplateModel(PackageTemplateModel model)
        {
            var personTypeList = _personTypeService.GetAll(null, null, true, null);
            if (personTypeList.Count > 0)
            {
                var firstItem = new SelectListItem()
                {
                    Value = "0",
                    Text = "-- انتخاب کنید --",
                };
                model.PersonTypeList = personTypeList.Select(
                    b => new SelectListItem
                    {
                        Text = b.Title,
                        Value = b.Id.ToString(),
                        Selected = b.Id == model.PersonTypeId ? true : false
                    }).ToList();
                model.PersonTypeList.Insert(0, firstItem);
            }
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

            var model = new PackageTemplateListModel();
            var packageTemplateList = new List<PackageTemplateModel>();
            IPagedList<PackageTemplate> packageTemplates = _packageTemplateService.GetPagedAll(page, pageSize);

            if (packageTemplates.Count > 0)
            {
                packageTemplateList = packageTemplates.Select((value, index) => new PackageTemplateModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id.ToString(),
                    Title = value.Title,
                    Description = value.Description,
                    PersonTypeId = value.PersonTypeId,
                    PersonTypeTitle = value.PersonType != null ? value.PersonType.Title : "",
                    RealPrice = value.RealPrice,
                    Price = value.Price,
                    ExpiredDuration = value.ExpiredDuration,
                    IsActive = value.IsActive,
                    HasAccessToDelete = _personPackageService.GetAllByPackageTemplateId(value.Id).Count > 0 ? false : true

                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = packageTemplates.PageSize,
                TotalRecords = packageTemplates.TotalCount,
                PageIndex = packageTemplates.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };




            model = new PackageTemplateListModel
            {
                PackageTemplateModel = packageTemplateList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(string id = "")
        {
            var model = new PackageTemplateModel();
            PackageTemplate packageTemplate = null;
            var RealPrice = 0;
            var serviceTypeList = _serviceTypeService.GetAll();
            var packageTemplateDetails = new List<PackageTemplateDetailModel>();
            if (!string.IsNullOrEmpty(id))
            {
                packageTemplate = _packageTemplateService.GetById(System.Xml.XmlConvert.ToGuid(id));
                if (packageTemplate == null)
                    return NotFound();


                if (serviceTypeList.Count > 0)
                {
                    foreach (var item in serviceTypeList)
                    {
                        var objPackageTemplateDetailItem =
                            _packageTemplateDetailService.GetByPackageTemplateIdAndServiceTypeId(
                                PackageTemplateId: packageTemplate.Id,
                                ServiceTypeId: item.Id);

                        var unitPrice = _serviceTypeUnitPriceService.GetByServiceTypeId(item.Id);

                        var packageTemplateDetailItem = new PackageTemplateDetailModel();
                        if (objPackageTemplateDetailItem != null)
                        {
                            packageTemplateDetailItem.Id = objPackageTemplateDetailItem.Id;
                            packageTemplateDetailItem.Quantity = objPackageTemplateDetailItem.Quantity;
                            RealPrice += (unitPrice != null ? Decimal.ToInt32(unitPrice.Price) : 0) * objPackageTemplateDetailItem.Quantity;
                        }
                        packageTemplateDetailItem.ServiceTypeUnitPrice = unitPrice != null ? unitPrice.Price : 0;
                        packageTemplateDetailItem.ServiceTypeId = item.Id;
                        packageTemplateDetailItem.ServiceTypeTitle = item.Title;
                        packageTemplateDetails.Add(packageTemplateDetailItem);
                    }

                }

                model = new PackageTemplateModel()
                {
                    Id = packageTemplate.Id.ToString(),
                    Title = packageTemplate.Title,
                    Description = packageTemplate.Description,
                    PersonTypeId = packageTemplate.PersonTypeId,
                    RealPrice = RealPrice,
                    Price = packageTemplate.Price,
                    ExpiredDuration = packageTemplate.ExpiredDuration,
                    IsActive = packageTemplate.IsActive,
                    PackageTemplateDetailList = packageTemplateDetails,
                    HasAccessToDelete = _personPackageService.GetAllByPackageTemplateId(packageTemplate.Id).Count > 0 ? false : true
                };
            }
            else
            {
                if (serviceTypeList.Count > 0)
                {
                    foreach (var item in serviceTypeList)
                    {
                        var unitPrice = _serviceTypeUnitPriceService.GetByServiceTypeId(item.Id);
                        var packageTemplateDetailItem = new PackageTemplateDetailModel();
                        packageTemplateDetailItem.Id = 0;
                        packageTemplateDetailItem.Quantity = 0;
                        packageTemplateDetailItem.ServiceTypeUnitPrice = unitPrice != null ? unitPrice.Price : 0;
                        packageTemplateDetailItem.ServiceTypeId = item.Id;
                        packageTemplateDetailItem.ServiceTypeTitle = item.Title;
                        packageTemplateDetails.Add(packageTemplateDetailItem);
                    }

                }
                model = new PackageTemplateModel()
                {
                    PackageTemplateDetailList = packageTemplateDetails,
                    HasAccessToDelete = true
                };
            }
            PreparePackageTemplateModel(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PackageTemplateModel model)
        {
            var objFormCollection = HttpContext.Request.ReadFormAsync();

            if (!ModelState.IsValid)
                return View(model);

            var packageTemplate = new PackageTemplate();
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    packageTemplate = _packageTemplateService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));
                    packageTemplate.Title = model.Title;
                    packageTemplate.Description = model.Description;
                    packageTemplate.IsActive = model.IsActive;
                    packageTemplate.PersonTypeId = model.PersonTypeId;
                    packageTemplate.Price = model.Price;
                    packageTemplate.RealPrice = model.RealPrice;
                    packageTemplate.ExpiredDuration = model.ExpiredDuration;
                    packageTemplate.UpdatedDate = DateTime.Now;
                    packageTemplate.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    packageTemplate = new PackageTemplate()
                    {
                        Id = Guid.NewGuid(),
                        Title = model.Title,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        PersonTypeId = model.PersonTypeId,
                        Price = model.Price,
                        RealPrice = model.RealPrice,
                        ExpiredDuration = model.ExpiredDuration,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User)),
                    };
                }
                List<PackageTemplateDetail> packageTemplateDetailList = model.PackageTemplateDetailList.Select(x => new PackageTemplateDetail()
                {
                    Id = x.Id,
                    PackageTemplateId = packageTemplate.Id,
                    ServiceTypeId = x.ServiceTypeId,
                    Quantity = x.Quantity,

                }).ToList();
                var RealPrice = 0;
                foreach (var item in packageTemplateDetailList)
                {
                    var unitPrice = _serviceTypeUnitPriceService.GetByServiceTypeId(item.ServiceTypeId);
                    RealPrice += (unitPrice != null ? Decimal.ToInt32(unitPrice.Price) : 0) * item.Quantity;
                }
                packageTemplate.RealPrice = RealPrice;

                if (!string.IsNullOrEmpty(model.Id))
                    _packageTemplateService.UpdateAsync(packageTemplate, packageTemplateDetailList);
                else
                    _packageTemplateService.CreateAsync(packageTemplate, packageTemplateDetailList);

                PreparePackageTemplateModel(model);
                return RedirectToAction("Edit", new { id = packageTemplate.Id.ToString() });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _packageTemplateService.Delete(System.Xml.XmlConvert.ToGuid(id));
            return RedirectToAction(nameof(Index));
        }
        #endregion Methods
    }
}
