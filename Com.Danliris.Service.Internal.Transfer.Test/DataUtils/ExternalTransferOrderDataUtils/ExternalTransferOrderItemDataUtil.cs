using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.InternalTransferOrderDataUtils;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.ExternalTransferOrderDataUtils
{
    public class ExternalTransferOrderItemDataUtil
    {
        private readonly InternalTransferOrderDataUtil internalTransferOrderDataUtil;
        private readonly ExternalTransferOrderDetailDataUtil externalTransferOrderDetailDataUtil;

        public ExternalTransferOrderItemDataUtil(InternalTransferOrderDataUtil internalTransferOrderDataUtil, ExternalTransferOrderDetailDataUtil externalTransferOrderDetailDataUtil)
        {
            this.internalTransferOrderDataUtil = internalTransferOrderDataUtil;
            this.externalTransferOrderDetailDataUtil = externalTransferOrderDetailDataUtil;
        }

        public ExternalTransferOrderItem GetNewData()
        {
            Task<InternalTransferOrder> internalTransferOrderTask = Task.Run(() => internalTransferOrderDataUtil.GetTestData());
            internalTransferOrderTask.Wait();

            InternalTransferOrder internalTransferOrder = internalTransferOrderTask.Result;

            return new ExternalTransferOrderItem
            {
                ITOId = internalTransferOrder.Id,
                ITONo = internalTransferOrder.ITONo,
                TRId = internalTransferOrder.Id,
                TRNo = internalTransferOrder.TRNo,
                UnitId = internalTransferOrder.UnitId,
                UnitCode = internalTransferOrder.UnitCode,
                UnitName = internalTransferOrder.UnitName,
                ExternalTransferOrderDetails = externalTransferOrderDetailDataUtil.GetNewData(internalTransferOrder)
            };
        }
    }
}
