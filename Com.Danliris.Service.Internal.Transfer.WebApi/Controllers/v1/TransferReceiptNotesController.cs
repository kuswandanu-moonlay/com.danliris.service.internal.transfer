using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/transfer-receipt-notes")]
    [Authorize]
    public class TransferReceiptNotesController : BasicController<InternalTransferDbContext, ExternalTransferOrderService, ExternalTransferOrderViewModel, ExternalTransferOrder>
    {
        private static readonly string ApiVersion = "1.0";
        public TransferReceiptNotesController(ExternalTransferOrderService Service) : base(Service, ApiVersion)
        {
        }
    }
}
