using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderService;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModel;
using Com.Danliris.Service.Internal.Transfer.WebApi.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Com.Danliris.Service.Internal.Transfer.WebApi.Controllers.v1
{
    [Produces("application/json")]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/external-transfer-orders")]
    //[Authorize]
    public class ExternalTransferOrdersController : BasicController<InternalTransferDbContext, ExternalTransferOrderService, ExternalTransferOrderViewModel, ExternalTransferOrder>
    {
        private static readonly string ApiVersion = "1.0";
        public ExternalTransferOrdersController(ExternalTransferOrderService Service) : base(Service, ApiVersion)
        {
        }

        [HttpGet("pdf/{id}")]
        public IActionResult GetPDF([FromRoute]int Id)
        {
            try
            {
                var model = Service.ReadModelById(Id).Result;
                var viewModel = Service.MapToViewModel(model);

                ExternalTransferOrderPDFTemplate PdfTemplate = new ExternalTransferOrderPDFTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"TransferOrderEksternal-{viewModel.ExternalTransferOrderNo}.pdf"
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

        [HttpGet("posted")]
        public IActionResult GetPostedExternalTransferOrderRequest(int Page = 1, int Size = 25, string Order = "{}", [Bind(Prefix = "Select[]")]List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Tuple<List<ExternalTransferOrder>, int, Dictionary<string, string>, List<string>> Data = Service.ReadModel(Page, Size, Order, Select, Keyword, Filter);

                Dictionary<string, object> Result =
                    new ResultFormatter(ApiVersion, General.OK_STATUS_CODE, General.OK_MESSAGE)
                    .Ok(Data.Item1, Service.MapToViewModel, Page, Size, Data.Item2, Data.Item1.Count, Data.Item3, Data.Item4);

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
