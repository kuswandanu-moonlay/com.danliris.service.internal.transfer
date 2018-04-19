using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using System;
using System.Collections.Generic;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderService
{
    public class ExternalTransferOrderDetailService : BasicService<InternalTransferDbContext, ExternalTransferOrderDetail>
    {
        public ExternalTransferOrderDetailService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<ExternalTransferOrderDetail>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            throw new NotImplementedException();
        }

        public override void OnCreating(ExternalTransferOrderDetail model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, ExternalTransferOrderDetail model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(ExternalTransferOrderDetail model)
        {
            base.OnDeleting(model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }
    }
}
