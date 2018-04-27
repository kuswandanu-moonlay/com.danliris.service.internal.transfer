using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib;
using Microsoft.AspNetCore.Authorization;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferDeliveryOrderViewModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using System.Collections.Generic;
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/transfer-delivery-orders")]
    [Authorize]
    public class TransferDeliveryOrderController : BasicController<InternalTransferDbContext , TransferDeliveryOrderService, TransferDeliveryOrderViewModel, TransferDeliveryOrder>
    {
        private static readonly string ApiVersion = "1.0";
        public TransferDeliveryOrderController(TransferDeliveryOrderService service) : base(service, ApiVersion)
        {
        }

    }
}
