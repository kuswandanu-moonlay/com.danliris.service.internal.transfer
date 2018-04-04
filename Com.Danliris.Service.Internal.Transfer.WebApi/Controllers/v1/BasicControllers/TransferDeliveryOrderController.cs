using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Services;
using Com.Danliris.Service.Internal.Transfer.Lib.Models;
using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Authorization;


namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1.BasicControllers
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/LotYarn")]
    [Authorize]
    public class TransferDeliveryOrderController : BasicController<InternalTransferDbContext , TransferDeliveryOrderService, TransferDeliveryOrderViewModel, TransferDeliveryOrder>
    {
        private static readonly string ApiVersion = "1.0";
        public TransferDeliveryOrderController(TransferDeliveryOrderService service) : base(service, ApiVersion)
        {
        }

        [HttpGet("{Supplier}")]
        public async Task<IActionResult> GetByQuery([FromRoute] string TransferDeliveryOrder)
        {
            var model = await Service.ReadModelByQuery(TransferDeliveryOrder);

            try
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(model, Service.MapToViewModel);
                return Ok(Result);
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
