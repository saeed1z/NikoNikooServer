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
    public class StateController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IStateService _stateService;

        public StateController(ICityService cityService,
            IStateService stateService)
        {
            _cityService = cityService;
            _stateService = stateService;
        }

        [HttpPost("StateList")]
        public IActionResult StateList()
        {
            var model = _stateService.GetAll().Select(c => new StateModel
            {
                Id = c.Id,
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
