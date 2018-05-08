using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService
{
    public class TransferDeliveryOrderItemService : BasicService<InternalTransferDbContext, TransferDeliveryOrderItem>
    {
        public TransferDeliveryOrderItemService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<TransferDeliveryOrderItem>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public override void OnCreating(TransferDeliveryOrderItem model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            TransferDeliveryOrderDetailService transferDeliveryOrderDetailService = ServiceProvider.GetService<TransferDeliveryOrderDetailService>();
            transferDeliveryOrderDetailService.Username = this.Username;

            foreach (TransferDeliveryOrderDetail transferDeliveryOrderDetail in model.transferDeliveryOrderDetail)
            {
                transferDeliveryOrderDetailService.OnCreating(transferDeliveryOrderDetail);
            }
        }

        public override void OnUpdating(int id, TransferDeliveryOrderItem model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(TransferDeliveryOrderItem model)
        {
            base.OnDeleting(model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
