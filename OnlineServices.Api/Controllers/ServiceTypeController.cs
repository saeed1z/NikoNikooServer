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
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceTypeService _serviceTypeService;

        public ServiceTypeController(IServiceTypeService serviceTypeService)
        {
            _serviceTypeService = serviceTypeService;
        }

        [HttpPost("ServiceTypeList")]
        public IActionResult ServiceTypeList()
        {
            var model = _serviceTypeService.GetAll().Select(pt => new PackageTemplateModel
            {
                Id = pt.Id.ToString(),
                Title = pt.Title,
                Description = pt.Description,
                IsActive = pt.IsActive
            }).ToList();

            return Ok(new
            {
                status = true,
                errorId = "",
                errorTitle = "",
                result = model
            });
        }

        [HttpPost("ServiceTypeQuestionsList")]
        public IActionResult ServiceTypeQuestionsList([FromBody] ServiceTypeModel model)
        {
            var ServiceTypeId = model.Id != null ? model.Id : 0;
            var retModel = _serviceTypeService.GetAllServiceTypeQuestion(ServiceTypeId).Where(x=>x.IsActive).Select(pt => new ServiceTypeQuestionsModel
            {
                Id = pt.Id,
                ServiceTypeTitle = pt.ServiceType != null ? pt.ServiceType.Title : "",
                Title = pt.Title,
                IsActive = pt.IsActive,
                Description = pt.Description
            }).ToList();

            return Ok(new
            {
                status = true,
                errorId = "",
                errorTitle = "",
                result = retModel
            });
        }
    }
}
