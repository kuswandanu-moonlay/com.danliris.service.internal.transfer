using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel;
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
    [Route("v{version:apiVersion}/transfer-requests")]
    [Authorize]
    public class TransferRequestController : BasicController<InternalTransferDbContext, TransferRequestService, TransferRequestViewModel, TransferRequest>
    {
        private static readonly string ApiVersion = "1.0";
        public TransferRequestController(TransferRequestService Service) : base(Service, ApiVersion)
        {
        }

        [HttpPut("trpost")]
        public IActionResult Put([FromBody]List<int> Ids)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.TRPost(Ids))
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

        [HttpGet("pdf/{id}")]
        public IActionResult GetPDF([FromRoute]int Id)
        {
            try
            {
                var model = Service.ReadModelById(Id).Result;
                var viewModel = Service.MapToViewModel(model);

                TransferRequestPDFTemplate PdfTemplate = new TransferRequestPDFTemplate();
                MemoryStream stream = PdfTemplate.GeneratePdfTemplate(viewModel);

                return new FileStreamResult(stream, "application/pdf")
                {
                    FileDownloadName = $"Transfer Order {viewModel.trNo}.pdf"
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
    }
}
