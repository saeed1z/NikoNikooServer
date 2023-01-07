using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineServices.Core;
using OnlineServices.Entity;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class BaseController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBaseService _baseService;
        private readonly IWebHostEnvironment webHostEnvironment;
        #endregion Fields

        #region Ctor
        public BaseController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IBaseService baseService,
            IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._baseService = baseService;
            this.webHostEnvironment = hostEnvironment;
        }
        #endregion Ctor

        #region Utilities
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0, int BaseKindId = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Base/Index/" + BaseKindId + "" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new BaseListModel();
            var BaseList = new List<BaseModel>();
            IPagedList<Base> Bases = _baseService.GetPagedAll(BaseKindId, page, pageSize);

            if (Bases.Count > 0)
            {
                BaseList = Bases.Select((value, index) => new BaseModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    Title = value.Title,
                    Description = value.Description,
                    BaseKindId = value.BaseKindId,
                    BaseKindTitle = value.BaseKind != null ? value.BaseKind.Title : "",
                    IsActive = value.IsActive,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = Bases.PageSize,
                TotalRecords = Bases.TotalCount,
                PageIndex = Bases.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new BaseListModel
            {
                BaseModel = BaseList,
                PagerModel = pagerModel,
                BaseKindId = BaseKindId
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(int id = 0, int BaseKindId = 0)
        {
            var model = new BaseModel();
            Base Base = null;

            if (id != 0)
            {
                Base = _baseService.GetById(id);
                if (Base == null)
                    return NotFound();

                model = new BaseModel()
                {
                    Id = Base.Id,
                    Title = Base.Title,
                    Description = Base.Description,
                    IsActive = Base.IsActive,
                };
            }
            model.BaseKindId = Convert.ToInt16(BaseKindId);
            //be change
            if(BaseKindId == Convert.ToInt32(BaseKindEnum.TechnicalSpecificationItems))
                model.BaseKindTitle = "آیتم های مشخصات فنی";
            else if (BaseKindId == Convert.ToInt32(BaseKindEnum.TypesOfCarWashServices))
                model.BaseKindTitle = "انواع خدمات کارواش";

            else if (BaseKindId == Convert.ToInt32(BaseKindEnum.TypesOfCarWashServicesWithOutWater))
                model.BaseKindTitle = "کارواش بدون آب";
            else if (BaseKindId == Convert.ToInt32(BaseKindEnum.TypesOfCarWashServicesWithWater))
                model.BaseKindTitle = "کارواش با آب";

            else if (BaseKindId == Convert.ToInt32(BaseKindEnum.TypesOfRepairServices))
                model.BaseKindTitle = "انواع خدمات تعمیرگاه";
            else if (BaseKindId == Convert.ToInt32(BaseKindEnum.TypesOfCarServiceCenters))
                model.BaseKindTitle = "انواع خدمات مراکز خدمات خودرو";
            else if (BaseKindId == Convert.ToInt32(BaseKindEnum.TypesOfAccessoryCenters))
                model.BaseKindTitle = "انواع خدمات مراکز خدمات خودرو";

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(BaseModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string Message = null;

            var Base = new Base();
            try
            {
                if (model.Id != 0)
                {
                    Base = _baseService.GetById(model.Id);

                    Base.Title = model.Title;
                    Base.Description = model.Description;
                    Base.IsActive = model.IsActive;
                    Base.BaseKindId = model.BaseKindId;
                    Base.UpdatedDate = DateTime.Now;
                    Base.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    Base = new Base()
                    {
                        Title = model.Title,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        BaseKindId = model.BaseKindId,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (model.Id != 0)
                {
                    _baseService.UpdateAsync(Base);
                    Message = "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                {
                    _baseService.CreateAsync(Base);
                    Message = "ذخیره اطلاعات با موفقیت انجام شد";
                }

                return RedirectToAction("Edit", new { id = Base.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return RedirectToAction("Edit", new { id = Base.Id });
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _baseService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion Methods
    }
}
