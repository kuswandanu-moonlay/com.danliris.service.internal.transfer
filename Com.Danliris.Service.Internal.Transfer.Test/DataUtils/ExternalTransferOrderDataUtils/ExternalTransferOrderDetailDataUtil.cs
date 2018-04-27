using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.ExternalTransferOrderDataUtils
{
    public class ExternalTransferOrderDetailDataUtil
    {
        public ExternalTransferOrderDetailDataUtil()
        {
        }

        public List<ExternalTransferOrderDetail> GetNewData (InternalTransferOrder internalTransferOrder)
        {
            List<ExternalTransferOrderDetail> list = new List<ExternalTransferOrderDetail>();

            foreach (InternalTransferOrderDetail internalTransferOrderDetail in internalTransferOrder.InternalTransferOrderDetails)
            {
                ExternalTransferOrderDetail externalTransferOrderDetail = new ExternalTransferOrderDetail();

                externalTransferOrderDetail.ITODetailId = internalTransferOrderDetail.Id;
                externalTransferOrderDetail.TRDetailId = internalTransferOrderDetail.TRDetailId;
                externalTransferOrderDetail.ProductId = internalTransferOrderDetail.ProductId;
                externalTransferOrderDetail.ProductCode = internalTransferOrderDetail.ProductCode;
                externalTransferOrderDetail.ProductName = internalTransferOrderDetail.ProductName;
                externalTransferOrderDetail.DefaultQuantity = internalTransferOrderDetail.Quantity;
                externalTransferOrderDetail.DefaultUomId = internalTransferOrderDetail.UomId;
                externalTransferOrderDetail.DefaultUomUnit = internalTransferOrderDetail.UomUnit;
                externalTransferOrderDetail.DealQuantity = internalTransferOrderDetail.Quantity;
                externalTransferOrderDetail.DealUomId = internalTransferOrderDetail.UomId;
                externalTransferOrderDetail.DealUomUnit = internalTransferOrderDetail.UomUnit;
                externalTransferOrderDetail.Convertion = 1;
                externalTransferOrderDetail.Price = 2000;
                externalTransferOrderDetail.Grade = internalTransferOrderDetail.Grade;
                externalTransferOrderDetail.ProductRemark = internalTransferOrderDetail.ProductRemark;

                list.Add(externalTransferOrderDetail);
            }

            return list;
        }
    }
}
