using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.InternalTransferOrderDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test.Controllers.InternalTransferOrderControllers
{
    [Collection("TestServerFixture Collection")]
    public class InternalTransferOrderBasicTest : BasicControllerTest<InternalTransferDbContext, InternalTransferOrderService, InternalTransferOrder, InternalTransferOrderViewModel, InternalTransferOrderDataUtil>
    {
        private static string URI = "v1/internal-transfer-orders";
        private static List<string> CreateValidationAttributes = new List<string> { "TRNo" };
        private static List<string> UpdateValidationAttributes = new List<string> { "TRNo" };

        public InternalTransferOrderBasicTest(TestServerFixture fixture) : base(fixture, URI, CreateValidationAttributes, UpdateValidationAttributes)
        {
        }
    }
}
