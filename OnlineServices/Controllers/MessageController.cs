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
using System.Drawing;
using OnlineServices.Models;

namespace OnlineServices.Controllers
{
    public class MessageController : Controller
    {
        #region Fields
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMessageService _messageService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IServiceCaptureService _serviceCaptureService;
        #endregion Fields

        #region Ctor
        public MessageController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IMessageService messageService,
            IWebHostEnvironment hostingEnvironment,
            IServiceCaptureService serviceCaptureService)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._messageService = messageService;
            this._hostingEnvironment = hostingEnvironment;
            this._serviceCaptureService = serviceCaptureService;
        }
        #endregion Ctor

        #region Utilities
        #endregion Utilities

        #region Methods
        public IActionResult Index(int page = 0)
        {
            if (!_signInManager.IsSignedIn(User))
                return RedirectToAction("Login", "Account", new { returnUrl = "/Message/Index" });

            if (page > 0)
                page -= 1;
            var pageSize = 10;
            int rowNum = page * pageSize + 1;

            var model = new MessageListModel();
            var messageList = new List<MessageModel>();
            IPagedList<Message> messages = _messageService.GetPagedAll(page, pageSize);

            if (messages.Count > 0)
            {
                messageList = messages.Select((value, index) => new MessageModel
                {
                    RowNum = rowNum + index,
                    Id = value.Id,
                    ServiceRequestId = value.ServiceRequestId.HasValue ? value.ServiceRequestId.Value.ToString() : "",
                    FromPersonId = value.FromPersonId.ToString(),
                    FromPersonName = value.FromPerson.FirstName,
                    ToPersonId = value.ToPersonId.HasValue ? value.ToPersonId.Value.ToString() : "",
                    ToPersonName = value.ToPersonId.HasValue ? value.ToPerson.FirstName : "",
                    Body = value.Body,
                    CreatedDate = BaseSettings.Gregorian2HijriSlashedWithTime(value.CreatedDate),
                    AllowResponse = value.AllowResponse,
                    IsRead = value.IsRead,
                }).ToList();
            }
            var pagerModel = new PagerModel
            {
                PageSize = messages.PageSize,
                TotalRecords = messages.TotalCount,
                PageIndex = messages.PageIndex,
                ShowTotalSummary = true,
                ShowFirst = true,
                ShowLast = true,
                RouteValues = new RouteValues { page = page }
            };

            model = new MessageListModel
            {
                MessageModel = messageList,
                PagerModel = pagerModel
            };
            return View(nameof(Index), model);
        }

        [HttpPost]
        public JsonResult SaveMessageImageFromApi([FromBody] MessageImageModel model)
        {
            try
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                string ImageFile = null;
                Guid ImageFileGuid = Guid.NewGuid();
                byte[] fileBinary = Convert.FromBase64String(model.Image);

                if (fileBinary != null && fileBinary.Length > 0)
                {
                    ImageFile = ImageFileGuid.ToString() + "." + model.ImageExtension.ToLower();
                    if (!Directory.Exists(Path.Combine(webRootPath, "Uploads/Message/")))
                        Directory.CreateDirectory(Path.Combine(webRootPath, "Uploads/Message/"));
                    var path = Path.Combine(Path.Combine(webRootPath, "Uploads/Message/"), ImageFile);
                    using (MemoryStream ms = new MemoryStream(fileBinary))
                    {
                        System.IO.File.WriteAllBytes(path, fileBinary);
                    }
                }

                var objServiceCapture = new ServiceCapture()
                {
                    Id = ImageFileGuid,
                    ServiceRequestId = System.Xml.XmlConvert.ToGuid(model.ServiceRequestId),
                    FileTypeBaseId = BaseSettings.FileTypeCode(model.ImageExtension.ToLower()),
                    Extension = model.ImageExtension.ToLower(),
                    CreatedUserId = System.Xml.XmlConvert.ToGuid(model.UserId),
                    CreatedDate = DateTime.Now,
                    IsDeletedFromServer = false
                };
                string iscall = _serviceCaptureService.CreateAsync(objServiceCapture);

                return Json(new { status = true, captureId = iscall });
            }
            catch (Exception ex)
            {
                return Json(new { status = false, error = ex.ToString() });
            }
        }

        //public IActionResult Edit(long id = 0)
        //{
        //    var model = new MessageModel();
        //    Message message = null;

        //    if (id != 0)
        //    {
        //        message = _messageService.GetById(id);
        //        if (message == null)
        //            return NotFound();

        //        model = new MessageModel()
        //        {
        //            Id = message.Id,
        //            Title = message.Title,
        //            EnTitle = message.EnTitle,
        //            Description = message.Description,
        //            MessageFile = message.MessageFile,
        //            IsActive = message.IsActive,
        //        };
        //    }
        //    return View(model);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Save(MessageModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return View(model);

        //    bool IsError = false;
        //    string Message = null;

        //    string uniqueFileName = null;

        //    var message = new Message();
        //    try
        //    {
        //        if (model.Id != 0)
        //        {
        //            message = _messageService.GetById(model.Id);

        //            if (model.MessageImage != null) { 
        //                uniqueFileName = UploadedFile(model);

        //                if (message.MessageFile != null)
        //                {
        //                    var path = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Message", Message.MessageFile);
        //                if (System.IO.File.Exists(path))
        //                    System.IO.File.Delete(path);
        //            }
        //            }

        //            message.Title = model.Title;
        //            message.EnTitle = model.EnTitle;
        //            message.Description = model.Description;
        //            message.IsActive = model.IsActive;
        //            message.IsActive = model.IsActive;
        //            message.MessageFile = model.MessageImage != null ? uniqueFileName : Message.MessageFile;
        //            Message.UpdatedDate = DateTime.Now;
        //            Message.UpdatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User));
        //        }
        //        else
        //        {
        //            if (model.MessageImage != null)
        //                uniqueFileName = UploadedFile(model);

        //            Message = new Message()
        //            {
        //                Title = model.Title,
        //                EnTitle = model.EnTitle,
        //                Description = model.Description,
        //                IsActive = model.IsActive,
        //                MessageFile = model.MessageImage != null ? uniqueFileName : null,
        //                CreatedDate = DateTime.Now,
        //                CreatedUserId = System.Xml.XmlConvert.ToGuid(_userManager.GetUserId(User))
        //            };
        //        }

        //        if (model.Id != 0)
        //        { 
        //            _MessageService.UpdateAsync(Message);
        //            Message = "ویرایش اطلاعات با موفقیت انجام شد";
        //        }
        //        else
        //        { 
        //            _MessageService.CreateAsync(Message);
        //            Message = "ذخیره اطلاعات با موفقیت انجام شد";
        //        }

        //        TempData["IsError"] = false;
        //        TempData["Message"] = Message.ToString();
        //        return RedirectToAction("Edit", new { id = Message.Id });
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError(string.Empty, ex.ToString());
        //        TempData["Message"] = ex.ToString();
        //        TempData["IsError"] = true;
        //        return View(model);
        //    }
        //}
        //public async Task<IActionResult> Delete(int id)
        //{
        //    await _MessageService.Delete(id);
        //    return RedirectToAction(nameof(Index));
        //}

        //private string UploadedFile(MessageModel model)
        //{
        //    string uniqueFileName = null;

        //    if (model.MessageImage != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads", "Message");

        //        if (!Directory.Exists(uploadsFolder))
        //            Directory.CreateDirectory(uploadsFolder);

        //        uniqueFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(model.MessageImage.FileName);
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            model.MessageImage.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
        #endregion Methods
    }
}
