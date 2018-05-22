using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferShippingOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpGet("pdf/{id}")]
        public IActionResult GetPDF([FromRoute]int Id)
        {
            try
            {
                var model = Service.ReadModelById(Id).Result;
                var viewModel = Service.MapToViewModel(model);

                TransferShippingOrderPDFTemplate PdfTemplate = new TransferShippingOrderPDFTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel, Service);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"SuratJalan-{viewModel.SONo}.pdf"
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

        [HttpPut("so-post")]
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
            catch (Exception)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpPut("so-unpost/{Id}")]
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
            catch (Exception)
            {
                return StatusCode(General.INTERNAL_ERROR_STATUS_CODE);
            }
        }

        [HttpGet("isused/{id}")]
        public IActionResult CheckIsUsedByDeliveryOrder([FromRoute]int Id)
        {
            try
            {
                var data = Service.CheckIdIsUsedByUnitReceiptNotes(Id);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok<bool>(data);
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