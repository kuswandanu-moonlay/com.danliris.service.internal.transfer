using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferShippingOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/shipping-orders")]
    [Authorize]
    public class TransferShippingOrderController : BasicController<InternalTransferDbContext, TransferShippingOrderService, TransferShippingOrderViewModel, TransferShippingOrder>
    {
        private static readonly string ApiVersion = "1.0";
        public TransferShippingOrderController(TransferShippingOrderService Service) : base(Service, ApiVersion)
        {
        }

        [HttpPut("trpost")]
        public IActionResult SOPost([FromBody]List<int> Ids)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.SOPost(Ids))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
                }
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpPut("sounpost/{Id}")]
        public IActionResult SOUnpost([FromRoute] int Id)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.SOUnpost(Id))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
                }
            }
            catch (Exception e)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

    }
}