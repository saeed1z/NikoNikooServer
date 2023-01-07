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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class PersonTypeController : ControllerBase
    {
        private readonly IPersonTypeService _personTypeService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenManagerService _tokenManagerService;

        public PersonTypeController(IPersonTypeService personTypeService,
            ITokenManagerService tokenManagerService,
            IHttpContextAccessor httpContextAccessor)
        {
            _personTypeService = personTypeService;
            _tokenManagerService = tokenManagerService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost("PersonTypeList")]
        public IActionResult PersonTypeList()
        {
            var model = _personTypeService.GetAll(null, null, true, true).Select(pt => new PersonTypeModel
            {
                Id = pt.Id,
                Title = pt.Title,
                Description = pt.Description
            }).ToList();

            return Ok(new
            {
                status = true,
                errorId = 0,
                errorTitle = "",
                result = model
            });
        }
    }
}
