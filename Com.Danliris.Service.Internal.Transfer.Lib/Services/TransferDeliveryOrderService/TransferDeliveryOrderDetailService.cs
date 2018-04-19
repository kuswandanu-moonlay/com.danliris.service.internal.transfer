using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService
{
    public class TransferDeliveryOrderDetailService : BasicService<InternalTransferDbContext, TransferDeliveryOrderDetail>
    {
        public TransferDeliveryOrderDetailService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<TransferDeliveryOrderDetail>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public override void OnCreating(TransferDeliveryOrderDetail model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, TransferDeliveryOrderDetail model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(TransferDeliveryOrderDetail model)
        {
            base.OnDeleting(model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }
    }
}
