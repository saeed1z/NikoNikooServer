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
    public class ServiceTypeQuestionController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IServiceTypeService _serviceTypeService;
        private readonly IWebHostEnvironment webHostEnvironment;
        #endregion Fields

        #region Ctor
        public ServiceTypeQuestionController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IServiceTypeService serviceTypeService,
            IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._serviceTypeService = serviceTypeService;
            this.webHostEnvironment = hostEnvironment;
        }
        #endregion Ctor

        #region Utilities
        [NonAction]
        protected virtual void PrepareServiceTypeQuestionModel(ServiceTypeQuestionModel model)
        {
            //Fill BrandList
            var ServiceTypeList = _serviceTypeService.GetAll();
            if (ServiceTypeList.Count > 0)
            {
                var firstItem = new SelectListItem()
                {
                    Value = "0",
                    Text = "-- انتخاب کنید --",
                };
                model.ServiceTypeList = ServiceTypeList.Select(
                    b => new SelectListItem
                    {
                        Text = b.Title,
                        Value = b.Id.ToString(),
                        Selected = b.Id == model.ServiceTypeId ? true : false
                    }).ToList();
                model.ServiceTypeList.Insert(0, firstItem);
            }
        }
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/ServiceTypeQuestion/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new ServiceTypeQuestionListModel();
            var serviceTypeQuestionList = new List<ServiceTypeQuestionModel>();
            IPagedList<ServiceTypeQuestion> serviceTypeQuestions = _serviceTypeService.GetServiceTypeQuestionPagedAll(page, pageSize);

            if (serviceTypeQuestions.Count > 0)
            {
                serviceTypeQuestionList = serviceTypeQuestions.Select((value, index) => new ServiceTypeQuestionModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    ServiceTypeId = value.ServiceTypeId,
                    ServiceTypeTitle = value.ServiceType != null ? value.ServiceType.Title : "",
                    OrderNo = value.OrderNo != null ? value.OrderNo : (byte)99,
                    Title = value.Title,
                    Description = value.Description,
                    IsActive = value.IsActive,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = serviceTypeQuestions.PageSize,
                TotalRecords = serviceTypeQuestions.TotalCount,
                PageIndex = serviceTypeQuestions.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ServiceTypeQuestionListModel
            {
                ServiceTypeQuestionModel = serviceTypeQuestionList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(int id = 0)
        {
            var model = new ServiceTypeQuestionModel();
            ServiceTypeQuestion serviceTypeQuestion = null;

            if (id != 0)
            {
                serviceTypeQuestion = _serviceTypeService.GetServiceTypeQuestionById(id);
                if (serviceTypeQuestion == null)
                    return NotFound();

                model = new ServiceTypeQuestionModel()
                {
                    Id = serviceTypeQuestion.Id,
                    ServiceTypeId = serviceTypeQuestion.ServiceTypeId,
                    Title = serviceTypeQuestion.Title,
                    OrderNo = serviceTypeQuestion.OrderNo,
                    Description = serviceTypeQuestion.Description,
                    IsActive = serviceTypeQuestion.IsActive,
                };
            }
            PrepareServiceTypeQuestionModel(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ServiceTypeQuestionModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string Message = null;

            var serviceTypeQuestion = new ServiceTypeQuestion();
            try
            {
                if (model.Id != 0)
                {
                    serviceTypeQuestion = _serviceTypeService.GetServiceTypeQuestionById(model.Id);
                    serviceTypeQuestion.Title = model.Title;
                    serviceTypeQuestion.ServiceTypeId = model.ServiceTypeId;
                    serviceTypeQuestion.Description = model.Description;
                    serviceTypeQuestion.OrderNo = model.OrderNo;
                    serviceTypeQuestion.IsActive = model.IsActive;
                    serviceTypeQuestion.UpdatedDate = DateTime.Now;
                    serviceTypeQuestion.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    serviceTypeQuestion = new ServiceTypeQuestion()
                    {
                        Title = model.Title,
                        ServiceTypeId = model.ServiceTypeId,
                        OrderNo = model.OrderNo,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (model.Id != 0)
                {
                    _serviceTypeService.UpdateServiceTypeQuestion(serviceTypeQuestion);
                    Message = "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    _serviceTypeService.CreateServiceTypeQuestion(serviceTypeQuestion);
                    Message = "ذخیره اطلاعات با موفقیت انجام شد";
                }

                TempData["IsError"] = false;
                TempData["Message"] = Message.ToString();
                return RedirectToAction("Edit", new { id = serviceTypeQuestion.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                TempData["Message"] = ex.ToString();
                TempData["IsError"] = true;
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceTypeService.DeleteServiceTypeQuestion(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion Methods
    }
}
