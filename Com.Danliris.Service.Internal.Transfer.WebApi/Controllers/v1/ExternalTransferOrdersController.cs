using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.PDFTemplates;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
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
    [Authorize]
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
        public IActionResult TRUnpost([FromRoute] int Id)
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

        [HttpPut("close/{Id}")]
        public IActionResult Close([FromRoute] int Id)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.Close(Id))
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

        [HttpPut("cancel/{Id}")]
        public IActionResult Cancel([FromRoute] int Id)
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;

                if (this.Service.Cancel(Id))
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

        [HttpGet("modelDo")]
        public IActionResult GetModelDoExternalTransferOrder(int Page = 1, int Size = 25, string Order = "{}", [Bind(Prefix = "Select[]")]List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            try
            {
                Service.Username = User.Claims.Single(p => p.Type.Equals("username")).Value;
                Tuple<List<ExternalTransferOrder>, int, Dictionary<string, string>, List<string>> Data = Service.ReadModelDo(Page, Size, Order, Select, Keyword, Filter);

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
