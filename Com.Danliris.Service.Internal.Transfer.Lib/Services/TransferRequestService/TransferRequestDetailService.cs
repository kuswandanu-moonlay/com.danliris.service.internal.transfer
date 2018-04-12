using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService
{
    public class TransferRequestDetailService : BasicService<InternalTransferDbContext, TransferRequestDetail>
    {
        public TransferRequestDetailService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override Tuple<List<TransferRequestDetail>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<TransferRequestDetail> Query = this.DbContext.TransferRequestDetails;

            List<string> SearchAttributes = new List<string>()
                {
                    "ProductCode"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                     "ProductCode"
                };
            Query = Query
                .Select(b => new TransferRequestDetail
                {
                    Id = b.Id,
                    ProductCode = b.ProductCode
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<TransferRequestDetail> pageable = new Pageable<TransferRequestDetail>(Query, Page - 1, Size);
            List<TransferRequestDetail> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(TransferRequestDetail model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, TransferRequestDetail model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(TransferRequestDetail model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
