using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Models;
using OnlineServices.Utilities;

namespace OnlineServices.Controllers
{
    public class CommercialRequestController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ICommercialUserRequestServices _commercialUserRequestServices;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IPersonService _personService;
        #endregion Fields

        #region Ctor
        public CommercialRequestController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IPersonService personService,
            ICommercialUserRequestServices commercialUserRequestServices,
            IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._commercialUserRequestServices = commercialUserRequestServices;
            this.webHostEnvironment = hostEnvironment;
            this._personService = personService;
        }
        #endregion Ctor

        #region Utilities
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/CommercialRequest/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new CommercialUserRequestListModel();
            var commercialUserRequestList = new List<CommercialUserRequestModel>();
            IPagedList<CommercialUserRequest> commercialUserRequests = _commercialUserRequestServices.GetPagedAll(page, pageSize);

            if (commercialUserRequests.Count > 0)
            {
                commercialUserRequestList = commercialUserRequests.Select((value, index) => new CommercialUserRequestModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    Mobile = value.Person.MobileNo,
                    PersonId = value.PersonId.ToString(),
                    PersonName = value.Person.FirstName + " " + value.Person.LastName,
                    CompanyName = value.CompanyName,
                    RegistrationNumber = value.RegistrationNumber,
                    RegistrationDate = value.RegistrationDate,
                    RegistrationPlace = value.RegistrationPlace,
                    EconomicCode = value.EconomicCode,
                    WebsiteUrl = value.WebsiteUrl,
                    Address = value.Address,
                    Email = value.Email,
                    ActivityAddress = value.ActivityAddress,
                    InterfaceName = value.InterfaceName,
                    InterfaceFamily = value.InterfaceFamily,
                    Post = value.Post,
                    IsRejected = value.IsRejected,
                    IsAccepted = value.IsAccepted,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = commercialUserRequests.PageSize,
                TotalRecords = commercialUserRequests.TotalCount,
                PageIndex = commercialUserRequests.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new CommercialUserRequestListModel
            {
                CommercialUserRequestModel = commercialUserRequestList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(int id = 0)
        {
            var model = new CommercialUserRequestModel();
            if (id != 0)
            {
                CommercialUserRequest commercialUserRequest = _commercialUserRequestServices.GetById(id);
                if (commercialUserRequest == null)
                    return NotFound();

                model = new CommercialUserRequestModel()
                {
                    Id = commercialUserRequest.Id,
                    PersonId = commercialUserRequest.PersonId.ToString(),
                    PersonName = commercialUserRequest.Person.MobileNo,
                    CompanyName = commercialUserRequest.CompanyName,
                    RegistrationNumber = commercialUserRequest.RegistrationNumber,
                    RegistrationDate = commercialUserRequest.RegistrationDate,
                    RegistrationPlace = commercialUserRequest.RegistrationPlace,
                    EconomicCode = commercialUserRequest.EconomicCode,
                    WebsiteUrl = commercialUserRequest.WebsiteUrl,
                    Address = commercialUserRequest.Address,
                    Email = commercialUserRequest.Email,
                    ActivityAddress = commercialUserRequest.ActivityAddress,
                    InterfaceName = commercialUserRequest.InterfaceName,
                    InterfaceFamily = commercialUserRequest.InterfaceFamily,
                    Post = commercialUserRequest.Post,
                    IsRejected = commercialUserRequest.IsRejected,
                    IsAccepted = commercialUserRequest.IsAccepted,
                };
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(CommercialUserRequestModel model, string submit)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                Person person = _personService.GetById(Guid.Parse(model.PersonId));
                var commercialUserRequest = new CommercialUserRequest();
                switch (submit)
                {
                    case "Reject":
                        commercialUserRequest = _commercialUserRequestServices.GetById(model.Id);
                        commercialUserRequest.IsAccepted = false;
                        commercialUserRequest.IsRejected = true;
                        commercialUserRequest.RejectedReason = model.RejectedReason;
                        _commercialUserRequestServices.Update(commercialUserRequest);                       
                        NotificationHandler.SendToUser("کارمند",person.Notifykey, false, "درخواست شما برای کاربری تجاری رد شده است", new Dictionary<string, string>());
                        break;
                    case "Confirm":
                        commercialUserRequest = _commercialUserRequestServices.GetById(model.Id);
                        commercialUserRequest.IsAccepted = true;
                        commercialUserRequest.IsRejected = false;
                        _commercialUserRequestServices.Update(commercialUserRequest);

                        var objPerson = _personService.GetById(Guid.Parse(model.PersonId));
                        objPerson.PersonTypeId = (int)PersonTypeEnum.CommercialUser;
                        objPerson.CompanyName = model.CompanyName;
                        objPerson.RegistrationNumber = model.RegistrationNumber;
                        objPerson.RegistrationDate = model.RegistrationDate;
                        objPerson.RegistrationPlace = model.RegistrationPlace;
                        objPerson.EconomicCode = model.EconomicCode;
                        objPerson.WebsiteUrl = model.WebsiteUrl;
                        objPerson.Email = model.Email;
                        objPerson.ActivityAddress = model.ActivityAddress;
                        objPerson.InterfaceName = model.InterfaceName;
                        objPerson.InterfaceFamily = model.InterfaceFamily;
                        objPerson.Post = model.Post;
                        _personService.Update(objPerson);
                        NotificationHandler.SendToUser("کارمند", person.Notifykey, false, "درخواست شما برای کاربر تجاری تایید شد", new Dictionary<string, string>());
                        break;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                TempData["Message"] = ex.ToString();
                TempData["IsError"] = true;
                return View(model);
            }
        }

        #endregion Methods
    }
}
