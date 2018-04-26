using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.TransferRequestDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test.Controllers.TransferRequestControllers
{
    [Collection("TestServerFixture Collection")]
    public class TransferRequestBasicTest : BasicControllerTest<InternalTransferDbContext, TransferRequestService, TransferRequest, TransferRequestViewModel, TransferRequestDataUtil>
    {
        private static string URI = "v1/transfer-requests";
        private static List<string> CreateValidationAttributes = new List<string> { "trDate", "requestedArrivalDate", "unit", "category", "details" };
        private static List<string> UpdateValidationAttributes = new List<string> { "trDate", "requestedArrivalDate", "unit", "category", "details" };

        public TransferRequestBasicTest(TestServerFixture fixture) : base(fixture, URI, CreateValidationAttributes, UpdateValidationAttributes)
        {
        }
    }
}
