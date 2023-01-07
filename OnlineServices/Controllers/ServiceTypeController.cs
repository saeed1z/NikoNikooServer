using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
    public class ServiceTypeController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPersonService_Service _personService_Service;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IServiceTypeUnitPriceService _serviceTypeUnitPriceService;
        #endregion Fields

        #region Ctor
        public ServiceTypeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonService_Service personService_Service,
            IServiceTypeService serviceTypeService,
            IServiceTypeUnitPriceService serviceTypeUnitPriceService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._serviceTypeService = serviceTypeService;
            this._serviceTypeUnitPriceService = serviceTypeUnitPriceService;
            this._personService_Service = personService_Service;
        }
        #endregion Ctor

        #region Utilities
        [NonAction]
        protected virtual void PrepareServiceTypeUnitPriceModel(ServiceTypeUnitPriceModel model)
        {
            var serviceTypeList = _serviceTypeService.GetAll();
            if (serviceTypeList.Count > 0)
            {
                model.ServiceTypeList = serviceTypeList.Select(
                    st => new SelectListItem
                    {
                        Text = st.Title,
                        Value = st.Id.ToString(),
                        Selected = st.Id == model.ServiceTypeId ? true : false
                    }).ToList();
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

            var model = new ServiceTypeListModel();
            var serviceTypeList = new List<ServiceTypeModel>();
            IPagedList<ServiceType> serviceTypes = _serviceTypeService.GetPagedAll(page, pageSize);

            if (serviceTypes.Count > 0)
            {
                serviceTypeList = serviceTypes.Select((value, index) => new ServiceTypeModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    Title = value.Title,
                    Description = value.Description,
                    IsActive = value.IsActive,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = serviceTypes.PageSize,
                TotalRecords = serviceTypes.TotalCount,
                PageIndex = serviceTypes.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ServiceTypeListModel
            {
                ServiceTypeModel = serviceTypeList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }
        public IActionResult UnitPrice(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/PersonType/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new ServiceTypeUnitPriceListModel();
            var serviceTypeUnitPriceList = new List<ServiceTypeUnitPriceModel>();
            IPagedList<ServiceTypeUnitPrice> serviceTypeUnitPrices = _serviceTypeUnitPriceService.GetPagedAll(page, pageSize);

            if (serviceTypeUnitPrices.Count > 0)
            {
                serviceTypeUnitPriceList = serviceTypeUnitPrices.Select((value, index) => new ServiceTypeUnitPriceModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    ServiceTypeTitle = value.ServiceType != null ? value.ServiceType.Title : "",
                    Price = value.Price,
                    CreatedDate = value.CreatedDate != null ? BaseSettings.Gregorian2HijriSlashed(value.CreatedDate.Value) : "",
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = serviceTypeUnitPrices.PageSize,
                TotalRecords = serviceTypeUnitPrices.TotalCount,
                PageIndex = serviceTypeUnitPrices.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ServiceTypeUnitPriceListModel
            {
                ServiceTypeUnitPriceModel = serviceTypeUnitPriceList,
                PagerModel = pagerModel
            };
            return View("UnitPrice", model);
        }
        public IActionResult UnitPriceEdit(int id = 0)
        {
            var model = new ServiceTypeUnitPriceModel();
            ServiceTypeUnitPrice berviceTypeUnitPrice = null;

            if (id != 0)
            {
                berviceTypeUnitPrice = _serviceTypeUnitPriceService.GetById(id);
                if (berviceTypeUnitPrice == null)
                    return NotFound();

                model = new ServiceTypeUnitPriceModel()
                {
                    Id = berviceTypeUnitPrice.Id,
                    ServiceTypeId = berviceTypeUnitPrice.ServiceTypeId,
                    Price = berviceTypeUnitPrice.Price,
                    CreatedDate = berviceTypeUnitPrice.CreatedDate.ToString(),
                };
            }
            PrepareServiceTypeUnitPriceModel(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveUnitPrice(ServiceTypeUnitPriceModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var serviceTypeUnitPrice = new ServiceTypeUnitPrice();
            try
            {
                if (model.Id != 0)
                {
                    serviceTypeUnitPrice = _serviceTypeUnitPriceService.GetById(model.Id);
                    serviceTypeUnitPrice.ServiceTypeId = model.ServiceTypeId;
                    serviceTypeUnitPrice.Price = model.Price;
                    serviceTypeUnitPrice.UpdatedDate = DateTime.Now;
                    serviceTypeUnitPrice.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    serviceTypeUnitPrice = new ServiceTypeUnitPrice()
                    {
                        ServiceTypeId = model.ServiceTypeId,
                        Price = model.Price,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (model.Id != 0)
                    _serviceTypeUnitPriceService.UpdateAsync(serviceTypeUnitPrice);
                else
                    _serviceTypeUnitPriceService.CreateAsync(serviceTypeUnitPrice);

                return RedirectToAction("UnitPriceEdit", new { id = serviceTypeUnitPrice.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceTypeUnitPriceService.Delete(id);
            return RedirectToAction("UnitPrice");
        }
        #endregion Methods
    }
}
