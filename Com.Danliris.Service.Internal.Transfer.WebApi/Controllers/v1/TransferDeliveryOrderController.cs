using Microsoft.AspNetCore.Mvc;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib;
using Microsoft.AspNetCore.Authorization;
using Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates;
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

        [HttpGet("pdf/{id}")]
        public IActionResult GetPDF([FromRoute]int Id)
        {
            try
            {
                var model = Service.ReadModelById(Id).Result;
                var viewModel = Service.MapToViewModel(model);

                TransferDeliveryOrderPDFTemplate PdfTemplate = new TransferDeliveryOrderPDFTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, Service);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"TransferDeliveryOrder-{viewModel.DONo}.pdf"
                };
            }
            catch (Exception e)
            {
                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.INTERNAL_ERROR_STATUS_CODE, e.Message)
                    .Fail();
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE, Result);
            }
        }

        [HttpPut("eto-post")]
        public IActionResult ETOPost([FromBody]List<int> Ids)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.ETOPost(Ids))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
                }
            }
            catch (Exception)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpPut("eto-unpost/{Id}")]
        public IActionResult ETOUnpost([FromRoute] int Id)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.ETOUnpost(Id))
                {
                    return NoContent();
                }
                else
                {
                    return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
                }
            }
            catch (Exception)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpGet("unused")]
        public IActionResult Unused(string Order = "{}", [Bind(Prefix = "Select[]")]List<string> Select = null, string Keyword = null, string Filter = "{}", [Bind(Prefix = "CurrentUsed[]")]List<int> CurrentUsed = null)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                List<TransferDeliveryOrder> Data = Service.ReadModelUnused(Order, Select, Keyword, Filter, CurrentUsed);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(Data, Service.MapToViewModel);

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
