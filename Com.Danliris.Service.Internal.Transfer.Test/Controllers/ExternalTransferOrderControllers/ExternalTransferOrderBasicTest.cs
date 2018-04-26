using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.ExternalTransferOrderDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test.Controllers.ExternalTransferOrderControllers
{
    [Collection("TestServerFixture Collection")]
    public class ExternalTransferOrderBasicTest : BasicControllerTest<InternalTransferDbContext, ExternalTransferOrderService, ExternalTransferOrder, ExternalTransferOrderViewModel, ExternalTransferOrderDataUtil>
    {
        private static string URI = "v1/external-transfer-orders";
        private static List<string> CreateValidationAttributes = new List<string> { "DeliveryDivision", "OrderDivision", "OrderDate", "DeliveryDate", "ExternalTransferOrderItemsCount" };
        private static List<string> UpdateValidationAttributes = new List<string> { "DeliveryDivision", "OrderDivision", "OrderDate", "DeliveryDate", "ExternalTransferOrderItemsCount" };

        public ExternalTransferOrderBasicTest(TestServerFixture fixture) : base(fixture, URI, CreateValidationAttributes, UpdateValidationAttributes)
        {
        }
    }
}
