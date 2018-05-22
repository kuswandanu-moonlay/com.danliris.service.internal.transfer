using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.IntegrationDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.TransferRequestDataUtils
{
    public class TransferRequestDetailDataUtil
    {
        private readonly HttpClientService client;

        public TransferRequestDetailDataUtil(HttpClientService client)
        {
            this.client = client;
        }

        public TransferRequestDetail GetNewData()
        {
            ProductViewModel product = ProductDataUtil.GetProduct(client);

            return new TransferRequestDetail
            {
                ProductId = product._id,
                ProductCode = product.code,
                ProductName = product.name,
                Quantity = 20,
                UomId = product.uom._id,
                UomUnit = product.uom.unit,
                Grade = "A"
            };
        }
    }
}
