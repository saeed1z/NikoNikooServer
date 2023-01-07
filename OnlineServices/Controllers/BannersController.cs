using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineServices.Controllers
{
    
    public class BannersController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        private readonly IBannerService _bannerService;

        private readonly IWebHostEnvironment _hostingEnvironment;
        public BannersController(
            SignInManager<IdentityUser> signInManager,
            IBannerService bannerService,
            IWebHostEnvironment hostEnvironment)
        {
            _signInManager = signInManager;
            _bannerService = bannerService;
            _hostingEnvironment = hostEnvironment;
        }
        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Banners/Index" });

            var banner = _bannerService.GetById(id);
            BannerModel bannerModel = new BannerModel()
            {
                BannerFile = banner.BannerFile,
                RowNum = banner.RowNum,
                Title = banner.Title
            };
          
            return View(bannerModel);
        }

        [HttpPost]
        public IActionResult Edit(BannerModel bannerModel)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Banners/Index" });

            if (!ModelState.IsValid)
            {
                return View(bannerModel);
            }

            OnlineServices.Entity.Banner model;

            if (bannerModel.BannerImage != null)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var OldFileAddress = Path.Combine(Path.Combine(webRootPath, "Uploads/Banners/"), bannerModel.BannerFile);
                System.IO.File.Delete(OldFileAddress);
                
                Guid MyFileGuid = Guid.NewGuid();
                string ext = Path.GetExtension(bannerModel.BannerImage.FileName);
                var MyFileName = MyFileGuid.ToString() + ext;
                var NewFileAddress = Path.Combine(Path.Combine(webRootPath, "Uploads/Banners/"), MyFileName);

                using (var fileStream = new FileStream(NewFileAddress, FileMode.Create))
                {
                    bannerModel.BannerImage.CopyTo(fileStream);
                }
                    

                model = new OnlineServices.Entity.Banner()
                {
                    RowNum = bannerModel.RowNum,
                    Title = bannerModel.Title,
                    BannerFile = MyFileName,
                    Id = bannerModel.Id
                };
                _bannerService.Update(model);
            }
            else
            {
                model = new OnlineServices.Entity.Banner()
                {
                    RowNum = bannerModel.RowNum,
                    Title = bannerModel.Title,
                    BannerFile = bannerModel.BannerFile,
                    Id = bannerModel.Id
                };
                _bannerService.Update(model);
            }
            return RedirectToAction("Index", "Banners");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Banners/Index" });

            return View();
        }

        [HttpPost]
        public IActionResult Create(BannerCreateModel bannerCreateModel)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Banners/Index" });

            if (!ModelState.IsValid)
            {
                return View();
            }

            string webRootPath = _hostingEnvironment.WebRootPath;
           
            Guid MyFileGuid = Guid.NewGuid();
            string ext = Path.GetExtension(bannerCreateModel.BannerImage.FileName);
            var MyFileName = MyFileGuid.ToString() + ext;
            var NewFileAddress = Path.Combine(Path.Combine(webRootPath, "Uploads/Banners/"), MyFileName);

            using (var fileStream = new FileStream(NewFileAddress, FileMode.Create))
            {
                bannerCreateModel.BannerImage.CopyTo(fileStream);
            }

            var model = new OnlineServices.Entity.Banner()
            {
                
                RowNum = bannerCreateModel.RowNum,
                Title = bannerCreateModel.Title,
                BannerFile = MyFileName,
                Id = Guid.NewGuid()
            };
            _bannerService.Create(model);

            return RedirectToAction("Index", "Banners");
        }

        public IActionResult Delete(Guid uid)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Banners/Index" });

            string webRootPath = _hostingEnvironment.WebRootPath;
            var banner = _bannerService.GetById(uid);
            if (banner != null) {
                var fileAddress = Path.Combine(Path.Combine(webRootPath, "Uploads/Banners/"), banner.BannerFile);

                System.IO.File.Delete(fileAddress);

                _bannerService.Delete(uid);
            }
            
            return RedirectToAction("Index", "Banners");
        }

        public IActionResult Index()
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Banners/Index" });
            //List<BannerModel> bannerList = new List<BannerModel>
            //{
            //    new BannerModel{
            //        BannerFile = "unnamed.jpg",
            //        RowNum = 2,
            //        Title = "دومین بنر"
            //    },
            //    new BannerModel
            //    {
            //        BannerFile = "carousel2.jpg",
            //        RowNum = 3,
            //        Title = "اولین بنر"
            //    },
            //    new BannerModel
            //    {
            //        BannerFile = "carousel3.jpg",
            //        RowNum = 1,
            //        Title = "سومین بنر"
            //    }
            //};
            return View(_bannerService.GetAll());
        }
    }
}
