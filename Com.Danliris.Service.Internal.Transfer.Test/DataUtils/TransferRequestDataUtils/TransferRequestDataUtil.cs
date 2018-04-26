using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.IntegrationDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using Com.Danliris.Service.Internal.Transfer.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.TransferRequestDataUtils
{
    public class TransferRequestDataUtil : BasicDataUtil<InternalTransferDbContext, TransferRequestService, TransferRequest>, IEmptyData<TransferRequestViewModel>
    {
        private readonly HttpClientService client;
        private readonly TransferRequestDetailDataUtil transferRequestDetailDataUtil;

        public TransferRequestDataUtil(InternalTransferDbContext dbContext, TransferRequestService service, HttpClientService client, TransferRequestDetailDataUtil transferRequestDetailDataUtil) : base(dbContext, service)
        {
            this.client = client;
            this.transferRequestDetailDataUtil = transferRequestDetailDataUtil;
        }

        public TransferRequestViewModel GetEmptyData()
        {
            return new TransferRequestViewModel
            {
                details = new List<TransferRequestDetailViewModel>
                {
                    new TransferRequestDetailViewModel()
                }
            };
        }

        public override TransferRequest GetNewData()
        {
            UnitViewModel unit = UnitDataUtil.GetUnit(client);
            CategoryViewModel category = CategoryDataUtil.GetCategory(client);

            return new TransferRequest
            {
                TRDate = DateTime.Now,
                RequestedArrivalDate = DateTime.Now,
                UnitId = unit._id,
                UnitCode = unit.code,
                UnitName = unit.name,
                DivisionId = unit.division._id,
                DivisionCode = unit.division.code,
                DivisionName = unit.division.name,
                CategoryId = category._id,
                CategoryCode = category.code,
                CategoryName = category.name,
                TransferRequestDetails = new List<TransferRequestDetail> { transferRequestDetailDataUtil.GetNewData() }
            };
        }

        public override async Task<TransferRequest> GetTestData()
        {
            TransferRequest transferRequest = GetNewData();
            this.Service.Token = HttpClientService.Token;
            await this.Service.CreateModel(transferRequest);
            return transferRequest;
        }
    }
}
