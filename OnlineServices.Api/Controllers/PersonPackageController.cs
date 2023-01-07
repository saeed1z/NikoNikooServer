using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using Microsoft.Extensions.Logging;
using OnlineServices.Api.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class PersonPackageController : ControllerBase
    {
        private readonly IPersonPackageService _personPackageService;
        private readonly IPersonPackageDetailService _personPackageDetailService;
        private readonly IPersonService _personService;
        private readonly IPersonCarService _personCarService;
        private readonly IAspNetUserService _aspNetUserService;

        public PersonPackageController(IPersonPackageService personPackageService,
            IPersonService personService,
            IPersonCarService personCarService,
            IPersonPackageDetailService personPackageDetailService,
            IAspNetUserService aspNetUserService)
        {
            _personPackageService = personPackageService;
            _personPackageDetailService = personPackageDetailService;
            _personService = personService;
            _personCarService = personCarService;
            _aspNetUserService = aspNetUserService;
        }

        [HttpPost("PersonPackageList")]
        public IActionResult PersonPackageList()
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

            var personPackageList = _personPackageService.GetAll(PersonId: person.Id);
            var packages = new List<PersonPackageModel>();
            foreach (var item in personPackageList)
            {
                var modelItem = new PersonPackageModel();
                modelItem.Id = item.Id;
                modelItem.PersonId = item.PersonId.ToString();
                modelItem.PersonTitle = item.Person != null ? item.Person.FirstName + " " + item.Person.LastName : "";
                modelItem.PackageTemplateId = item.PackageTemplateId.ToString();
                modelItem.PackageTemplateTitle = item.PackageTemplate != null ? item.PackageTemplate.Title : "";
                modelItem.Price = item.Price;
                modelItem.FactorDate = item.FactorDate != null ? item.FactorDate.ToString() : "";
                modelItem.ExpiredDate = item.ExpiredDate != null ? item.ExpiredDate.ToString() : "";
                modelItem.FactorNumber = item.FactorNumber;
                modelItem.BankDocument = item.BankDocument;
                modelItem.BankId = item.BankId;
                modelItem.BankDocumentDate = item.BankDocumentDate != null ? BaseSettings.Gregorian2HijriSlashedWithNull(item.BankDocumentDate) : "";
                modelItem.PersonPackageDetailList = _personPackageDetailService.GetAllByPersonPackageId(item.Id).Select(ptd => new PersonPackageDetailModel
                {
                    Id = ptd.Id,
                    ServiceTypeId = ptd.ServiceTypeId,
                    ServiceTypeTitle = ptd.ServiceType != null ? ptd.ServiceType.Title : "",
                    Quantity = ptd.Quantity,
                    UsedQuantity = ptd.UsedQuantity
                }).ToList();

                if (item.PersonCarId.HasValue)
                {
                    var personCar = _personCarService.GetById(item.PersonCarId.Value);
                    if (personCar != null)
                    {
                        modelItem.PersonCarId = personCar.Id;
                        modelItem.PersonCarPlaqueNo = personCar.PlaqueNo; 
                    }
                }
                packages.Add(modelItem);
            }
            var retModel = packages;
            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }
        //[HttpPost("PersonPackageDetailList")]
        //public IActionResult PersonPackageDetailList([FromBody] PersonPackageDetailModel model)
        //{
        //    var retModel = new List<PersonPackageDetailModel>();
        //    var personPackageDetailList = new List<PersonPackageDetail>();
        //    if (model.PersonPackageId != 0)
        //    {
        //        personPackageDetailList = _personPackageDetailService.GetAllByPersonPackageId(model.PersonPackageId);
        //        retModel = personPackageDetailList.Select(pt => new PersonPackageDetailModel
        //        {
        //            Id = pt.Id,
        //            PersonPackageId = pt.PersonPackageId,
        //            PersonPackageTitle = pt.PersonPackage != null ? pt.PersonPackage.PackageTemplate.Title : "",
        //            ServiceTypeId = pt.ServiceTypeId,
        //            ServiceTypeTitle = pt.ServiceType != null ? pt.ServiceType.Title : "",
        //            Quantity = pt.Quantity
        //        }).ToList();
        //    }
        //    return Ok(new
        //    {
        //        status = true,
        //        errorId = "",
        //        errorTitle = "",
        //        result = retModel
        //    });
        //}


        //[HttpPost("SavePersonPackage")]
        //public IActionResult SavePersonPackage([FromBody] PersonPackageModel model)
        //{
        //    var token = HttpContext.GetTokenAsync("token");
        //    var objPersonPackage = new PersonPackage();
        //    try
        //    {
        //        if (model.Id != 0)
        //        {
        //            objPersonPackage = _personPackageService.GetById(model.Id);
        //            objPersonPackage.PersonId = System.Xml.XmlConvert.ToGuid(model.PersonId);
        //            objPersonPackage.PackageTemplateId = System.Xml.XmlConvert.ToGuid(model.PackageTemplateId);
        //            objPersonPackage.Price = model.Price;
        //            objPersonPackage.FactorDate = BaseSettings.ParseDate(model.FactorDate).Value;
        //            objPersonPackage.ExpiredDate = BaseSettings.ParseDate(model.ExpiredDate);
        //            objPersonPackage.FactorNumber = model.FactorNumber;
        //            objPersonPackage.BankDocument = model.BankDocument;
        //            objPersonPackage.BankId = model.BankId;
        //            objPersonPackage.BankDocumentDate = BaseSettings.ParseDate(model.BankDocumentDate);
        //            objPersonPackage.CanceledDate = BaseSettings.ParseDate(model.CanceledDate);
        //            objPersonPackage.UpdatedDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            objBrand = new Brand()
        //            {
        //                objPersonPackage = _personPackageService.GetById(model.Id),
        //            objPersonPackage.PersonId = System.Xml.XmlConvert.ToGuid(model.PersonId),
        //            objPersonPackage.PackageTemplateId = System.Xml.XmlConvert.ToGuid(model.PackageTemplateId),
        //            objPersonPackage.Price = model.Price,
        //            objPersonPackage.FactorDate = BaseSettings.ParseDate(model.FactorDate).Value,
        //            objPersonPackage.ExpiredDate = BaseSettings.ParseDate(model.ExpiredDate),
        //            objPersonPackage.FactorNumber = model.FactorNumber,
        //            objPersonPackage.BankDocument = model.BankDocument,
        //            objPersonPackage.BankId = model.BankId,
        //            objPersonPackage.BankDocumentDate = BaseSettings.ParseDate(model.BankDocumentDate),
        //            objPersonPackage.CanceledDate = BaseSettings.ParseDate(model.CanceledDate),
        //            objPersonPackage.CreatedDate = DateTime.Now,
        //            objPersonPackage.CreatedUserId = DateTime.Now,
        //        };
        //        }

        //        if (model.Id != 0)
        //            _brandService.UpdateAsync(objBrand);
        //        else
        //            _brandService.CreateAsync(objBrand);

        //        return Ok(new { errorId = 0, errorTitle = "", result = objBrand.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = 0 });
        //    }

        //}
    }
}
