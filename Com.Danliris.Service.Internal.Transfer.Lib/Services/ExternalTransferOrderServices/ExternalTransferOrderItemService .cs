using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Newtonsoft.Json;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices
{
    public class ExternalTransferOrderItemService : BasicService<InternalTransferDbContext, ExternalTransferOrderItem>
    {
        public ExternalTransferOrderItemService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<ExternalTransferOrderItem>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<ExternalTransferOrderItem> Query = this.DbContext.ExternalTransferOrderItems;

            List<string> SearchAttributes = new List<string>()
                {
                    "ExternalTransferOrderId", "TransferRequestNo", "InternalTransferOrderNo"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "ExternalTransferOrderId", "TransferRequestNo", "InternalTransferOrderNo"
                };
            Query = Query
                .Select(result => new ExternalTransferOrderItem
                {
                    ETOId = result.ETOId,
                    TRNo = result.TRNo,
                    ITONo = result.ITONo,
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<ExternalTransferOrderItem> pageable = new Pageable<ExternalTransferOrderItem>(Query, Page - 1, Size);
            List<ExternalTransferOrderItem> Data = pageable.Data.ToList<ExternalTransferOrderItem>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(ExternalTransferOrderItem model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            ExternalTransferOrderDetailService externalTransferOrderDetailService = ServiceProvider.GetService<ExternalTransferOrderDetailService>();
            externalTransferOrderDetailService.Username = this.Username;

            foreach (ExternalTransferOrderDetail externalTransferOrderDetail in model.ExternalTransferOrderDetails)
            {
                externalTransferOrderDetailService.OnCreating(externalTransferOrderDetail);
            }
        }

        public override void OnUpdating(int id, ExternalTransferOrderItem model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(ExternalTransferOrderItem model)
        {
            base.OnDeleting(model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }
    }
}
