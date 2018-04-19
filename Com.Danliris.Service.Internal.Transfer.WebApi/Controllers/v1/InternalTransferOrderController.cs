using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/internal-transfer-orders")]
    [Authorize]
    public class InternalTransferOrderController : BasicController<InternalTransferDbContext, InternalTransferOrderService, InternalTransferOrderViewModel, InternalTransferOrder>
    {
        private static readonly string ApiVersion = "1.0";
        private InternalTransferOrderService internalTransferOrderService { get; }
        // GET: api/InternalTransferOrder
        public InternalTransferOrderController(InternalTransferOrderService Service) : base(Service, ApiVersion)
        {
        }

        
        [HttpPost("split")]
        public async Task<IActionResult> Split([FromBody] InternalTransferOrderViewModel data)
        {

            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Service.Token = Request.Headers["Authorization"].First().Replace("Bearer ", "");

                InternalTransferOrder model = Service.MapToModel(data);
                int ID = (from a in data.InternalTransferOrderDetails
                          select a.ITOId).FirstOrDefault();

                await Service.SplitUpdate(ID, data, model);
 
                Dictionary<string, object> Result =
                   new ResultFormatter(ApiVersion, General.CREATED_STATUS_CODE, General.OK_MESSAGE)
                   .Ok();
                return Created(String.Concat(HttpContext.Request.Path, "/", model.Id), Result);
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpGet("report")]
        public IActionResult Get(string TRNo, string unitRequest, string unit, DateTime startDate, DateTime endDate, int page, int size, string Order = "{}")
        {
            int offset = Convert.ToInt32(Request.Headers["x-timezone-offset"]);
            string accept = Request.Headers["Accept"];
            string pdf = "application/pdf";

            try
            {
                 var data = internalTransferOrderService.GetReport(TRNo/*, unitRequest, unit, startDate, endDate*/, page, size, Order, offset);
                { 
                    return Ok(new
                    {
                        apiVersion = ApiVersion,
                        data = data,
                        info = new { total = data, page = page, size = size }
                    });
                }
               
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }
    }
}
