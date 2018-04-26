using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.ExternalTransferOrderDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test.Services.ExternalTransferOrderServices
{
    [Collection("ServiceProviderFixture Collection")]
    public class ExternalTransferOrderBasicTest : BasicServiceTest<InternalTransferDbContext, ExternalTransferOrderService, ExternalTransferOrder, ExternalTransferOrderDataUtil>
    {
        private static List<string> keys = new List<string>();
        private IServiceProvider serviceProvider { get; set; }
        public ExternalTransferOrderBasicTest(ServiceProviderFixture fixture) : base(fixture, keys)
        {
            serviceProvider = fixture.ServiceProvider;
        }
    }
}
