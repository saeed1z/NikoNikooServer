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
    public class ModelController : ControllerBase
    {
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;

        public ModelController(IModelService modelService,
            IBrandService brandService)
        {
            _modelService = modelService;
            _brandService = brandService;
        }

        [HttpPost("ModelList")]
        public IActionResult ModelList([FromBody] ModelModel model)
        {
            var retModel = _modelService.GetAll(
                BrandId: model.BrandId,
                FromPrice: model.FromPrice,
                ToPrice: model.ToPrice,
                CarTypeBaseId:model.CarTypeBaseId
                //Title: model.Title,
                //Description: model.Description,
                //Price: model.Price,
                //IsActive: model.IsActive

                ).Select(c => new
                {
                    Id = c.Id,
                    BrandId = c.BrandId,
                    CarTypeBaseId = c.CarTypeBaseId,
                    CarTypeBaseTitle = c.CarTypeBase.Title,
                    BrandTitle = c.Brand.Title,
                    BrandEnTitle = c.Brand.EnTitle,
                    BrandFile = c.Brand != null ? c.Brand.BrandFile != null ? c.Brand.BrandFile : "" : "",
                    Title = c.Title,
                    EnTitle = c.EnTitle,
                    Description = c.Description,
                    ModelFile = c.ModelFile,
                    Price = c.Price,
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
        [HttpPost("ModelGalleryList")]
        public IActionResult ModelGalleryList([FromBody] ModelGalleryModel model)
        {
            var retModel = _modelService.GetPagedAllModelGallery(
                ModelId: model.ModelId).Select(c => new 
                {
                    Id = c.Id.ToString(),
                    //ModelId = c.ModelId,
                    //ModelTitle = c.Model != null ? c.Model.Title : "",
                    //ModelEnTitle = c.Model != null ? c.Model.EnTitle : "",
                    //ModelCarTypeBaseTitle = c.Model != null ? c.Model.CarTypeBase!=null ? c.Model.CarTypeBase.Title : "" : "",
                    //BrandId = c.Model != null ? c.Model.BrandId : 0,
                    //BrandTitle = c.Model != null ? c.Model.Brand != null ? c.Model.Brand.Title : "" : "",
                    //BrandEnTitle = c.Model != null ? c.Model.Brand != null ? c.Model.Brand.EnTitle : "" : "",
                    Title = c.Title,
                    Description = c.Description,
                    ImageName = c.ImageName
                }).ToList();

            return Ok(new
            {
                status = true,
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

        [HttpPost("ModelTechnicalInfoList")]
        public IActionResult ModelTechnicalInfoList([FromBody] ModelModel model)
        {
            if (model.Id == 0)
                return Ok(new { errorId = 99, errorTitle = "شناسه مدل خودرو یافت نشد", result = (string)null });

            var retModel = _modelService.GetAllTechnicalInfo(
                ModelId: model.Id
                ).Select(c => new ModelTechnicalInfoModel
                {
                    Key = c.Base.Title,
                    Value = c.Value
                }).ToList();

            return Ok(new
            {
                status = true,
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }
    }
}
