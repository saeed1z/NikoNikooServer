using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OnlineServices.Core;
using OnlineServices.Entity;
//using OnlineServices.Core.Interfaces;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class BrandController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IBrandService _brandService;
        private readonly IWebHostEnvironment webHostEnvironment;
        #endregion Fields

        #region Ctor
        public BrandController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IBrandService brandService,
            IWebHostEnvironment hostEnvironment)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._brandService = brandService;
            this.webHostEnvironment = hostEnvironment;
        }
        #endregion Ctor

        #region Utilities
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Brand/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new BrandListModel();
            var brandList = new List<BrandModel>();
            IPagedList<Brand> brands = _brandService.GetPagedAll(page, pageSize);

            if (brands.Count > 0)
            {
                brandList = brands.Select((value, index) => new BrandModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    Title = value.Title,
                    EnTitle = value.EnTitle,
                    Description = value.Description,
                    BrandFile = value.BrandFile,
                    IsActive = value.IsActive,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = brands.PageSize,
                TotalRecords = brands.TotalCount,
                PageIndex = brands.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new BrandListModel
            {
                BrandModel = brandList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        public IActionResult Edit(int id = 0)
        {
            var model = new BrandModel();
            Brand brand = null;

            if (id != 0)
            {
                brand = _brandService.GetById(id);
                if (brand == null)
                    return NotFound();

                model = new BrandModel()
                {
                    Id = brand.Id,
                    Title = brand.Title,
                    EnTitle = brand.EnTitle,
                    Description = brand.Description,
                    BrandFile = brand.BrandFile,
                    IsActive = brand.IsActive,
                };
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(BrandModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            bool IsError = false;
            string Message = null;

            string uniqueFileName = null;

            var brand = new Brand();
            try
            {
                if (model.Id != 0)
                {
                    brand = _brandService.GetById(model.Id);

                    if (model.BrandImage != null) { 
                        uniqueFileName = UploadedFile(model);

                        if (brand.BrandFile != null)
                        {
                            var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Brand", brand.BrandFile);
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                    }
                    }

                    brand.Title = model.Title;
                    brand.EnTitle = model.EnTitle;
                    brand.Description = model.Description;
                    brand.IsActive = model.IsActive;
                    brand.IsActive = model.IsActive;
                    brand.BrandFile = model.BrandImage != null ? uniqueFileName : brand.BrandFile;
                    brand.UpdatedDate = DateTime.Now;
                    brand.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
                }
                else
                {
                    if (model.BrandImage != null)
                        uniqueFileName = UploadedFile(model);

                    brand = new Brand()
                    {
                        Title = model.Title,
                        EnTitle = model.EnTitle,
                        Description = model.Description,
                        IsActive = model.IsActive,
                        BrandFile = model.BrandImage != null ? uniqueFileName : null,
                        CreatedDate = DateTime.Now,
                        CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
                    };
                }

                if (model.Id != 0)
                { 
                    _brandService.UpdateAsync(brand);
                    Message = "ویرایش اطلاعات با موفقیت انجام شد";
                }
                else
                { 
                    _brandService.CreateAsync(brand);
                    Message = "ذخیره اطلاعات با موفقیت انجام شد";
                }

                TempData["IsError"] = false;
                TempData["Message"] = Message.ToString();
                return RedirectToAction("Edit", new { id = brand.Id });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.ToString());
                TempData["Message"] = ex.ToString();
                TempData["IsError"] = true;
                return View(model);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _brandService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private string UploadedFile(BrandModel model)
        {
            string uniqueFileName = null;

            if (model.BrandImage != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Brand");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                uniqueFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(model.BrandImage.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BrandImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion Methods
    }
}
