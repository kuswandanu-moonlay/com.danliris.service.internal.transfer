using Com.Danliris.Service.Internal.Transfer.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using Com.Danliris.Service.Internal.Transfer.Test.DataUtils.IntegrationDataUtils;
using Com.Danliris.Service.Internal.Transfer.Test.Helpers;
using Com.Danliris.Service.Internal.Transfer.Test.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Com.Danliris.Service.Internal.Transfer.Test.DataUtils.ExternalTransferOrderDataUtils
{
    public class ExternalTransferOrderDataUtil : BasicDataUtil<InternalTransferDbContext, ExternalTransferOrderService, ExternalTransferOrder>, IEmptyData<ExternalTransferOrderViewModel>
    {
        private readonly HttpClientService client;
        private readonly ExternalTransferOrderItemDataUtil externalTransferOrderItemDataUtil;

        public ExternalTransferOrderDataUtil(InternalTransferDbContext dbContext, ExternalTransferOrderService service, HttpClientService client, ExternalTransferOrderItemDataUtil externalTransferOrderItemDataUtil) : base(dbContext, service)
        {
            this.client = client;
            this.externalTransferOrderItemDataUtil = externalTransferOrderItemDataUtil;
        }

        public ExternalTransferOrderViewModel GetEmptyData()
        {
            return new ExternalTransferOrderViewModel
            {
                // empty data ^_^
            };
        }

        public override ExternalTransferOrder GetNewData()
        {
            DivisionViewModel orderDivision = DivisionDataUtil.GetDivision(client);
            DivisionViewModel deliveryDivision = DivisionDataUtil.GetDivision(client);
            CurrencyViewModel currency = CurrencyDataUtil.GetCurrencyIDR(client);

            return new ExternalTransferOrder
            {
                OrderDivisionId = orderDivision._id,
                OrderDivisionCode = orderDivision.code,
                OrderDivisionName = orderDivision.name,
                DeliveryDivisionId = deliveryDivision._id,
                DeliveryDivisionCode = deliveryDivision.code,
                DeliveryDivisionName = deliveryDivision.name,
                OrderDate = DateTime.Now,
                DeliveryDate = DateTime.Now,
                CurrencyId = currency._id,
                CurrencyDescription = currency.description,
                CurrencyCode = currency.code,
                CurrencySymbol = currency.symbol,
                CurrencyRate = currency.rate,
                ExternalTransferOrderItems = new List<ExternalTransferOrderItem> { externalTransferOrderItemDataUtil.GetNewData() }
            };
        }

        public override async Task<ExternalTransferOrder> GetTestData()
        {
            ExternalTransferOrder externalTransferOrder = GetNewData();
            this.Service.Token = HttpClientService.Token;
            await this.Service.CreateModel(externalTransferOrder);
            return externalTransferOrder;
        }
    }
}
