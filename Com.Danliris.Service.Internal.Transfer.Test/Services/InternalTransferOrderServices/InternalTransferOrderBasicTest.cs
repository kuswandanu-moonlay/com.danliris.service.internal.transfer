using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.InternalTransferOrderDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test.Services.InternalTransferOrderServices
{
    [Collection("ServiceProviderFixture Collection")]
    public class InternalTransferOrderBasicTest : BasicServiceTest<InternalTransferDbContext, InternalTransferOrderService, InternalTransferOrder, InternalTransferOrderDataUtil>
    {
        private static List<string> keys = new List<string>();
        private IServiceProvider serviceProvider { get; set; }
        public InternalTransferOrderBasicTest(ServiceProviderFixture fixture) : base(fixture, keys)
        {
        }
    }
}
