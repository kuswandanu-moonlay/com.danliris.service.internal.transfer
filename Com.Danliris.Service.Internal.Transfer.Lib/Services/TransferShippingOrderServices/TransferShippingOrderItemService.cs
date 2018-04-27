using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel;
using Com.Moonlay.NetCore.Lib;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferShippingOrderServices
{
    public class TransferShippingOrderItemService : BasicService<InternalTransferDbContext, TransferShippingOrderItem>
    {
        public TransferShippingOrderItemService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<TransferShippingOrderItem>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<TransferShippingOrderItem> Query = this.DbContext.TransferShippingOrderItems;

            List<string> SearchAttributes = new List<string>()
                {
                    "SOId", "TRNo", "ITONo", "ETONo"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    "SOId", "TRNo", "ITONo", "ETONo"
                };
            Query = Query
                .Select(result => new TransferShippingOrderItem
                {
                    SOId = result.SOId,
                    TRNo = result.TRNo,
                    ITONo = result.ITONo,
                    ETONo = result.ETONo
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<TransferShippingOrderItem> pageable = new Pageable<TransferShippingOrderItem>(Query, Page - 1, Size);
            List<TransferShippingOrderItem> Data = pageable.Data.ToList<TransferShippingOrderItem>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(TransferShippingOrderItem model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            TransferShippingOrderDetailService shippingOrderDetailService = ServiceProvider.GetService<TransferShippingOrderDetailService>();
            shippingOrderDetailService.Username = this.Username;

            foreach (TransferShippingOrderDetail transferShippingOrderDetail in model.TransferShippingOrderDetails)
            {
                shippingOrderDetailService.OnCreating(transferShippingOrderDetail);
            }
        }

        public override void OnUpdating(int id, TransferShippingOrderItem model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(TransferShippingOrderItem model)
        {
            base.OnDeleting(model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }
    }
}
