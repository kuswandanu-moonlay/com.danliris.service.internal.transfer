using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [Route("v{version:apiVersion}/internal-transfer-orders")]
    public class InternalTransferOrderController : BasicController<InternalTransferDbContext, InternalTransferOrderService, InternalTransferOrderViewModel, InternalTransferOrder>
    {
        private static readonly string ApiVersion = "1.0";
        // GET: api/InternalTransferOrder
        public InternalTransferOrderController(InternalTransferOrderService Service) : base(Service, ApiVersion)
        {
        }
    }
}
