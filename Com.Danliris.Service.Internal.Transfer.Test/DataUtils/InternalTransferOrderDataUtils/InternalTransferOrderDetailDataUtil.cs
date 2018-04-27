using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.InternalTransferOrderDataUtils
{
    public class InternalTransferOrderDetailDataUtil
    {
        private readonly HttpClientService client;

        public InternalTransferOrderDetailDataUtil(HttpClientService client)
        {
            this.client = client;
        }

        public List<InternalTransferOrderDetail> GetNewData(TransferRequest transferRequest)
        {
            List<InternalTransferOrderDetail> list = new List<InternalTransferOrderDetail>();

            foreach (TransferRequestDetail transferRequestDetail in transferRequest.TransferRequestDetails)
            {
                InternalTransferOrderDetail internalTransferOrderDetail = new InternalTransferOrderDetail
                {
                    TRDetailId = transferRequestDetail.Id,
                    ProductId = transferRequestDetail.ProductId,
                    ProductCode = transferRequestDetail.ProductCode,
                    ProductName = transferRequestDetail.ProductName,
                    Quantity = transferRequestDetail.Quantity,
                    UomId = transferRequestDetail.UomId,
                    UomUnit = transferRequestDetail.UomUnit,
                    Grade = transferRequestDetail.Grade,
                    ProductRemark = transferRequestDetail.ProductRemark
                };
                list.Add(internalTransferOrderDetail);
            }

            return list;
        }

    }
}
