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
    public class BrandController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IBrandService _brandService;
        private readonly IAspNetUserService _aspNetUserService;

        public BrandController(IPersonService personService, IBrandService brandService,
            IAspNetUserService aspNetUserService)
        {
            _personService = personService;
            _brandService = brandService;
            _aspNetUserService = aspNetUserService;
        }

        [HttpPost("BrandList")]
        public IActionResult BrandList([FromBody] BrandModel model)
        {
            var retModel = _brandService.GetAll(
                Title: model.Title,
                Description: model.Description,
                IsActive: model.IsActive
                ).Select(c => new BrandModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    EnTitle = c.EnTitle,
                    Description = c.Description,
                    BrandFile = c.BrandFile,
                    IsActive = c.IsActive
                }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

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
