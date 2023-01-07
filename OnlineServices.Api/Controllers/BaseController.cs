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
using Microsoft.AspNetCore.Authorization;
using OnlineServices.Api.Helpers;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IPersonCarService _personCarService;
        private readonly IModelService _modelService;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IServiceRequestAcceptService _serviceRequestAcceptService;
        private readonly IBaseService _baseService;
        private readonly IBaseKindService _baseKindService;

        public BaseController(IModelService modelService,
            IPersonService personService,
            IPersonCarService personCarService,
            IServiceRequestService serviceRequestService,
            IAspNetUserService aspNetUserService,
            IBaseService baseService,
            IBaseKindService baseKindService,
            IServiceRequestAcceptService serviceRequestAcceptService)
        {
            _modelService = modelService;
            _personService = personService;
            _personCarService = personCarService;
            _serviceRequestService = serviceRequestService;
            _aspNetUserService = aspNetUserService;
            _serviceRequestAcceptService = serviceRequestAcceptService;
            _baseService = baseService;
            _baseKindService = baseKindService;
        }

        [HttpPost("BaseKindList")]
        public IActionResult BaseKindList([FromBody] BaseKindModel model)
        {
            var retModel = _baseKindService.GetAll(
                ParentBaseKindId: model.Id != 0 ? model.Id : Convert.ToInt32(BaseKindEnum.BaseInformation)
                ).Select(c => new BaseKindModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    HasChild = _baseKindService.GetAll(c.Id).Count > 0 ? true : false
                }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

        [HttpPost("BaseList")]
        public IActionResult BaseList([FromBody] BaseModel model)
        {

            var retModel = _baseService.GetAll(
               BaseKindId: model.BaseKindId
               ).Select(c => new
               {
                   Id = c.Id,
                   Title = c.Title
               }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });

        }
    }
}
