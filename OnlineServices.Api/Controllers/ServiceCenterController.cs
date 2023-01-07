using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class ServiceCenterController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IBrandService _brandService;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IBaseService _baseService;
        private readonly IServiceCenterService _serviceCenterService;

        public ServiceCenterController(IPersonService personService, IBrandService brandService,
            IBaseService baseService,
            IServiceCenterService serviceCenterService,
            IAspNetUserService aspNetUserService)
        {
            _personService = personService;
            _brandService = brandService;
            _baseService = baseService;
            _aspNetUserService = aspNetUserService;
            _serviceCenterService = serviceCenterService;
        }

        //[HttpPost("ServiceCenterList")]
        //public IActionResult ServiceCenterList([FromBody] ServiceCenterModel model)
        //{
        //    var retModel = _serviceCenterService.GetAll(
        //        IsCarwash : model.IsCarwash,
        //        IsMechanic : model.IsMechanic,
        //        IsAccessory : model.IsAccessory,
        //        IsService : model.IsService
        //        ).Select(c => new ServiceCenterModel
        //        {
        //            Id = c.Id.ToString(),
        //            Title = c.Title,
        //            FirstName = c.FirstName,
        //            LastName = c.LastName,
        //            StateId = c.StateId,
        //            StateTitle = c.State!=null ? c.State.Title : "",
        //            CityId = c.CityId,
        //            CityTitle = c.City != null ? c.City.Title : "",
        //            Latitude = c.Latitude,
        //            Longitude = c.Longitude,
        //            IsActive = c.IsActive,
        //            ServiceItemsList = _serviceCenterService.GetServiceCenterDetail()
        //        }).ToList();

        //    return Ok(new
        //    {
        //        errorId = 0,
        //        errorTitle = "",
        //        result = retModel
        //    });
        //}

        //[HttpPost("ServiceTypeItemsList")]
        //public IActionResult ServiceTypeItemsList([FromBody] BaseKindModel model)
        //{

        //    if (model.Id == 0)
        //        return BadRequest(new { errorId = 99, errorTitle = "شناسه خدمت معتبر نمی باشد", result = (string)null });

        //    var objServiceTypeItemsList = _baseService.GetAll(model.Id);
        //    var retModel = _baseService.GetAll(model.Id).Select(c => new BaseKindModel
        //    {
        //        Id = c.Id,
        //        Title = c.Title,
        //    }).ToList();

        //    return Ok(new
        //    {
        //        errorId = 0,
        //        errorTitle = "",
        //        result = retModel
        //    });
        //}

        //[HttpPost("SaveBrand")]
        //public IActionResult SaveBrand([FromBody] BrandModel model)
        //{
        //    var token = HttpContext.GetTokenAsync("token");
        //    var objBrand = new Brand();
        //    try
        //    {
        //        if (model.Id != 0)
        //        {
        //            objBrand = _brandService.GetById(model.Id);
        //            objBrand.Title = model.Title;
        //            objBrand.Description = model.Description;
        //            objBrand.IsActive = model.IsActive;
        //            objBrand.UpdatedDate = DateTime.Now;
        //        }
        //        else
        //        {
        //            objBrand = new Brand()
        //            {
        //                Title = model.Title,
        //                Description = model.Description,
        //                IsActive = model.IsActive,
        //            };
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
