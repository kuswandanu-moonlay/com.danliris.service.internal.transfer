using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Interfaces;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels;
using Com.Moonlay.NetCore.Lib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Com.Moonlay.NetCore.Lib.Service;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices
{
    public class ExternalTransferOrderService : BasicService<InternalTransferDbContext, ExternalTransferOrder>, IMap<ExternalTransferOrder, ExternalTransferOrderViewModel>
    {
        public ExternalTransferOrderService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public ExternalTransferOrder MapToModel(ExternalTransferOrderViewModel viewModel)
        {
            ExternalTransferOrder model = new ExternalTransferOrder();
            PropertyCopier<ExternalTransferOrderViewModel, ExternalTransferOrder>.Copy(viewModel, model);

            model.SupplierId = viewModel.Supplier._id;
            model.SupplierCode = viewModel.Supplier.code;
            model.SupplierName = viewModel.Supplier.name;
            model.CurrencyId = viewModel.Currency._id;
            model.CurrencyCode = viewModel.Currency.code;
            model.CurrencyRate = viewModel.Currency.rate;
            model.CurrencySymbol = viewModel.Currency.symbol;
            model.CurrencyDescription = viewModel.Currency.description;

            model.ExternalTransferOrderItems = new List<ExternalTransferOrderItem>();
            foreach (ExternalTransferOrderItemViewModel externalTransferOrderItemViewModel in viewModel.ExternalTransferOrderItems)
            {
                ExternalTransferOrderItem externalTransferOrderItem = new ExternalTransferOrderItem();
                PropertyCopier<ExternalTransferOrderItemViewModel, ExternalTransferOrderItem>.Copy(externalTransferOrderItemViewModel, externalTransferOrderItem);

                externalTransferOrderItem.ExternalTransferOrderDetails = new List<ExternalTransferOrderDetail>();
                foreach (ExternalTransferOrderDetailViewModel externalTransferOrderDetailViewModel in externalTransferOrderItemViewModel.ExternalTransferOrderDetails)
                {
                    ExternalTransferOrderDetail externalTransferOrderDetail = new ExternalTransferOrderDetail();
                    PropertyCopier<ExternalTransferOrderDetailViewModel, ExternalTransferOrderDetail>.Copy(externalTransferOrderDetailViewModel, externalTransferOrderDetail);

                    externalTransferOrderDetail.ProductId = externalTransferOrderDetailViewModel.Product._id;
                    externalTransferOrderDetail.ProductCode = externalTransferOrderDetailViewModel.Product.code;
                    externalTransferOrderDetail.ProductName = externalTransferOrderDetailViewModel.Product.name;
                    externalTransferOrderDetail.DefaultUomId = externalTransferOrderDetailViewModel.DefaultUom._id;
                    externalTransferOrderDetail.DefaultUomUnit = externalTransferOrderDetailViewModel.DefaultUom.unit;
                    externalTransferOrderDetail.DealUomId = externalTransferOrderDetailViewModel.DealUom._id;
                    externalTransferOrderDetail.DealUomUnit = externalTransferOrderDetailViewModel.DealUom.unit;

                    externalTransferOrderItem.ExternalTransferOrderDetails.Add(externalTransferOrderDetail);
                }

                model.ExternalTransferOrderItems.Add(externalTransferOrderItem);
            }

            return model;
        }

        public ExternalTransferOrderViewModel MapToViewModel(ExternalTransferOrder model)
        {
            ExternalTransferOrderViewModel viewModel = new ExternalTransferOrderViewModel();
            PropertyCopier<ExternalTransferOrder, ExternalTransferOrderViewModel>.Copy(model, viewModel);

            viewModel.Supplier = new SupplierViewModel()
            {
                _id = model.SupplierId,
                code = model.SupplierCode,
                name = model.SupplierName
            };

            viewModel.Currency = new CurrencyViewModel()
            {
                _id = model.CurrencyId,
                code = model.CurrencyCode,
                rate = model.CurrencyRate,
                symbol = model.CurrencySymbol,
                description = model.CurrencyDescription
            };

            viewModel.ExternalTransferOrderItems = new List<ExternalTransferOrderItemViewModel>();
            if (model.ExternalTransferOrderItems != null)
            {
                foreach (ExternalTransferOrderItem externalTransferOrderItem in model.ExternalTransferOrderItems)
                {
                    ExternalTransferOrderItemViewModel externalTransferOrderItemViewModel = new ExternalTransferOrderItemViewModel();
                    PropertyCopier<ExternalTransferOrderItem, ExternalTransferOrderItemViewModel>.Copy(externalTransferOrderItem, externalTransferOrderItemViewModel);

                    externalTransferOrderItemViewModel.ExternalTransferOrderDetails = new List<ExternalTransferOrderDetailViewModel>();
                    if (externalTransferOrderItem.ExternalTransferOrderDetails != null)
                    {
                        foreach (ExternalTransferOrderDetail externalTransferOrderDetail in externalTransferOrderItem.ExternalTransferOrderDetails)
                        {
                            ExternalTransferOrderDetailViewModel externalTransferOrderDetailViewModel = new ExternalTransferOrderDetailViewModel();
                            PropertyCopier<ExternalTransferOrderDetail, ExternalTransferOrderDetailViewModel>.Copy(externalTransferOrderDetail, externalTransferOrderDetailViewModel);

                            externalTransferOrderDetailViewModel.Product = new ProductViewModel
                            {
                                _id = externalTransferOrderDetail.ProductId,
                                code = externalTransferOrderDetail.ProductCode,
                                name = externalTransferOrderDetail.ProductName
                            };
                            externalTransferOrderDetailViewModel.DefaultUom = new UomViewModel
                            {
                                _id = externalTransferOrderDetail.DefaultUomId,
                                unit = externalTransferOrderDetail.DefaultUomUnit
                            };
                            externalTransferOrderDetailViewModel.DealUom = new UomViewModel
                            {
                                _id = externalTransferOrderDetail.DealUomId,
                                unit = externalTransferOrderDetail.DealUomUnit
                            };

                            externalTransferOrderItemViewModel.ExternalTransferOrderDetails.Add(externalTransferOrderDetailViewModel);
                        }
                    }

                    viewModel.ExternalTransferOrderItems.Add(externalTransferOrderItemViewModel);
                }
            }

            return viewModel;
        }

        public override Tuple<List<ExternalTransferOrder>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<ExternalTransferOrder> Query = this.DbContext.ExternalTransferOrders;

            List<string> SearchAttributes = new List<string>()
                {
                    // Model
                    "ExternalTransferOrderNo", "SupplierName", "ExternalTransferOrderItems.TransferRequestNo", "ExternalTransferOrderItems.InternalTransferOrderNo"
                };
            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
                {
                    // ViewModel
                    "Id", "ExternalTransferOrderNo", "OrderDate", "Supplier", "ExternalTransferOrderItems", "isPosted"
                };
            Query = Query
                .Select(result => new ExternalTransferOrder
                {
                    // Model
                    Id = result.Id,
                    ExternalTransferOrderNo = result.ExternalTransferOrderNo,
                    OrderDate = result.OrderDate,
                    SupplierName = result.SupplierName,
                    IsPosted = result.IsPosted,
                    Remark = result.Remark,
                    _LastModifiedUtc = result._LastModifiedUtc,
                    ExternalTransferOrderItems = result.ExternalTransferOrderItems
                        .Select(
                            p => new ExternalTransferOrderItem
                            {
                                Id = p.Id,
                                ExternalTransferOrderId = p.ExternalTransferOrderId,
                                InternalTransferOrderId = p.InternalTransferOrderId,
                                InternalTransferOrderNo = p.InternalTransferOrderNo,
                                TransferRequestId = p.TransferRequestId,
                                TransferRequestNo = p.TransferRequestNo,
                                ExternalTransferOrderDetails = p.ExternalTransferOrderDetails
                            }
                        )
                        .Where(
                            i => i.ExternalTransferOrderId.Equals(result.Id)
                        )
                        .ToList()

                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<ExternalTransferOrder> pageable = new Pageable<ExternalTransferOrder>(Query, Page - 1, Size);
            List<ExternalTransferOrder> Data = pageable.Data.ToList<ExternalTransferOrder>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override async Task<ExternalTransferOrder> ReadModelById(int Id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(Id) && d._IsDeleted.Equals(false))
                .Include(d => d.ExternalTransferOrderItems)
                    .ThenInclude(d => d.ExternalTransferOrderDetails)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> CreateModel(ExternalTransferOrder Model)
        {
            int Created = 0;
            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Model.ExternalTransferOrderNo = await this.GenerateExternalTransferOrderNo(Model);
                    Created = await this.CreateAsync(Model);

                    foreach (var item in Model.ExternalTransferOrderItems)
                    {
                        foreach (var detail in item.ExternalTransferOrderDetails)
                        {
                            TransferRequestDetail transferRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TransferRequestDetailId);
                            transferRequestDetail.Status = "Sudah diorder ke Supplier";
                            transferRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                            transferRequestDetail._LastModifiedAgent = "Service";
                            transferRequestDetail._LastModifiedBy = this.Username;

                            InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.InternalTransferOrderDetailId);
                            internalTransferOrderDetail.Status = "Sudah dibuat TO Eksternal";
                            internalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                            internalTransferOrderDetail._LastModifiedAgent = "Service";
                            internalTransferOrderDetail._LastModifiedBy = this.Username;
                        }
                    }
                    this.DbContext.SaveChanges();

                    Transaction.Commit();
                }
                catch (ServiceValidationExeption e)
                {
                    throw new ServiceValidationExeption(e.ValidationContext, e.ValidationResults);
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                }
            }

            return Created;
        }

        public override void OnCreating(ExternalTransferOrder model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            ExternalTransferOrderItemService externalTransferOrderItemService = ServiceProvider.GetService<ExternalTransferOrderItemService>();
            externalTransferOrderItemService.Username = this.Username;

            foreach (ExternalTransferOrderItem externalTransferOrderItem in model.ExternalTransferOrderItems)
            {
                externalTransferOrderItemService.OnCreating(externalTransferOrderItem);
            }
        }

        async Task<string> GenerateExternalTransferOrderNo(ExternalTransferOrder model)
        {
            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");

            string externalTransferOrderNo = "ETO" + model.SupplierCode + Year;

            var lastExternalTransferOrderNo = await this.DbSet.Where(w => w.ExternalTransferOrderNo.StartsWith(externalTransferOrderNo)).OrderByDescending(o => o.ExternalTransferOrderNo).FirstOrDefaultAsync();

            if (lastExternalTransferOrderNo == null)
            {
                return externalTransferOrderNo + "00001";
            }
            else
            {
                int lastNo = Int32.Parse(lastExternalTransferOrderNo.ExternalTransferOrderNo.Replace(externalTransferOrderNo, "")) + 1;
                return externalTransferOrderNo + lastNo.ToString().PadLeft(5, '0');
            }
        }

        public override async Task<int> UpdateModel(int Id, ExternalTransferOrder Model)
        {
            int Updated = 0;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    ExternalTransferOrderItemService externalTransferOrderItemService = ServiceProvider.GetService<ExternalTransferOrderItemService>();
                    externalTransferOrderItemService.Username = this.Username;
                    ExternalTransferOrderDetailService externalTransferOrderDetailService = ServiceProvider.GetService<ExternalTransferOrderDetailService>();
                    externalTransferOrderDetailService.Username = this.Username;

                    HashSet<int> ExternalTransferOrderItemIds = new HashSet<int>(
                        this.DbContext.ExternalTransferOrderItems
                            .Where(p => p.ExternalTransferOrderId.Equals(Id))
                            .Select(p => p.Id)
                        );

                    foreach (int itemId in ExternalTransferOrderItemIds)
                    {
                        HashSet<int> ExternalTransferOrderDetailIds = new HashSet<int>(
                            this.DbContext.ExternalTransferOrderDetails
                                .Where(p => p.ExternalTransferOrderItemId.Equals(itemId))
                                .Select(p => p.Id)
                            );

                        ExternalTransferOrderItem externalTransferOrderItem = Model.ExternalTransferOrderItems.FirstOrDefault(p => p.Id.Equals(itemId));

                        if (externalTransferOrderItem == null)
                        {
                            ExternalTransferOrderItem item = this.DbContext.ExternalTransferOrderItems
                                .Include(d => d.ExternalTransferOrderDetails)
                                .FirstOrDefault(p => p.Id.Equals(itemId));

                            if (item != null)
                            {
                                foreach (var detail in item.ExternalTransferOrderDetails)
                                {
                                    TransferRequestDetail transferRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TransferRequestDetailId);
                                    transferRequestDetail.Status = "Sudah diterima Pembelian";

                                    InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.InternalTransferOrderDetailId);
                                    internalTransferOrderDetail.Status = "TO Internal belum diorder";
                                }
                            }

                            foreach (int detailId in ExternalTransferOrderDetailIds)
                            {
                                ExternalTransferOrderDetail externalTransferOrderDetail = this.DbContext.ExternalTransferOrderDetails.FirstOrDefault(p => p.Id.Equals(detailId));

                                await externalTransferOrderDetailService.DeleteModel(detailId);
                            }

                            await externalTransferOrderItemService.DeleteModel(itemId);
                        }
                        else
                        {
                            await externalTransferOrderItemService.UpdateModel(itemId, externalTransferOrderItem);

                            foreach (int detailId in ExternalTransferOrderDetailIds)
                            {
                                ExternalTransferOrderDetail externalTransferOrderDetail = externalTransferOrderItem.ExternalTransferOrderDetails.FirstOrDefault(p => p.Id.Equals(detailId));

                                await externalTransferOrderDetailService.UpdateModel(detailId, externalTransferOrderDetail);
                            }
                        }
                    }

                    Updated = await this.UpdateAsync(Id, Model);

                    foreach (ExternalTransferOrderItem item in Model.ExternalTransferOrderItems)
                    {
                        if (item.Id == 0)
                        {
                            await externalTransferOrderItemService.CreateModel(item);

                            foreach (var detail in item.ExternalTransferOrderDetails)
                            {
                                TransferRequestDetail transferRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TransferRequestDetailId);
                                transferRequestDetail.Status = "Sudah diorder ke Supplier";

                                InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.InternalTransferOrderDetailId);
                                internalTransferOrderDetail.Status = "Sudah dibuat TO Eksternal";
                            }
                        }
                    }

                    this.DbContext.SaveChanges();

                    Transaction.Commit();
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                }
            }

            return Updated;
        }

        public override void OnUpdating(int id, ExternalTransferOrder model)
        {
            base.OnUpdating(id, model);

            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override async Task<int> DeleteModel(int Id)
        {
            int Deleted = 0;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    //ExternalTransferOrder externalTransferOrder = await this.ReadModelById(Id);
                    ExternalTransferOrderItemService externalTransferOrderItemService = ServiceProvider.GetService<ExternalTransferOrderItemService>();
                    externalTransferOrderItemService.Username = this.Username;
                    ExternalTransferOrderDetailService externalTransferOrderDetailService = ServiceProvider.GetService<ExternalTransferOrderDetailService>();
                    externalTransferOrderDetailService.Username = this.Username;

                    HashSet<int> ExternalTransferOrderItemIds = new HashSet<int>(
                        this.DbContext.ExternalTransferOrderItems
                            .Where(p => p.ExternalTransferOrderId.Equals(Id))
                            .Select(p => p.Id)
                        );

                    foreach (int itemId in ExternalTransferOrderItemIds)
                    {
                        HashSet<int> ExternalTransferOrderDetailIds = new HashSet<int>(
                            this.DbContext.ExternalTransferOrderDetails
                                .Where(p => p.ExternalTransferOrderItemId.Equals(itemId))
                                .Select(p => p.Id)
                            );

                        foreach (int detailId in ExternalTransferOrderDetailIds)
                        {
                            await externalTransferOrderDetailService.DeleteModel(detailId);

                            ExternalTransferOrderItem item = this.DbContext.ExternalTransferOrderItems
                                .Include(d => d.ExternalTransferOrderDetails)
                                .FirstOrDefault(p => p.Id.Equals(itemId));

                            if (item != null)
                            {
                                foreach (var detail in item.ExternalTransferOrderDetails)
                                {
                                    TransferRequestDetail transferRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TransferRequestDetailId);
                                    transferRequestDetail.Status = "Sudah diterima Pembelian";

                                    InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.InternalTransferOrderDetailId);
                                    internalTransferOrderDetail.Status = "TO Internal belum diorder";
                                }
                            }
                        }

                        await externalTransferOrderItemService.DeleteModel(itemId);
                    }

                    Deleted = await this.DeleteAsync(Id);

                    this.DbContext.SaveChanges();
                    Transaction.Commit();
                }
                catch (Exception)
                {
                    Transaction.Rollback();
                }
            }

            return Deleted;
        }

        public override void OnDeleting(ExternalTransferOrder model)
        {
            base.OnDeleting(model);

            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public bool ETOPost(List<int> Ids)
        {
            bool IsSuccessful = false;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var listData = this.DbSet
                        .Where(m => Ids.Contains(m.Id))
                        .Include(d => d.ExternalTransferOrderItems)
                            .ThenInclude(d => d.ExternalTransferOrderDetails)
                        .ToList();
                    listData.ForEach(data => {
                        data.IsPosted = true;
                        data._LastModifiedUtc = DateTime.UtcNow;
                        data._LastModifiedAgent = "Service";
                        data._LastModifiedBy = this.Username;

                        foreach (var item in data.ExternalTransferOrderItems)
                        {
                            InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(s => s.Id == item.InternalTransferOrderId);
                            internalTransferOrder.IsPost = true;
                            internalTransferOrder._LastModifiedUtc = DateTime.UtcNow;
                            internalTransferOrder._LastModifiedAgent = "Service";
                            internalTransferOrder._LastModifiedBy = this.Username;

                            foreach (var detail in item.ExternalTransferOrderDetails)
                            {
                                InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.InternalTransferOrderDetailId);
                                internalTransferOrderDetail.Status = "Sudah diorder ke Supplier";
                                internalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                                internalTransferOrderDetail._LastModifiedAgent = "Service";
                                internalTransferOrderDetail._LastModifiedBy = this.Username;
                            }
                        }
                    });

                    this.DbContext.SaveChanges();

                    IsSuccessful = true;
                    Transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Transaction.Rollback();
                    throw;
                }
            }

            return IsSuccessful;
        }

        public bool ETOUnpost(int Id)
        {
            bool IsSuccessful = false;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = this.DbSet
                        .Include(d => d.ExternalTransferOrderItems)
                            .ThenInclude(d => d.ExternalTransferOrderDetails)
                        .FirstOrDefault(tr => tr.Id == Id && tr._IsDeleted == false);
                    data.IsPosted = false;
                    data._LastModifiedUtc = DateTime.UtcNow;
                    data._LastModifiedAgent = "Service";
                    data._LastModifiedBy = this.Username;

                    foreach (var item in data.ExternalTransferOrderItems)
                    {
                        InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(s => s.Id == item.InternalTransferOrderId);
                        internalTransferOrder.IsPost = false;
                        internalTransferOrder._LastModifiedUtc = DateTime.UtcNow;
                        internalTransferOrder._LastModifiedAgent = "Service";
                        internalTransferOrder._LastModifiedBy = this.Username;

                        foreach (var detail in item.ExternalTransferOrderDetails)
                        {
                            InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.InternalTransferOrderDetailId);
                            internalTransferOrderDetail.Status = "Sudah dibuat TO Eksternal";
                            internalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                            internalTransferOrderDetail._LastModifiedAgent = "Service";
                            internalTransferOrderDetail._LastModifiedBy = this.Username;
                        }
                    }

                    this.DbContext.SaveChanges();

                    IsSuccessful = true;
                    Transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Transaction.Rollback();
                    throw;
                }
            }

            return IsSuccessful;
        }

        public bool Close(int Id)
        {
            bool IsSuccessful = false;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = this.DbSet.FirstOrDefault(tr => tr.Id == Id && tr._IsDeleted == false);
                    data.IsClosed = true;
                    data._LastModifiedUtc = DateTime.UtcNow;
                    data._LastModifiedAgent = "Service";
                    data._LastModifiedBy = this.Username;

                    this.DbContext.SaveChanges();

                    IsSuccessful = true;
                    Transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Transaction.Rollback();
                    throw;
                }
            }

            return IsSuccessful;
        }

        public bool Cancel(int Id)
        {
            bool IsSuccessful = false;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    var data = this.DbSet
                        .Include(d => d.ExternalTransferOrderItems)
                            .ThenInclude(d => d.ExternalTransferOrderDetails)
                        .FirstOrDefault(tr => tr.Id == Id && tr._IsDeleted == false);
                    data.IsCanceled = true;
                    data._LastModifiedUtc = DateTime.UtcNow;
                    data._LastModifiedAgent = "Service";
                    data._LastModifiedBy = this.Username;

                    foreach (var item in data.ExternalTransferOrderItems)
                    {
                        InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(s => s.Id == item.InternalTransferOrderId);
                        internalTransferOrder.IsCanceled = true;
                        internalTransferOrder._LastModifiedUtc = DateTime.UtcNow;
                        internalTransferOrder._LastModifiedAgent = "Service";
                        internalTransferOrder._LastModifiedBy = this.Username;

                        TransferRequest transferRequest = this.DbContext.TransferRequests.FirstOrDefault(s => s.Id == item.TransferRequestId);
                        transferRequest.IsCanceled = true;
                        transferRequest._LastModifiedUtc = DateTime.UtcNow;
                        transferRequest._LastModifiedAgent = "Service";
                        transferRequest._LastModifiedBy = this.Username;
                    }

                    this.DbContext.SaveChanges();

                    IsSuccessful = true;
                    Transaction.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    Transaction.Rollback();
                    throw;
                }
            }

            return IsSuccessful;
        }

        public UnitViewModel GetUnitFromInternalTransferOrderByInternalTransferOrderId(int Id)
        {
            InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(p => p.Id.Equals(Id));
            return new UnitViewModel()
            {
                _id = internalTransferOrder.UnitId,
                code = internalTransferOrder.UnitCode,
                name = internalTransferOrder.UnitName,
            };
        }
    }
}

