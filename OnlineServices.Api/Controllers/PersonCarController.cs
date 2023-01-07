using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Api.Models;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class PersonCarController : ControllerBase
    {
        private readonly IPersonCarService _personCarService;
        private readonly IModelService _modelService;
        private readonly IPersonService _personService;
        private readonly IAspNetUserService _aspNetUserService;

        public PersonCarController(IModelService modelService,
            IPersonService personService,
            IPersonCarService personCarService,
            IAspNetUserService aspNetUserService)
        {
            _modelService = modelService;
            _personCarService = personCarService;
            _personService = personService;
            _aspNetUserService = aspNetUserService;
        }

        [HttpPost("PersonCarList")]
        public IActionResult PersonCarList([FromBody] PersonCarModel model)
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


            var retModel = _personCarService.GetAll(
                PersonId: person.Id).Select(c => new PersonCarModel
                {
                    Id = c.Id,
                    PersonId = c.PersonId.ToString(),
                    PersonName = c.Person != null ? c.Person.FirstName + " " + c.Person.LastName : "",
                    ModelId = c.ModelId,
                    ModelTitle = c.Model != null ? c.Model.Title : "",
                    ChassisNo = c.ChassisNo,
                    PlaqueNo = c.PlaqueNo,
                    Description = c.Description,
                    IsActive = c.IsActive
                }).ToList();

            return Ok(new
            {
                status = true,
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

        [HttpPost("SavePersonCar")]
        public IActionResult SavePersonCar([FromBody] PersonCarModel model)
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

            PersonCar objPersonCar;
            try
            {
                if (model.Id != 0)
                {
                    objPersonCar = _personCarService.GetById(model.Id);
                    if (objPersonCar == null)
                        return Ok(new { errorId = 99, errorTitle = "خودرویی با این شناسه یافت نشد", result = (string)null });

                    objPersonCar = new PersonCar
                    {
                        PersonId = person.Id,
                        ModelId = model.ModelId.Value,
                        PlaqueNo = model.PlaqueNo,
                        ChassisNo = model.ChassisNo,
                        Description = model.Description,
                        IsActive = true,
                        UpdatedDate = DateTime.Now,
                        UpdatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id)
                    };
                    _personCarService.UpdateAsync(objPersonCar);
                }
                else
                {
                    objPersonCar = new PersonCar
                    {
                        PersonId = person.Id,
                        ModelId = model.ModelId.Value,
                        PlaqueNo = model.PlaqueNo,
                        ChassisNo = model.ChassisNo,
                        Description = model.Description,
                        IsActive = true,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(currentUser.Id)
                    };
                    _personCarService.CreateAsync(objPersonCar);
                }

                return Ok(new { errorId = 0, errorTitle = "", result = objPersonCar.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("DeletePersonCar")]
        public IActionResult DeletePersonCar([FromBody] PersonCarModel model)
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

            if (model.Id == 0)
                return Ok(new { errorId = 99, errorTitle = "شناسه خودرو نامعتبر است", result = (string)null });

            try
            {
                PersonCar objPersonCar = _personCarService.GetByIdAndPersonId(PersonCarId: model.Id, PersonId: person.Id);
                if (objPersonCar == null)
                    return Ok(new { errorId = 99, errorTitle = "این خودرو متعلق به شما نیست", result = (string)null });

                _personCarService.Delete(objPersonCar);
                return Ok(new { errorId = 0, errorTitle = "", result = (string)null });
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("conflicted"))
                    return BadRequest(new { errorId = 99, errorTitle = "بنا به استفاده از این خودرو در درخواست خدمات اجازه حذف وجود ندارد. ولی میتوانید این پلاک رو غیر فعال کنید", result = (string)null });
                else
                    return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }

        }
    }
}
