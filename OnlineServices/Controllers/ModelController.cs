using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using OnlineServices.Core;
using OnlineServices.Entity;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class ModelController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IModelService _modelService;
        private readonly IBrandService _brandService;
        private readonly IBaseService _baseService;
        private readonly IWebHostEnvironment webHostEnvironment;
        #endregion Fields

        #region Ctor
        public ModelController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IModelService modelService,
            IBaseService baseService,
            IWebHostEnvironment hostEnvironment,
            IBrandService brandService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._modelService = modelService;
            this._brandService = brandService;
            this._baseService = baseService;
            this.webHostEnvironment = hostEnvironment;
        }
        #endregion Ctor

        #region Utilities

        [NonAction]
        protected virtual void PrepareModelModel(ModelModel model)
        {
            //Fill BrandList
            var brandList = _brandService.GetAll();
            if (brandList.Count > 0)
            {
                var firstItem = new SelectListItem()
                {
                    Value = "0",
                    Text = "-- انتخاب کنید --",
                };
                model.BrandList = brandList.Select(
                    b => new SelectListItem
                    {
                        Text = b.Title + (!string.IsNullOrEmpty(b.EnTitle) ? " - " + b.EnTitle : ""),
                        Value = b.Id.ToString(),
                        Selected = b.Id == model.BrandId ? true : false
                    }).ToList();
                model.BrandList.Insert(0, firstItem);
            }

            //Fill BrandList
            var carTypeList = _baseService.GetAll(4);
            if (carTypeList.Count > 0)
            {
                var firstItem = new SelectListItem()
                {
                    Value = "0",
                    Text = "-- انتخاب کنید --",
                };
                model.CarTypeList = carTypeList.Select(
                    b => new SelectListItem
                    {
                        Text = b.Title,
                        Value = b.Id.ToString(),
                        Selected = b.Id == model.CarTypeBaseId ? true : false
                    }).ToList();
                model.CarTypeList.Insert(0, firstItem);
            }
        }
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/PersonType/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new ModelListModel();
            var modelList = new List<ModelModel>();
            IPagedList<Model> models = _modelService.GetPagedAll(page, pageSize);

            if (models.Count > 0)
            {
                modelList = models.Select((value, index) => new ModelModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    BrandId = value.BrandId,
                    BrandTitle = value.Brand != null ? value.Brand.Title : "",
                    BrandEnTitle = value.Brand != null ? value.Brand.EnTitle : "",
                    Title = value.Title,
                    EnTitle = value.EnTitle,
                    CarTypeBaseTitle = value.CarTypeBase != null ? value.CarTypeBase.Title : "",
                    Price = value.Price,
                    Description = value.Description,
                    ModelFile = value.ModelFile,
                    IsActive = value.IsActive,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = models.PageSize,
                TotalRecords = models.TotalCount,
                PageIndex = models.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ModelListModel
            {
                ModelModel = modelList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(int id = 0)
        {
            var model = new ModelModel();
            Model objModel = null;

            if (id != 0)
            {
                objModel = _modelService.GetById(id);
                if (objModel == null)
                    return NotFound();

                model = new ModelModel()
                {
                    Id = objModel.Id,
                    BrandId = objModel.BrandId,
                    Title = objModel.Title,
                    EnTitle = objModel.EnTitle,
                    CarTypeBaseId = objModel.CarTypeBaseId,
                    CarTypeBaseTitle = objModel.CarTypeBase != null ? objModel.CarTypeBase.Title : "",
                    Price = objModel.Price,
                    ModelFile = objModel.ModelFile,
                    Description = objModel.Description,
                    IsActive = objModel.IsActive,
                };
            }
            PrepareModelModel(model);
            return View(model);
        }

        public IActionResult TechnicalInfo(int id = 0)
        {
            var model = new TechnicalInfoModel();
            Model objModel = null;

            if (id != 0)
            {
                objModel = _modelService.GetById(id);
                if (objModel == null)
                    return NotFound();

                var technicalInfoParametersModelList = new List<TechnicalInfoParametersModel>();
                var baseList = _baseService.GetAll(Convert.ToInt32(BaseKindEnum.TechnicalSpecificationItems));
                if (baseList.Count > 0)
                    technicalInfoParametersModelList = baseList.Select((value, index) => new TechnicalInfoParametersModel
                    {
                        BaseId = value.Id,
                        BaseTitle = value.Title,
                        Value = _modelService.GetTechnicalInfoValue(objModel.Id, value.Id)
                    }).ToList();

                model = new TechnicalInfoModel()
                {
                    ModelId = objModel.Id,
                    ModelTitle = objModel.Title,
                    ModelEnTitle = objModel.EnTitle,
                    ModelCarTypeTitle = objModel.CarTypeBase != null ? objModel.CarTypeBase.Title : "",
                    ModelDescription = objModel.Description,
                    ModelIsActive = objModel.IsActive,
                    TechnicalInfoParametersModelList = technicalInfoParametersModelList
                };
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(ModelModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            string uniqueFileName = null;

            var Model = new Model();
            try
            {
                if (model.Id != 0)
                {
                    Model = _modelService.GetById(model.Id);

                    if (model.ModelImage != null)
                    {
                        uniqueFileName = UploadedFile(model.ModelImage);

                        if (Model.ModelFile != null)
                        {
                            var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Model", Model.ModelFile);
                            if (System.IO.File.Exists(path))
                                System.IO.File.Delete(path);
                        }
                    }

                    Model.Title = model.Title;
                    Model.EnTitle = model.EnTitle;
                    Model.BrandId = model.BrandId;
                    Model.CarTypeBaseId = model.CarTypeBaseId;
                    Model.Description = model.Description;
                    Model.Price = model.Price;
                    Model.IsActive = model.IsActive;
                    Model.ModelFile = model.ModelImage != null ? uniqueFileName : Model.ModelFile;
                    Model.UpdatedDate = DateTime.Now;
                    Model.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    if (model.ModelImage != null)
                        uniqueFileName = UploadedFile(model.ModelImage);

                    Model = new Model()
                    {
                        Title = model.Title,
                        EnTitle = model.EnTitle,
                        Description = model.Description,
                        BrandId = model.BrandId,
                        CarTypeBaseId = model.CarTypeBaseId,
                        Price = model.Price,
                        IsActive = model.IsActive,
                        ModelFile = model.ModelImage != null ? uniqueFileName : null,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (model.Id != 0)
                    _modelService.UpdateAsync(Model);
                else
                    _modelService.CreateAsync(Model);

                PrepareModelModel(model);
                return RedirectToAction("Edit", new { id = Model.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveTechnicalInfo(TechnicalInfoModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var Model = new Model();
            try
            {
                foreach (var item in model.TechnicalInfoParametersModelList)
                {
                    var objTechnicalInfo = _modelService.GetTechnicalInfo(model.ModelId, item.BaseId);
                    if (objTechnicalInfo != null)
                    {
                        objTechnicalInfo.Value = item.Value;
                        Model.UpdatedDate = DateTime.Now;
                        Model.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                        _modelService.UpdateTechnicalInfo(objTechnicalInfo);

                    }
                    else
                    {
                        objTechnicalInfo = new ModelTechnicalInfo();
                        objTechnicalInfo.Id = Guid.NewGuid();
                        objTechnicalInfo.BaseId = item.BaseId;
                        objTechnicalInfo.ModelId = model.ModelId;
                        objTechnicalInfo.Value = item.Value;
                        objTechnicalInfo.IsActive = true;
                        objTechnicalInfo.CreatedDate = DateTime.Now;
                        objTechnicalInfo.CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                        _modelService.CreateTechnicalInfo(objTechnicalInfo);
                    }

                }
                return RedirectToAction("TechnicalInfo", new { id = model.ModelId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _modelService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


        private string UploadedFile(IFormFile File)
        {
            string uniqueFileName = null;

            if (File != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Model");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(File.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    File.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }


        public IActionResult LoadModelGalleryList(int ModelId = 0, int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Model/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new ModelGalleryListModel();
            var modelGalleryList = new List<ModelGalleryModel>();
            IPagedList<ModelGallery> modelGalleries = _modelService.GetPagedAllModelGallery(ModelId, page, pageSize);

            if (modelGalleries.Count > 0)
            {
                modelGalleryList = modelGalleries.Select((value, index) => new ModelGalleryModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id.ToString(),
                    ModelId = value.ModelId,
                    ModelTitle = value.Model != null ? value.Model.Title : "",
                    ModelEnTitle = value.Model != null ? value.Model.EnTitle : "",
                    ModelCarTypeBaseTitle = value.Model != null ? value.Model.CarTypeBase != null ? value.Model.CarTypeBase.Title : "" : "",
                    Title = value.Title,
                    Description = value.Description,
                    ImageName = value.ImageName
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = modelGalleries.PageSize,
                TotalRecords = modelGalleries.TotalCount,
                PageIndex = modelGalleries.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new ModelGalleryListModel
            {
                ModelGalleryModel = modelGalleryList,
                PagerModel = pagerModel
            };
            return View("LoadModelGalleryList", model);
        }

        public IActionResult LoadModelGallery(int ModelGalleryId = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Model/Index" });

            var model = new ModelGalleryModel();

            return View("ModelGallery", model);
        }
        [HttpPost]
        public IActionResult SaveModelImage(ModelGalleryModel model, IFormFile file)
        {
            string uniqueFileName = null;

            if (file == null)
                return Json(new { status = false, error = "تصویری انتخاب نشده است" });

            var objModelGallery = new ModelGallery();
            try
            {
                if (!string.IsNullOrEmpty(model.Id))
                {
                    objModelGallery = _modelService.GetModelGalleryById(System.Xml.XmlConvert.ToGuid(model.Id));

                    if (file != null)
                        uniqueFileName = UploadedFile(file);

                    if (objModelGallery.ImageName != null)
                    {
                        var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Model", objModelGallery.ImageName);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }
                    objModelGallery.Title = model.Title;
                    objModelGallery.ModelId = model.ModelId;
                    objModelGallery.Description = model.Description;
                }
                else
                {
                    if (file != null)
                        uniqueFileName = UploadedFile(file);

                    objModelGallery = new ModelGallery()
                    {
                        Id = Guid.NewGuid(),
                        ModelId = model.ModelId,
                        Title = model.Title,
                        ImageName = uniqueFileName,
                        Description = model.Description
                    };
                }

                if (!string.IsNullOrEmpty(model.Id))
                    _modelService.UpdateModelGallery(objModelGallery);
                else
                    _modelService.CreateModelGallery(objModelGallery);

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, error = ex.ToString() });
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteModelImage(string Id)
        {
            if (string.IsNullOrEmpty(Id))
                return Json(new { status = false, error = "شناسه تصویر نامعتبر" });

            try
            {
                var objModelGallery = _modelService.GetModelGalleryById(System.Xml.XmlConvert.ToGuid(Id));
                if (objModelGallery == null)
                    return Json(new { status = false, error = "تصویر مورد نظر پیدا نشد" });

                await _modelService.DeleteModelGallery(objModelGallery);
                var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Model", objModelGallery.ImageName);
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);

                return Json(new { status = true });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, error = ex.ToString() });
            }
        }
        #endregion Methods
    }
}
