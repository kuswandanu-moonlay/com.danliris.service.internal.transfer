using Com.Danliris.Service.Internal.Transfer.Lib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System.Linq.Dynamic.Core;
using System.Reflection;
using Com.Moonlay.NetCore.Lib;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services
{
    public class TransferDeliveryOrderService : BasicService<InternalTransferDbContext , TransferDeliveryOrder>, IMap<TransferDeliveryOrder, TransferDeliveryOrderViewModel>
    {
        public TransferDeliveryOrderService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public override Tuple<List<TransferDeliveryOrder>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<TransferDeliveryOrder> Query = this.DbContext.TransferDeliveryOrders;

            List<string> SearchAttributes = new List<string>()
            {
                "Code", "DeliveryOrderDate", "ArrivedDate", "Supplier"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
                "Code", "DeliveryOrderDate", "ArrivedDate", "Supplier", "Remark"
            };

            Query = Query
                .Select(o => new TransferDeliveryOrder
                {
                    Code = o.Code,
                    DeliveryOrderDate = o.DeliveryOrderDate,
                    ArrivedDate = o.ArrivedDate,
                    SupplierId = o.SupplierId,
                    SupplierCode = o.SupplierCode,
                    SupplierName = o.SupplierName
                });

            /* Order */
            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            /* Pagination */
            Pageable<TransferDeliveryOrder> pageable = new Pageable<TransferDeliveryOrder>(Query, Page - 1, Size);
            List<TransferDeliveryOrder> Data = pageable.Data.ToList<TransferDeliveryOrder>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(TransferDeliveryOrder model)
        {
            do
            {
                model.Code = CodeGenerator.GenerateCode();
            }
            while (this.DbSet.Any(d => d.Code.Equals(model.Code)));

            base.OnCreating(model);
        }

        public TransferDeliveryOrderViewModel MapToViewModel(TransferDeliveryOrder model)
        {
            TransferDeliveryOrderViewModel viewModel = new TransferDeliveryOrderViewModel();
            PropertyCopier<TransferDeliveryOrder, TransferDeliveryOrderViewModel>.Copy(model, viewModel);
            viewModel.Supplier = new TransferDeliveryOrderViewModel.SupplierVM();
            viewModel.Supplier._id = model.SupplierId;
            viewModel.Supplier.code = model.SupplierCode;
            viewModel.Supplier.name = model.SupplierName;
            viewModel.DeliveryOrderDate = model.DeliveryOrderDate;
            return viewModel;
        }

        public TransferDeliveryOrder MapToModel(TransferDeliveryOrderViewModel viewModel)
        {
            TransferDeliveryOrder model = new TransferDeliveryOrder();

            PropertyCopier<TransferDeliveryOrderViewModel, TransferDeliveryOrder>.Copy(viewModel, model);

            model.SupplierId = viewModel.Supplier._id != null ? viewModel.Supplier._id : "";
            model.SupplierCode = viewModel.Supplier.code;
            model.SupplierName = viewModel.Supplier.name;
            model.DeliveryOrderDate = (DateTime)viewModel.DeliveryOrderDate;
            return model;
        }

        public class Keys
        {
        }

        public async Task<TransferDeliveryOrder> ReadModelByQuery(string Supplier)
        {
            TransferDeliveryOrder result = new TransferDeliveryOrder();
            result = await this.DbSet.Where(TransferDeliveryOrder => String.Equals(TransferDeliveryOrder.Code, Supplier) && !TransferDeliveryOrder._IsDeleted).OrderByDescending(x => x._LastModifiedUtc).FirstOrDefaultAsync();

            if (result == null)
            {
                result = new TransferDeliveryOrder();
                return result;
            }
            return result;
        }
    }
}
