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
using System.IO;
using System.Drawing;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using RestSharp;
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
    public class ServiceFactorController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly IServiceFactorService _serviceFactorService;
        private readonly IServiceFactorDetailService _serviceFactorDetailService;
        private readonly IServiceRequestService _serviceRequestService;
        private readonly IMessageService _messageService;
        private readonly IAspNetUserService _aspNetUserService;
        private readonly IConfiguration _configuration;
        private readonly IServiceCaptureService _serviceCaptureService;

        public ServiceFactorController(IMessageService messageService,
            IServiceFactorService serviceFactorService,
            IServiceFactorDetailService serviceFactorDetailService,
            IPersonService personService,
            IServiceRequestService serviceRequestService,
            IAspNetUserService aspNetUserService,
            IConfiguration configuration,
            IServiceCaptureService serviceCaptureService
            )
        {
            _serviceFactorService = serviceFactorService;
            _serviceFactorDetailService = serviceFactorDetailService;
            _personService = personService;
            _serviceRequestService = serviceRequestService;
            _messageService = messageService;
            _aspNetUserService = aspNetUserService;
            _configuration = configuration;
            _serviceCaptureService = serviceCaptureService;
        }

        [HttpPost("AddItem")]
        public IActionResult AddItem([FromBody] ItemModel model)
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

            try
            {
                var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceRequestId));
                if (objServiceRequest == null)
                    return Ok(new { errorId = 99, errorTitle = "سرویس ناشناخته", result = (string)null });


                var objServiceFactor = _serviceFactorService.GetByServiceRequestId(objServiceRequest.Id);
                var objServiceFactorDetail = new ServiceFactorDetail();

                if (objServiceFactor != null)
                {
                    objServiceFactorDetail = new ServiceFactorDetail
                    {
                        Id = Guid.NewGuid(),
                        ServiceFactorId = objServiceFactor.Id,
                        ItemTitle = model.ItemTitle,
                        Quantity = model.Quantity,
                        UnitFee = model.UnitFee,
                    };
                    _serviceFactorDetailService.Create(objServiceFactorDetail);

                    objServiceFactor.TotalCost = (objServiceFactor.TotalCost) + (model.Quantity * model.UnitFee);
                    _serviceFactorService.Update(objServiceFactor);
                }
                else
                {
                    objServiceFactor = new ServiceFactor
                    {
                        Id = Guid.NewGuid(),
                        ServiceRequestId = objServiceRequest.Id,
                        IssueDate = DateTime.Now,
                        SalesShop = model.SalesShop,
                        SalesShopAddress = model.SalesShopAddress,
                        SalesShopPhone = model.SalesShopPhone,
                        TotalCost = 0,
                        DiscountFee = 0,
                        FinalFee = 0,
                    };

                    string isCall = _serviceFactorService.Create(objServiceFactor);
                    if (!string.IsNullOrEmpty(isCall))
                    {
                        objServiceFactorDetail = new ServiceFactorDetail
                        {
                            Id = Guid.NewGuid(),
                            ServiceFactorId = System.Xml.XmlConvert.ToGuid(isCall),
                            ItemTitle = model.ItemTitle,
                            Quantity = model.Quantity,
                            UnitFee = model.UnitFee,
                        };
                        _serviceFactorDetailService.Create(objServiceFactorDetail);

                        objServiceFactor.TotalCost = (objServiceFactor.TotalCost) + (model.Quantity * model.UnitFee);
                        _serviceFactorService.Update(objServiceFactor);
                    }
                }
                return Ok(new { errorId = 0, errorTitle = "", result = objServiceFactorDetail.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("AddFactor")]
        public IActionResult AddFactor([FromBody] ItemModel model)
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

            try
            {
                var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceRequestId));
                if (objServiceRequest == null)
                    return Ok(new { errorId = 99, errorTitle = "سرویس ناشناخته", result = (string)null });


                var objServiceFactor = new ServiceFactor
                {
                    Id = Guid.NewGuid(),
                    ServiceRequestId = objServiceRequest.Id,
                    IssueDate = DateTime.Now,
                    SalesShop = model.SalesShop,
                    SalesShopAddress = model.SalesShopAddress,
                    SalesShopPhone = model.SalesShopPhone,
                    TotalCost = 0,
                    DiscountFee = 0,
                    FinalFee = 0,
                    IsPaid=false,
                    IsAccepted=false
                };
                string isCall = _serviceFactorService.Create(objServiceFactor);
                return Ok(new { errorId = 0, errorTitle = "", result = isCall });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("FactorList")]
        public IActionResult FactorList([FromBody] ItemModel model)
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

            try
            {
                var objServiceRequest = _serviceRequestService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceRequestId));
                if (objServiceRequest == null)
                    return Ok(new { errorId = 99, errorTitle = "سرویس ناشناخته", result = (string)null });

                var retModel = _serviceFactorService.LoadData(objServiceRequest.Id,model.IsPaid).Select(c => new
                {
                    Id = c.Id.ToString(),
                    SalesShop = c.SalesShop,
                    SalesShopAddress = c.SalesShopAddress,
                    SalesShopPhone = c.SalesShopPhone,
                    TotalCost = c.TotalCost,
                    DiscountFee = c.DiscountFee,
                    FinalFee = c.FinalFee,
                    isAccepted = c.IsAccepted,
                    isPaid = c.IsPaid
                }).ToList();

                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = retModel
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }



        [HttpPost("FactorItemsList")]
        public IActionResult FactorItemsList([FromBody] ItemModel model)
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

            if (string.IsNullOrEmpty(model.ServiceFactorId))
                return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

            try
            {
                var objServiceFactor = _serviceFactorService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceFactorId));
                if (objServiceFactor == null)
                    return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

                var retModel = _serviceFactorDetailService.LoadData(objServiceFactor.Id).Select(c => new
                {
                    Id = c.Id.ToString(),
                    ItemTitle = c.ItemTitle,
                    Quantity = c.Quantity,
                    Row = c.Row,
                    UnitFee = c.UnitFee
                }).ToList();

                return Ok(new
                {
                    errorId = 0,
                    errorTitle = "",
                    result = retModel
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("AddFactorItem")]
        public IActionResult AddFactorItem([FromBody] ItemModel model)
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

            if (string.IsNullOrEmpty(model.ServiceFactorId))
                return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

            try
            {
                var objServiceFactor = _serviceFactorService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceFactorId));
                if (objServiceFactor == null)
                    return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

                var objServiceFactorDetail = new ServiceFactorDetail
                {
                    Id = Guid.NewGuid(),
                    ServiceFactorId = objServiceFactor.Id,
                    ItemTitle = model.ItemTitle,
                    Quantity = model.Quantity,
                    UnitFee = model.UnitFee,
                };
                string isCall = _serviceFactorDetailService.Create(objServiceFactorDetail);
                if (!string.IsNullOrEmpty(isCall))
                {
                    objServiceFactor.TotalCost = (objServiceFactor.TotalCost) + (objServiceFactorDetail.Quantity * objServiceFactorDetail.UnitFee);
                    objServiceFactor.FinalFee = objServiceFactor.TotalCost;
                    _serviceFactorService.Update(objServiceFactor);
                }

                return Ok(new { errorId = 0, errorTitle = "", result = objServiceFactorDetail.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }

        [HttpPost("DeleteFactorItem")]
        public IActionResult DeleteFactorItem([FromBody] ItemModel model)
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

            if (string.IsNullOrEmpty(model.ServiceFactorDetailId))
                return Ok(new { errorId = 99, errorTitle = "آیتم ناشناخته", result = (string)null });

            try
            {
                var objServiceDetailFactor = _serviceFactorDetailService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceFactorDetailId));
                if (objServiceDetailFactor == null)
                    return Ok(new { errorId = 99, errorTitle = "آیتم ناشناخته", result = (string)null });

                var price = objServiceDetailFactor.Quantity * objServiceDetailFactor.UnitFee;

                var objServiceFactor = _serviceFactorService.GetById(objServiceDetailFactor.ServiceFactorId);
                //if(objServiceFactor.IsAccepted==true)
                //{
                //    return Ok(new { errorId = 99, errorTitle = "فاکتور قبلا تایید شده و امکان حذف آیتم های آن وجود ندارد ", result = (string)null });
                //}

                _serviceFactorDetailService.Delete(objServiceDetailFactor.Id);

                
                objServiceFactor.TotalCost = objServiceFactor.TotalCost - price;
                objServiceFactor.FinalFee = objServiceFactor.TotalCost;
                _serviceFactorService.Update(objServiceFactor);

                return Ok(new { errorId = 0, errorTitle = "", result = (string)null });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }



        [HttpPost("AcceptFactor")]
        public IActionResult AcceptFactor([FromBody] ItemModel model)
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

            if (string.IsNullOrEmpty(model.ServiceFactorId))
                return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

            try
            {
                var objServiceFactor = _serviceFactorService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceFactorId));
                if (objServiceFactor == null)
                    return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

                if (objServiceFactor.IsAccepted==true)
                    return Ok(new { errorId = 99, errorTitle = "این فاکتور قبلا تایید شده است", result = (string)null });

                var objServiceFactorDetail = new ServiceFactorDetail
                {
                    Id = Guid.NewGuid(),
                    ServiceFactorId = objServiceFactor.Id,
                    ItemTitle = "مالیات نه درصد",
                    Quantity = 1,
                    UnitFee = (decimal)0.09 * (objServiceFactor.TotalCost),
                };
                string isCall = _serviceFactorDetailService.Create(objServiceFactorDetail);
                if (!string.IsNullOrEmpty(isCall))
                {
                    objServiceFactor.TotalCost = (decimal)1.09*(objServiceFactor.TotalCost);
                    objServiceFactor.FinalFee = objServiceFactor.TotalCost;
                    objServiceFactor.IsAccepted = true;
                    _serviceFactorService.Update(objServiceFactor);
                }

                return Ok(new { errorId = 0, errorTitle = "", result = objServiceFactorDetail.Id.ToString() });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }


        [HttpPost("PayFactor")]
        public IActionResult PayFactor([FromBody] ItemModel model)
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

            if (string.IsNullOrEmpty(model.ServiceFactorId))
                return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

            try
            {
                var objServiceFactor = _serviceFactorService.GetById(System.Xml.XmlConvert.ToGuid(model.ServiceFactorId));
                if (objServiceFactor == null)
                    return Ok(new { errorId = 99, errorTitle = "فاکتور ناشناخته", result = (string)null });

                if (objServiceFactor.IsPaid == true)
                    return Ok(new { errorId = 99, errorTitle = "این فاکتور قبلا پرداخت شده است", result = (string)null });


                objServiceFactor.IsPaid = true;
                objServiceFactor.IsAccepted = true;
                _serviceFactorService.Update(objServiceFactor);

                return Ok(new { errorId = 0, errorTitle = "", result = "" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorId = 99, errorTitle = ex.Message, result = (string)null });
            }
        }
    }
}
