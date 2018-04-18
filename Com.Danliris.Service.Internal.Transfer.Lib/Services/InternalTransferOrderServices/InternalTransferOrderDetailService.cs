using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Com.Moonlay.NetCore.Lib;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices
{
    public class InternalTransferOrderDetailService : BasicService<InternalTransferDbContext, InternalTransferOrderDetail>
    {
        public InternalTransferOrderDetailService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<InternalTransferOrderDetail>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<InternalTransferOrderDetail> Query = this.DbContext.InternalTransferOrderDetails;

          

            List<string> SelectedFields = new List<string>()
                {
                    "Id", "ITOId"
                };
            Query = Query
                .Select(b => new InternalTransferOrderDetail
                {
                    Id = b.Id,
                    ITOId = b.ITOId
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<InternalTransferOrderDetail> pageable = new Pageable<InternalTransferOrderDetail>(Query, Page - 1, Size);
            List<InternalTransferOrderDetail> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(InternalTransferOrderDetail model)
        {
            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }
        public override void OnUpdating(int id, InternalTransferOrderDetail model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(InternalTransferOrderDetail model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }


    }
}
