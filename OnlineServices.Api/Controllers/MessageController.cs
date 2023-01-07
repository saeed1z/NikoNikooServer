using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using OnlineServices.Core;
using OnlineServices.Entity;
using OnlineServices.Api.Models;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;
using OnlineServices.Api.Helpers;
using System.Globalization;
using Microsoft.AspNetCore.Cors;

namespace OnlineServices.Api.Controllers
{
    [Route("[controller]")]
    [EnableCors]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IMessageService _messageService;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IConfiguration _configuration;
        private readonly IServiceCaptureService _serviceCaptureService;

        public MessageController(IMessageService messageService,
            IPersonService personService,
            IServiceRequestService serviceRequestService,
            IAspNetUserService aspNetUserService,
            IConfiguration configuration,
            IServiceCaptureService serviceCaptureService
            )
        {
            _personService = personService;
            _serviceRequestService = serviceRequestService;
            _messageService = messageService;
            _aspNetUserService = aspNetUserService;
            _configuration = configuration;
            _serviceCaptureService = serviceCaptureService;
        }

        [HttpPost("MessageList")]
        public IActionResult MessageList([FromBody] MessageModel model)
        {
            var token = Request.Headers["Token"];

            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });


            var retModel = _messageService.GetAllForApp(
                ToPersonId: person.Id,
                ServiceRequestId: !string.IsNullOrEmpty(model.ServiceRequestId) ? System.Xml.XmlConvert.ToGuid(model.ServiceRequestId) : (Guid?)null,
                LastDateTime: !string.IsNullOrEmpty(model.LastDateTime) ? Convert.ToDateTime(model.LastDateTime) : (DateTime?)null
                ).Select(c => new
                {
                    Id = c.Id,
                    FromPersonName = c.FromPerson != null ? c.FromPerson.FirstName + " " + c.FromPerson.LastName : "ناشناس",
                    ToPersonName = c.ToPerson != null ? c.ToPerson.FirstName + " " + c.ToPerson.LastName : "ناشناس",
                    Body = c.Body,
                    AllowResponse = c.AllowResponse,
                    HasImage = c.ServiceCaptureId.HasValue,
                    IsSender = c.FromPersonId == person.Id,
                    CreatedDate = c.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss.fff")
                }).ToList();

            return Ok(new
            {
                errorId = 0,
                errorTitle = "",
                result = retModel
            });
        }

        [HttpPost("SendServiceRequestMessage")]
        public IActionResult SendServiceRequestMessage([FromBody] MessageModel model)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            if (string.IsNullOrEmpty(model.ServiceRequestId))
                return Ok(new { errorId = 99, errorTitle = "سرویس ناشناخته", result = (string)null });

            Message objMessage = new Message();
            try
            {
                var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceRequestId));
                if (objServiceRequest == null)
                    return Ok(new { errorId = 99, errorTitle = "سرویس ناشناخته", result = (string)null });

                if (objServiceRequest.ExpertId == null)
                    return Ok(new { errorId = 99, errorTitle = "سرویس دهنده ای مشخص نشده است", result = (string)null });

                if(objServiceRequest.EmployeeId == person.Id)
                {
                    return Ok(new { errorId = 99, errorTitle = "راننده نمی تواند به مشتری پیام ارسال کند", result = (string)null });
                }

                dynamic result = null;
                if (!string.IsNullOrEmpty(model.Image))
                {
                    string apiUrl = $"{_configuration.GetValue<string>("HostAddress:Local")}/Message/SaveMessageImageFromApi";
                    var input = new
                    {
                        UserId = currentUser.Id.ToString(),
                        ServiceRequestId = objServiceRequest.Id.ToString(),
                        Image = model.Image.ToString(),
                        ImageExtension = model.ImageExtension.ToString(),
                    };
                    string inputJson = Newtonsoft.Json.JsonConvert.SerializeObject(input);
                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(apiUrl));
                    httpRequest.ContentType = "application/json";
                    httpRequest.Method = "POST";

                    byte[] bytes = Encoding.UTF8.GetBytes(inputJson);
                    using (Stream stream = httpRequest.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                        stream.Close();
                    }
                    HttpWebResponse httpResponse;
                    try
                    {
                        httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        string message = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
                        httpResponse = (HttpWebResponse)ex.Response;
                    }
                    using (Stream stream = httpResponse.GetResponseStream())
                    {
                        string json = (new StreamReader(stream)).ReadToEnd();
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    }
                }

                if (result != null)
                    if (result.status == false)
                        return BadRequest(new { errorId = 99, errorTitle = result.error, result = (string)null });

                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff",
                                            CultureInfo.InvariantCulture);
                var t = DateTime.Parse(timestamp);

                objMessage = new Message()
                {
                    FromPersonId = person.Id,
                    ServiceRequestId = objServiceRequest.Id,
                    ServiceCaptureId = result != null ? result.status == true ? System.Xml.XmlConvert.ToGuid(result.captureId.ToString()) : (Guid?)null : (Guid?)null,
                    ToPersonId = person.Id == objServiceRequest.PersonId ? objServiceRequest.ExpertId.Value : objServiceRequest.PersonId,
                    Body = model.Body,
                    CreatedDate = DateTime.Now,
                    AllowResponse = true,
                    IsRead = false,
                };
                _messageService.CreateAsync(objMessage);

                if (person.Id == objServiceRequest.PersonId)
                    NotificationHandler.SendToUser("پیام جدید", objServiceRequest.Employee.Notifykey, false, model.Body, new Dictionary<string, string>());
                else
                    NotificationHandler.SendToUser("پیام جدید", objServiceRequest.Person.Notifykey, false, model.Body, new Dictionary<string, string>());


                return Ok(new { errorId = 0, errorTitle = "", result = objMessage.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }


        [HttpGet("GetMessageImage")]
        public IActionResult GetMessageImage(long MessageId)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            try
            {
                var objMessage = _messageService.GetById(MessageId);
                if (objMessage == null)
                    return Ok(new { errorId = 99, errorTitle = "پیام مربوطه پیدا نشد", result = (string)null });

                if (!objMessage.ServiceCaptureId.HasValue)
                    return Ok(new { errorId = 99, errorTitle = "پیام مربوطه فایلی ندارد پیدا نشد", result = (string)null });

                var objCapture = _serviceCaptureService.GetById(objMessage.ServiceCaptureId.Value);

                string PanelUrl = _configuration.GetValue<string>("HostAddress:MotaharUrl");
                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = PanelUrl + "/Uploads/Message/" + objCapture.Id.ToString() + "." + objCapture.Extension
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }


        [HttpGet("GetAllMessageCount")]
        public IActionResult GetAllMessageCount(string ServiceRequestId)
        {
            var token = Request.Headers["Token"];
            if (string.IsNullOrEmpty(token))
                return Ok(new { errorId = 15, errorTitle = "توکن یافت نشد", result = (string)null });

            var currentUser = _aspNetUserService.GetUserByToken(token);
            if (currentUser == null)
                return Ok(new { errorId = 15, errorTitle = "کاربری یافت نشد", result = (string)null });

            var person = _personService.GetByMobileNo(currentUser.UserName);
            if (person == null)
                return Ok(new { errorId = 99, errorTitle = "اطلاعات پروفایل تکمیل نشده است", result = (string)null });

            try
            {
                int allMessageCount = _messageService.GetAllMessageCount(person.Id, !string.IsNullOrEmpty(ServiceRequestId) ? System.Xml.XmlConvert.ToGuid(ServiceRequestId) : (Guid?)null);
                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = allMessageCount
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }
    }
}
