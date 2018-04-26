using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.TransferRequestDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using Com.Danliris.Service.Internal.Transfer.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.InternalTransferOrderDataUtils
{
    public class InternalTransferOrderDataUtil : BasicDataUtil<InternalTransferDbContext, InternalTransferOrderService, InternalTransferOrder>, IEmptyData<InternalTransferOrderViewModel>
    {
        private readonly TransferRequestDataUtil transferRequestDataUtil;
        private readonly InternalTransferOrderDetailDataUtil internalTransferOrderDetailDataUtil;

        public InternalTransferOrderDataUtil(InternalTransferDbContext dbContext, InternalTransferOrderService service, TransferRequestDataUtil transferRequestDataUtil, InternalTransferOrderDetailDataUtil internalTransferOrderDetailDataUtil) : base (dbContext, service)
        {
            this.transferRequestDataUtil = transferRequestDataUtil;
            this.internalTransferOrderDetailDataUtil = internalTransferOrderDetailDataUtil;
        }

        public InternalTransferOrderViewModel GetEmptyData()
        {
            return new InternalTransferOrderViewModel
            {
                InternalTransferOrderDetails = new List<InternalTransferOrderDetailViewModel>
                {
                    new InternalTransferOrderDetailViewModel()
                }
            };
        }

        public override InternalTransferOrder GetNewData()
        {
            Task<TransferRequest> transferRequestTask = Task.Run(() => transferRequestDataUtil.GetTestData());
            transferRequestTask.Wait();

            TransferRequest transferRequest = transferRequestTask.Result;

            return new InternalTransferOrder
            {
                TRId = transferRequest.Id,
                TRNo = transferRequest.TRNo,
                TRDate = transferRequest.TRDate,
                RequestedArrivalDate = transferRequest.RequestedArrivalDate,
                UnitId = transferRequest.UnitId,
                UnitCode = transferRequest.UnitCode,
                UnitName = transferRequest.UnitName,
                DivisionId = transferRequest.DivisionId,
                DivisionCode = transferRequest.DivisionCode,
                DivisionName = transferRequest.DivisionName,
                CategoryId = transferRequest.CategoryId,
                CategoryCode = transferRequest.CategoryCode,
                CategoryName = transferRequest.CategoryName,
                Remarks = transferRequest.Remark,
                InternalTransferOrderDetails = internalTransferOrderDetailDataUtil.GetNewData(transferRequest)
            };
        }

        public override async Task<InternalTransferOrder> GetTestData()
        {
            InternalTransferOrder internalTransferOrder = GetNewData();
            this.Service.Token = HttpClientService.Token;
            await this.Service.CreateModel(internalTransferOrder);
            return internalTransferOrder;
        }
    }
}
