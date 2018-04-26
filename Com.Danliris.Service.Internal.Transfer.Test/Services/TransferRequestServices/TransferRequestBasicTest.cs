using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.TransferRequestDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Com.Danliris.Service.Internal.Transfer.Test.Services.TransferRequestServices
{
    [Collection("ServiceProviderFixture Collection")]
    public class TransferRequestBasicTest : BasicServiceTest<InternalTransferDbContext, TransferRequestService, TransferRequest, TransferRequestDataUtil>
    {
        private static List<string> keys = new List<string>();
        private IServiceProvider serviceProvider { get; set; }
        public TransferRequestBasicTest(ServiceProviderFixture fixture) : base(fixture, keys)
        {
        }
    }
}
