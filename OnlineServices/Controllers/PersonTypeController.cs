using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineServices.Core;
using OnlineServices.Entity;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class PersonTypeController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IPersonTypeService _personTypeService;
        #endregion Fields

        #region Ctor
        public PersonTypeController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonTypeService personTypeService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._personTypeService = personTypeService;
        }
        #endregion Ctor

        #region Utilities
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

            var model = new PersonTypeListModel();
            var personTypeList = new List<PersonTypeModel>();
            IPagedList<PersonType> personTypes = _personTypeService.GetPagedAll(null, null, null, null, page, pageSize);

            if (personTypes.Count > 0)
            {
                personTypeList = personTypes.Select((value, index) => new PersonTypeModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    Title = value.Title,
                    Description = value.Description,
                    PanelAccess = value.PanelAccess,
                    ServiceAppAccess = value.ServiceAppAccess,
                    UserAppAccess = value.UserAppAccess,
                    IsActive = value.IsActive,
                    IsReserved = value.IsReserved
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = personTypes.PageSize,
                TotalRecords = personTypes.TotalCount,
                PageIndex = personTypes.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new PersonTypeListModel
            {
                PersonTypeModel = personTypeList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }
        #endregion Methods
    }
}
