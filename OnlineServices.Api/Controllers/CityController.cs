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
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        [HttpPost("CityList")]
        public IActionResult CityList()
        {
            var model = _cityService.GetAll().Select(c => new CityModel
            {
                Id = c.Id,
                StateId = c.StateId,
                Title = c.Title,
                Description = c.Description,
                IsActive = c.IsActive
            }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = model
            });
        }
    }
}
