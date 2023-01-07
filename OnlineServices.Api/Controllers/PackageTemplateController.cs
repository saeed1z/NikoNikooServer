using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Api.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class PackageTemplateController : ControllerBase
    {
        private readonly IPackageTemplateService _packageTemplateService;
        private readonly IPersonService _personService;
        private readonly IPersonPackageService _personPackageService;
        private readonly IPersonCarService _personCarService;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IPackageTemplateDetailService _packageTemplateDetailService;

        public PackageTemplateController(IPackageTemplateService packageTemplateService,
            IPersonService personService,
            IPersonCarService personCarService,
            IPersonPackageService personPackageService,
            IAspNetUserService aspNetUserService,
            IPackageTemplateDetailService packageTemplateDetailService)
        {
            _packageTemplateService = packageTemplateService;
            _personPackageService = personPackageService;
            _personCarService = personCarService;
            _personService = personService;
            _aspNetUserService = aspNetUserService;
            _packageTemplateDetailService = packageTemplateDetailService;
        }

        [HttpPost("PackageTemplateList")]
        public IActionResult PackageTemplateList()
        {
            var model = _packageTemplateService.GetAll().Select(pt => new
            {
                Id = pt.Id.ToString(),
                Title = pt.Title,
                Description = pt.Description,
                PersonTypeId = pt.PersonTypeId,
                PersonTypeTitle = pt.PersonType != null ? pt.PersonType.Title : "",
                RealPrice = pt.RealPrice,
                Price = pt.Price,
                ExpiredDuration = pt.ExpiredDuration,
                IsActive = pt.IsActive,
                PackageTemplateDetailList = _packageTemplateDetailService.GetAllByPackageTemplateId(pt.Id).Select(ptd => new
                {
                    Id = ptd.Id,
                    ServiceTypeId = ptd.ServiceTypeId,
                    ServiceTypeTitle = ptd.ServiceType != null ? ptd.ServiceType.Title : "",
                    Quantity = ptd.Quantity
                }).ToList()
            }).ToList();

            return Ok(new
            {
                errorId = "",
                errorTitle = "",
                result = model
            });
        }

        [HttpPost("BuyPackage")]
        public IActionResult BuyPackage([FromBody] PackageTemplateModel model)
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

            if (!_personService.IsFullProfileAsync(person).Result && person.PersonTypeId != (int)PersonTypeEnum.CommercialUser)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = new { IsComplete = false } });

            if (string.IsNullOrEmpty(model.Id))
                return Ok(new { errorId = 99, errorTitle = "بسته مورد نظر پیدا نشد", result = (string)null });

            var objPackageTemplate = _packageTemplateService.GetById(System.Xml.XmlConvert.ToGuid(model.Id));
            if (objPackageTemplate == null)
                return Ok(new { errorId = 99, errorTitle = "بسته مورد نظر پیدا نشد", result = (string)null });

            if (person.PersonType != objPackageTemplate.PersonType)
                return Ok(new { errorId = 99, errorTitle = "این بسته برای استفاده این نوع کاربر نمی باشد", result = (string)null });


            PersonCar objPersonCar = null;
            if (person.PersonTypeId == (int)PersonTypeEnum.NormalUser)
            {
                if (model.PersonCarId == null || model.PersonCarId == 0)
                    return Ok(new { errorId = 99, errorTitle = "انتخاب خودرو برای خرید این بسته الزامی می باشد", result = (string)null });

                objPersonCar = _personCarService.GetById(model.PersonCarId.Value);
                if (objPersonCar == null)
                    return Ok(new { errorId = 99, errorTitle = "شناسه خودرو معتبر نمی باشد", result = (string)null });
            }

            try
            {
                PersonPackage objPersonPackage = new PersonPackage()
                {
                    PersonId = person.Id,
                    PersonCarId = objPersonCar != null ? objPersonCar.Id : (int?)null,
                    PackageTemplateId = objPackageTemplate.Id,
                    Price = objPackageTemplate.Price,
                    FactorDate = DateTime.Now,
                    ExpiredDate = DateTime.Now.AddDays(objPackageTemplate.ExpiredDuration),
                    FactorNumber = 123456,
                    CreatedDate = DateTime.Now,
                    CreatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id)
                };

                _personPackageService.BuyPackage(objPersonPackage, objPackageTemplate.PackageTemplateDetail);

                return Ok(new { errorId = 0, errorTitle = "", result = "اطلاعات صحیح می باشد و باید به درگاه بانکی متصل شود. پکیج با موفقیت خریداری شد" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = 0 });
            }

        }

        //[HttpPost("PackageTemplateDetailList")]
        //public IActionResult PackageTemplateDetailList([FromBody] PackageTemplateDetailModel model)
        //{
        //    var retModel = new List<PackageTemplateDetailModel>();
        //    var packageTemplateDetailList = new List<PackageTemplateDetail>();
        //    if (!string.IsNullOrEmpty(model.PackageTemplateId))
        //    {
        //        bool isValid = Guid.TryParse(model.PackageTemplateId, out var guidOutput);
        //        if (isValid)
        //        {
        //            packageTemplateDetailList = _packageTemplateDetailService.GetAllByPackageTemplateId(System.Xml.XmlConvert.ToGuid(model.PackageTemplateId));
        //            retModel = packageTemplateDetailList.Select(pt => new PackageTemplateDetailModel
        //            {
        //                Id = pt.Id,
        //                PackageTemplateId = pt.PackageTemplateId.ToString(),
        //                PackageTemplateTitle = pt.PackageTemplate != null ? pt.PackageTemplate.Title : "",
        //                ServiceTypeId = pt.ServiceTypeId,
        //                ServiceTypeTitle = pt.ServiceType != null ? pt.ServiceType.Title : "",
        //                Quantity = pt.Quantity
        //            }).ToList();
        //        }
        //    }
        //    return Ok(new
        //    {
        //        status = true,
        //        errorId = "",
        //        errorTitle = "",
        //        result = retModel
        //    });
        //}
    }
}
