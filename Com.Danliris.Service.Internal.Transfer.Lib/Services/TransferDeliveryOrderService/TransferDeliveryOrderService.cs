using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Interfaces;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferDeliveryOrderViewModel;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Moonlay.NetCore.Lib;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferShippingOrderServices;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService
{
    public class TransferDeliveryOrderService : BasicService<InternalTransferDbContext , TransferDeliveryOrder>, IMap<TransferDeliveryOrder, TransferDeliveryOrderViewModel>
    {
        public TransferDeliveryOrderService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public TransferDeliveryOrder MapToModel(TransferDeliveryOrderViewModel viewModel)
        {
            TransferDeliveryOrder model = new TransferDeliveryOrder();

            PropertyCopier<TransferDeliveryOrderViewModel, TransferDeliveryOrder>.Copy(viewModel, model);

            model.DONo = viewModel.DONo;
            model.DOdate = (DateTime)viewModel.DODate;
            model.SupplierId = viewModel.Supplier._id ?? "";
            model.SupplierCode = viewModel.Supplier.code;
            model.SupplierName = viewModel.Supplier.name;
            model.OrderDivisionId = viewModel.Division._id;
            model.OrderDivisionCode = viewModel.Division.code;
            model.OrderDivisionName = viewModel.Division.name;
            model.Remark = viewModel.Remark;
            model.IsPosted = viewModel.IsPosted;

            model.TransferDeliveryOrderItem = new List<TransferDeliveryOrderItem>();

            foreach (TransferDeliveryOrderItemViewModel transferDeliveryOrderItemViewModel in viewModel.items)
            {
                TransferDeliveryOrderItem transferDeliveryOrderItem = new TransferDeliveryOrderItem();

                PropertyCopier<TransferDeliveryOrderItemViewModel, TransferDeliveryOrderItem>.Copy(transferDeliveryOrderItemViewModel, transferDeliveryOrderItem);

                transferDeliveryOrderItem.DOId = transferDeliveryOrderItemViewModel.DOId;
                transferDeliveryOrderItem.ETOId = transferDeliveryOrderItemViewModel.ETOId;
                transferDeliveryOrderItem.ETONo = transferDeliveryOrderItemViewModel.ETONo;
                transferDeliveryOrderItem.TRId = transferDeliveryOrderItemViewModel.TRId;
                transferDeliveryOrderItem.TRNo = transferDeliveryOrderItemViewModel.TRNo;
                transferDeliveryOrderItem.ITOId = transferDeliveryOrderItemViewModel.ITOId;
                transferDeliveryOrderItem.ITONo = transferDeliveryOrderItemViewModel.ITONo;
                transferDeliveryOrderItem.UnitId = transferDeliveryOrderItemViewModel.UnitId;
                transferDeliveryOrderItem.UnitCode = transferDeliveryOrderItemViewModel.UnitCode;
                transferDeliveryOrderItem.UnitName = transferDeliveryOrderItemViewModel.UnitName;

                transferDeliveryOrderItem.transferDeliveryOrderDetail = new List<TransferDeliveryOrderDetail>();
                foreach (TransferDeliveryOrderDetailViewModel transferDeliveryOrderDetailViewModel in transferDeliveryOrderItemViewModel.details)
                {
                    TransferDeliveryOrderDetail transferDeliveryOrderDetail = new TransferDeliveryOrderDetail();
                    PropertyCopier<TransferDeliveryOrderDetailViewModel, TransferDeliveryOrderDetail>.Copy(transferDeliveryOrderDetailViewModel, transferDeliveryOrderDetail);

                    transferDeliveryOrderDetail.DOItemId = transferDeliveryOrderDetailViewModel.DOItemId;
                    transferDeliveryOrderDetail.ETODetailId = transferDeliveryOrderDetailViewModel.ETODetailId;
                    transferDeliveryOrderDetail.ITODetailId = transferDeliveryOrderDetailViewModel.ITODetailId;
                    transferDeliveryOrderDetail.TRDetailId = transferDeliveryOrderDetailViewModel.TRDetailId;
                    transferDeliveryOrderDetail.ProductId = transferDeliveryOrderDetailViewModel.ProductId;
                    transferDeliveryOrderDetail.ProductCode = transferDeliveryOrderDetailViewModel.ProductCode;
                    transferDeliveryOrderDetail.ProductName = transferDeliveryOrderDetailViewModel.ProductName;
                    transferDeliveryOrderDetail.Grade = transferDeliveryOrderDetailViewModel.Grade;
                    transferDeliveryOrderDetail.ProductRemark = transferDeliveryOrderDetailViewModel.ProductRemark;
                    transferDeliveryOrderDetail.RequestedQuantity = transferDeliveryOrderDetailViewModel.RequestedQuantity;
                    transferDeliveryOrderDetail.UomId = transferDeliveryOrderDetailViewModel.UomId;
                    transferDeliveryOrderDetail.UomUnit = transferDeliveryOrderDetailViewModel.UomUnit;
                    transferDeliveryOrderDetail.DOQuantity = transferDeliveryOrderDetailViewModel.DOQuantity;
                    transferDeliveryOrderDetail.ShippingOrderQuantity = transferDeliveryOrderDetailViewModel.ShippingOrderQuantity;
                    transferDeliveryOrderDetail.RemainingQuantity = transferDeliveryOrderDetailViewModel.RemainingQuantity;

                    transferDeliveryOrderItem.transferDeliveryOrderDetail.Add(transferDeliveryOrderDetail);
                }
                model.TransferDeliveryOrderItem.Add(transferDeliveryOrderItem);
            }

            return model;
        }

        public TransferDeliveryOrderViewModel MapToViewModel(TransferDeliveryOrder model)
        {
            TransferDeliveryOrderViewModel viewModel = new TransferDeliveryOrderViewModel();
            PropertyCopier<TransferDeliveryOrder, TransferDeliveryOrderViewModel>.Copy(model, viewModel);
            viewModel.Supplier = new SupplierViewModel()
            {
                _id = model.SupplierId,
                code = model.SupplierCode,
                name = model.SupplierName
            };
            viewModel.Division = new DivisionViewModel()
            {
                _id = model.OrderDivisionId,
                code = model.OrderDivisionCode,
                name = model.OrderDivisionName
            };
            
            viewModel.DODate = model.DOdate;
            viewModel.DONo = model.DONo;
            viewModel.Remark = model.Remark;
            viewModel.IsPosted = model.IsPosted;

            viewModel.items = new List<TransferDeliveryOrderItemViewModel>();
            if (model.TransferDeliveryOrderItem != null)
            {
                foreach (TransferDeliveryOrderItem transferDeliveryOrderItem in model.TransferDeliveryOrderItem)
                {
                    TransferDeliveryOrderItemViewModel transferDeliveryOrderItemViewModel = new TransferDeliveryOrderItemViewModel();
                    PropertyCopier<TransferDeliveryOrderItem, TransferDeliveryOrderItemViewModel>.Copy(transferDeliveryOrderItem, transferDeliveryOrderItemViewModel);

                    transferDeliveryOrderItemViewModel.DOId = transferDeliveryOrderItem.DOId;
                    transferDeliveryOrderItemViewModel.ETOId = transferDeliveryOrderItem.ETOId;
                    transferDeliveryOrderItemViewModel.ETONo = transferDeliveryOrderItem.ETONo;
                    transferDeliveryOrderItemViewModel.TRId = transferDeliveryOrderItem.TRId;
                    transferDeliveryOrderItemViewModel.TRNo = transferDeliveryOrderItem.TRNo;
                    transferDeliveryOrderItemViewModel.ITOId = transferDeliveryOrderItem.ITOId;
                    transferDeliveryOrderItemViewModel.ITONo = transferDeliveryOrderItem.ITONo;
                    transferDeliveryOrderItemViewModel.UnitId = transferDeliveryOrderItem.UnitId;
                    transferDeliveryOrderItemViewModel.UnitCode = transferDeliveryOrderItem.UnitCode;
                    transferDeliveryOrderItemViewModel.UnitName = transferDeliveryOrderItem.UnitName;

                    transferDeliveryOrderItemViewModel.details = new List<TransferDeliveryOrderDetailViewModel>();
                    if (transferDeliveryOrderItem.transferDeliveryOrderDetail != null)
                    {
                        foreach (TransferDeliveryOrderDetail transferDeliveryOrderDetail in transferDeliveryOrderItem.transferDeliveryOrderDetail)
                        {
                            TransferDeliveryOrderDetailViewModel transferDeliveryOrderDetailViewModel = new TransferDeliveryOrderDetailViewModel();
                            PropertyCopier<TransferDeliveryOrderDetail, TransferDeliveryOrderDetailViewModel>.Copy(transferDeliveryOrderDetail, transferDeliveryOrderDetailViewModel);

                            transferDeliveryOrderDetailViewModel.DOItemId = transferDeliveryOrderDetail.DOItemId;
                            transferDeliveryOrderDetailViewModel.ETODetailId = transferDeliveryOrderDetail.ETODetailId;
                            transferDeliveryOrderDetailViewModel.ITODetailId = transferDeliveryOrderDetail.ITODetailId;
                            transferDeliveryOrderDetailViewModel.TRDetailId = transferDeliveryOrderDetail.TRDetailId;
                            transferDeliveryOrderDetailViewModel.ProductId = transferDeliveryOrderDetail.ProductId;
                            transferDeliveryOrderDetailViewModel.ProductCode = transferDeliveryOrderDetail.ProductCode;
                            transferDeliveryOrderDetailViewModel.ProductName = transferDeliveryOrderDetail.ProductName;
                            transferDeliveryOrderDetailViewModel.Grade = transferDeliveryOrderDetail.Grade;
                            transferDeliveryOrderDetailViewModel.ProductRemark = transferDeliveryOrderDetail.ProductRemark;
                            transferDeliveryOrderDetailViewModel.RequestedQuantity = transferDeliveryOrderDetail.RequestedQuantity;
                            transferDeliveryOrderDetailViewModel.UomId = transferDeliveryOrderDetail.UomId;
                            transferDeliveryOrderDetailViewModel.UomUnit = transferDeliveryOrderDetail.UomUnit;
                            transferDeliveryOrderDetailViewModel.DOQuantity = transferDeliveryOrderDetail.DOQuantity;
                            transferDeliveryOrderDetailViewModel.ShippingOrderQuantity = transferDeliveryOrderDetail.ShippingOrderQuantity;
                            transferDeliveryOrderDetailViewModel.RemainingQuantity = transferDeliveryOrderDetail.RemainingQuantity;

                            transferDeliveryOrderItemViewModel.details.Add(transferDeliveryOrderDetailViewModel);
                        }
                    }
                    viewModel.items.Add(transferDeliveryOrderItemViewModel);
                }
            }

            return viewModel;
        }

        public override Tuple<List<TransferDeliveryOrder>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<TransferDeliveryOrder> Query = this.DbContext.TransferDeliveryOrders;

            List<string> SearchAttributes = new List<string>()
            {
                "DONo"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            /* Const Select */
            List<string> SelectedFields = new List<string>()
            {
                "Id", "DONo", "DOdate", "Supplier", "items", "IsPosted"
            };

            Query = Query
                .Select(o => new TransferDeliveryOrder
                {
                    Id = o.Id,
                    DONo = o.DONo,
                    DOdate = o.DOdate,
                    SupplierId = o.SupplierId,
                    SupplierCode = o.SupplierCode,
                    SupplierName = o.SupplierName,
                    IsPosted = o.IsPosted,
                    TransferDeliveryOrderItem = o.TransferDeliveryOrderItem
                        .Select(
                            p => new TransferDeliveryOrderItem
                            {
                                Id = p.Id,
                                DOId = p.DOId,
                                ETONo = p.ETONo
                            }
                        )
                        .Where(
                            i => i.DOId.Equals(o.Id)
                        )
                        .ToList()
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

        public override async Task<TransferDeliveryOrder> ReadModelById(int Id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(Id) && d._IsDeleted.Equals(false))
                .Include(d => d.TransferDeliveryOrderItem)
                    .ThenInclude(d => d.transferDeliveryOrderDetail)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> CreateModel(TransferDeliveryOrder Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    Model.DONo = await this.GenerateTransferDeliveryOrderNo(Model);
                    Created = await this.CreateAsync(Model);
                    foreach (var item in Model.TransferDeliveryOrderItem)
                    {
                        foreach (var detail in item.transferDeliveryOrderDetail)
                        {
                            ExternalTransferOrderDetail externalTransferOrderDetail = this.DbContext.ExternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ETODetailId);
                            externalTransferOrderDetail.DOQuantity = (externalTransferOrderDetail.DOQuantity) + (detail.DOQuantity);
                            externalTransferOrderDetail.RemainingQuantity = (externalTransferOrderDetail.RemainingQuantity) - (detail.DOQuantity);
                            externalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                            externalTransferOrderDetail._LastModifiedAgent = "Service";
                            externalTransferOrderDetail._LastModifiedBy = this.Username;

                            if (externalTransferOrderDetail.RemainingQuantity > 0)
                            {
                                TransferRequestDetail transferRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                transferRequestDetail.Status = "Sudah diorder sebagian ke Unit Pengirim";
                                transferRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                transferRequestDetail._LastModifiedAgent = "Service";
                                transferRequestDetail._LastModifiedBy = this.Username;

                                InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                internalTransferOrderDetail.Status = "Sudah diorder sebagian ke Unit Pengirim";
                                internalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                                internalTransferOrderDetail._LastModifiedAgent = "Service";
                                internalTransferOrderDetail._LastModifiedBy = this.Username;
                            }
                            else if(externalTransferOrderDetail.RemainingQuantity <= 0)
                            {
                                TransferRequestDetail transferRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                transferRequestDetail.Status = "Sudah diorder semua ke Unit Pengirim";
                                transferRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                transferRequestDetail._LastModifiedAgent = "Service";
                                transferRequestDetail._LastModifiedBy = this.Username;

                                InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                internalTransferOrderDetail.Status = "Sudah diorder semua ke Unit Pengirim";
                                internalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                                internalTransferOrderDetail._LastModifiedAgent = "Service";
                                internalTransferOrderDetail._LastModifiedBy = this.Username;
                            }
                        }
                    }
                    this.DbContext.SaveChanges();

                    transaction.Commit();
                }
                catch (ServiceValidationExeption e)
                {
                    throw new ServiceValidationExeption(e.ValidationContext, e.ValidationResults);
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }
            return Created;
        }

        public override void OnCreating(TransferDeliveryOrder model)
        {
            base.OnCreating(model);

            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;

            TransferDeliveryOrderItemService transferDeliveryOrderItemService = ServiceProvider.GetService<TransferDeliveryOrderItemService>();
            transferDeliveryOrderItemService.Username = this.Username;

            foreach (TransferDeliveryOrderItem transferDeliveryOrderItem in model.TransferDeliveryOrderItem)
            {
                transferDeliveryOrderItemService.OnCreating(transferDeliveryOrderItem);
            }
        }

        async Task<string> GenerateTransferDeliveryOrderNo(TransferDeliveryOrder model)
        {
            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");
            string Month = Now.ToString("mm");

            string transferDeliveryOrderNo = "DO" + model.SupplierCode + Year + Month;

            var lastTransferDeliveryOrderNo = await this.DbSet.Where(w => w.DONo.StartsWith(transferDeliveryOrderNo)).OrderByDescending(o => o.DONo).FirstOrDefaultAsync();

            if (lastTransferDeliveryOrderNo == null)
            {
                return transferDeliveryOrderNo + "001";
            }
            else
            {
                int lastNo = Int32.Parse(lastTransferDeliveryOrderNo.DONo.Replace(transferDeliveryOrderNo, "")) + 1;
                return transferDeliveryOrderNo + lastNo.ToString().PadLeft(5, '0');
            }
        }

        public override async Task<int> UpdateModel(int Id, TransferDeliveryOrder Model)
        {
            int Updated = 0;

            using (var Transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    TransferDeliveryOrderItemService transferDeliveryOrderItemService = ServiceProvider.GetService<TransferDeliveryOrderItemService>();
                    transferDeliveryOrderItemService.Username = this.Username;
                    TransferDeliveryOrderDetailService transferDeliveryOrderDetailService = ServiceProvider.GetService<TransferDeliveryOrderDetailService>();
                    transferDeliveryOrderDetailService.Username = this.Username;

                    HashSet<int> TransferDeliveryOrderItemIds = new HashSet<int>(
                        this.DbContext.TransferDeliveryOrderItems
                            .Where(p => p.DOId.Equals(Id))
                            .Select(p => p.Id)
                        );

                    foreach (int itemId in TransferDeliveryOrderItemIds)
                    {
                        HashSet<int> TransferDeliveryOrderDetailIds = new HashSet<int>(
                            this.DbContext.TransferDeliveryOrderDetails
                                .Where(p => p.DOItemId.Equals(itemId))
                                .Select(p => p.Id)
                            );

                        TransferDeliveryOrderItem transferDeliveryOrderItem = Model.TransferDeliveryOrderItem.FirstOrDefault(p => p.Id.Equals(itemId));

                        // cek item apakah dihapus (sesuai data yang diubah)
                        if (transferDeliveryOrderItem == null)
                        {
                            TransferDeliveryOrderItem item = this.DbContext.TransferDeliveryOrderItems
                                .Include(d => d.transferDeliveryOrderDetail)
                                .FirstOrDefault(p => p.Id.Equals(itemId));

                            if (item != null)
                            {
                                foreach (var detail in item.transferDeliveryOrderDetail)
                                {
                                    ExternalTransferOrderDetail externalTransferOrderDetail = this.DbContext.ExternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ETODetailId);
                                    externalTransferOrderDetail.DOQuantity = (externalTransferOrderDetail.DOQuantity) + (detail.DOQuantity);
                                    externalTransferOrderDetail.RemainingQuantity = (externalTransferOrderDetail.RemainingQuantity) - (detail.DOQuantity);
                                    externalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                                    externalTransferOrderDetail._LastModifiedAgent = "Service";
                                    externalTransferOrderDetail._LastModifiedBy = this.Username;

                                    if (externalTransferOrderDetail.DealQuantity == detail.RemainingQuantity)
                                    {
                                        TransferRequestDetail transRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                        transRequestDetail.Status = "Sudah diorder ke Penjualan";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;

                                        InternalTransferOrderDetail internTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                        transRequestDetail.Status = "Sudah diorder ke Penjualan";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;
                                    }
                                    else if (externalTransferOrderDetail.RemainingQuantity <= 0)
                                    {
                                        TransferRequestDetail transRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                        transRequestDetail.Status = "Sudah diorder semua ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;

                                        InternalTransferOrderDetail internTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                        transRequestDetail.Status = "Sudah diorder semua ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;
                                    }
                                    else if (externalTransferOrderDetail.RemainingQuantity > 0)
                                    {
                                        TransferRequestDetail transRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                        transRequestDetail.Status = "Sudah diorder sebagian ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;

                                        InternalTransferOrderDetail internTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                        transRequestDetail.Status = "Sudah diorder sebagian ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;
                                    }
                                }

                                InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(s => s.Id == item.ITOId);
                                internalTransferOrder.IsPost = false;
                            }

                            foreach (int detailId in TransferDeliveryOrderDetailIds)
                            {
                                TransferDeliveryOrderDetail transferDeliveryOrderDetail = this.DbContext.TransferDeliveryOrderDetails.FirstOrDefault(p => p.Id.Equals(detailId));

                                await transferDeliveryOrderDetailService.DeleteModel(detailId);
                            }

                            await transferDeliveryOrderItemService.DeleteModel(itemId);
                        }
                        else
                        {
                            await transferDeliveryOrderItemService.UpdateModel(itemId, transferDeliveryOrderItem);

                            foreach (int detailId in TransferDeliveryOrderDetailIds)
                            {
                                TransferDeliveryOrderDetail transferDeliveryOrderDetail = transferDeliveryOrderItem.transferDeliveryOrderDetail.FirstOrDefault(p => p.Id.Equals(detailId));

                                await transferDeliveryOrderDetailService.UpdateModel(detailId, transferDeliveryOrderDetail);
                            }
                        }
                    }

                    Updated = await this.UpdateAsync(Id, Model);

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

        public override void OnUpdating(int id, TransferDeliveryOrder model)
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
                    TransferDeliveryOrderItemService transferDeliveryOrderItemService = ServiceProvider.GetService<TransferDeliveryOrderItemService>();
                    transferDeliveryOrderItemService.Username = this.Username;
                    TransferDeliveryOrderDetailService transferDeliveryOrderDetailService = ServiceProvider.GetService<TransferDeliveryOrderDetailService>();
                    transferDeliveryOrderDetailService.Username = this.Username;

                    HashSet<int> TransferDeliveryOrderItemIds = new HashSet<int>(
                        this.DbContext.TransferDeliveryOrderItems
                            .Where(p => p.DOId.Equals(Id))
                            .Select(p => p.Id)
                        );

                    foreach (int itemId in TransferDeliveryOrderItemIds)
                    {
                        HashSet<int> TransferDeliveryOrderDetailIds = new HashSet<int>(
                            this.DbContext.TransferDeliveryOrderDetails
                                .Where(p => p.DOItemId.Equals(itemId))
                                .Select(p => p.Id)
                            );

                        foreach (int detailId in TransferDeliveryOrderDetailIds)
                        {
                            await transferDeliveryOrderDetailService.DeleteModel(detailId);

                            TransferDeliveryOrderItem item = this.DbContext.TransferDeliveryOrderItems
                                .Include(d => d.transferDeliveryOrderDetail)
                                .FirstOrDefault(p => p.Id.Equals(itemId));

                            if (item != null)
                            {
                                foreach (var detail in item.transferDeliveryOrderDetail)
                                {
                                    ExternalTransferOrderDetail externalTransferOrderDetail = this.DbContext.ExternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ETODetailId);
                                    externalTransferOrderDetail.DOQuantity = (externalTransferOrderDetail.DOQuantity) - (detail.DOQuantity);
                                    externalTransferOrderDetail.RemainingQuantity = (externalTransferOrderDetail.RemainingQuantity) + (detail.DOQuantity);
                                    externalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                                    externalTransferOrderDetail._LastModifiedAgent = "Service";
                                    externalTransferOrderDetail._LastModifiedBy = this.Username;

                                    if (externalTransferOrderDetail.DealQuantity == detail.RemainingQuantity)
                                    {
                                        TransferRequestDetail transRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                        transRequestDetail.Status = "Sudah diorder ke Penjualan";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;

                                        InternalTransferOrderDetail internTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                        transRequestDetail.Status = "Sudah diorder ke Penjualan";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;
                                    }
                                    else if (externalTransferOrderDetail.RemainingQuantity <= 0)
                                    {
                                        TransferRequestDetail transRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                        transRequestDetail.Status = "Sudah diorder semua ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;

                                        InternalTransferOrderDetail internTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                        transRequestDetail.Status = "Sudah diorder semua ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;
                                    }
                                    else if (externalTransferOrderDetail.RemainingQuantity > 0)
                                    {
                                        TransferRequestDetail transRequestDetail = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                                        transRequestDetail.Status = "Sudah diorder sebagian ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;

                                        InternalTransferOrderDetail internTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                        transRequestDetail.Status = "Sudah diorder sebagian ke Unit Pengirim";
                                        transRequestDetail._LastModifiedUtc = DateTime.UtcNow;
                                        transRequestDetail._LastModifiedAgent = "Service";
                                        transRequestDetail._LastModifiedBy = this.Username;
                                    }
                                }

                                InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(s => s.Id == item.ITOId);
                                internalTransferOrder.IsPost = false;
                            }
                        }

                        await transferDeliveryOrderItemService.DeleteModel(itemId);
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


        //public override void OnUpdating(int id, TransferDeliveryOrder model)
        //{
        //    base.OnUpdating(id, model);
        //    model._LastModifiedAgent = "Service";
        //    model._LastModifiedBy = this.Username;
        //}

        public override void OnDeleting(TransferDeliveryOrder model)
        {
            base.OnDeleting(model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
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
                        .Include(d => d.TransferDeliveryOrderItem)
                            .ThenInclude(d => d.transferDeliveryOrderDetail)
                        .ToList();
                    listData.ForEach(data => {
                        data.IsPosted = true;
                        data._LastModifiedUtc = DateTime.UtcNow;
                        data._LastModifiedAgent = "Service";
                        data._LastModifiedBy = this.Username;

                        foreach (var item in data.TransferDeliveryOrderItem)
                        {
                            foreach (var detail in item.transferDeliveryOrderDetail)
                            {
                                InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
                                internalTransferOrderDetail.Status = "Sudah diorder ke Penjualan";
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
                        .Include(d => d.TransferDeliveryOrderItem)
                            .ThenInclude(d => d.transferDeliveryOrderDetail)
                        .FirstOrDefault(tr => tr.Id == Id && tr._IsDeleted == false);
                    data.IsPosted = false;
                    data._LastModifiedUtc = DateTime.UtcNow;
                    data._LastModifiedAgent = "Service";
                    data._LastModifiedBy = this.Username;

                    foreach (var item in data.TransferDeliveryOrderItem)
                    {
                        foreach (var detail in item.transferDeliveryOrderDetail)
                        {
                            InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s => s.Id == detail.ITODetailId);
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

        public class Keys
        {
        }

        public bool CheckIdIsUsedByDeliveryOrder(int Id)
        {
            List<int> ETOinDO = new List<int> { 1, 2, 3 };
            return ETOinDO.Contains(Id);
        }

        public UnitViewModel GetFromExternalTransferOrderByExternalTransferOrderId(int Id)
        {
            InternalTransferOrder internalTransferOrder = this.DbContext.InternalTransferOrders.FirstOrDefault(p => p.Id.Equals(Id));
            return new UnitViewModel()
            {
                _id = internalTransferOrder.UnitId,
                code = internalTransferOrder.UnitCode,
                name = internalTransferOrder.UnitName,
            };
        }

        // public async Task<TransferDeliveryOrder> ReadModelByQuery(string Supplier)
        //{
        //    TransferDeliveryOrder result = new TransferDeliveryOrder();
        //    result = await this.DbSet.Where(TransferDeliveryOrder => String.Equals(TransferDeliveryOrder.DONo, Supplier) && !TransferDeliveryOrder._IsDeleted).OrderByDescending(x => x._LastModifiedUtc).FirstOrDefaultAsync();
        //
        //    if (result == null)
        //    {
        // result = new TransferDeliveryOrder();
        //  return result;
        //}
        //  return result;
        //}

        public List<TransferDeliveryOrder> ReadModelUnused(string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}", List<int> CurrentUsed = null)
        {
            IQueryable<TransferDeliveryOrder> Query = this.DbContext.TransferDeliveryOrders;

            List<string> SearchAttributes = new List<string>()
            {
                "DONo"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            HashSet<int> transferShippingOrderDetails = new HashSet<int>(this.DbContext.TransferShippingOrderDetails.Select(p => p.DODetailId));

            Query = Query
                .Select(result => new TransferDeliveryOrder
                {
                    Id = result.Id,
                    DONo = result.DONo,
                    SupplierId = result.SupplierId,
                    IsPosted = result.IsPosted,
                    TransferDeliveryOrderItem = result.TransferDeliveryOrderItem
                        .Select(item => new TransferDeliveryOrderItem
                        {
                            transferDeliveryOrderDetail = item.transferDeliveryOrderDetail
                                .Select(detail => new TransferDeliveryOrderDetail
                                {
                                    RemainingQuantity = detail.RemainingQuantity
                                })
                                .Where(s => !transferShippingOrderDetails.Contains(s.Id))
                                .Where(s => s.RemainingQuantity > 0)
                                .ToList()
                        })
                        .Where(s => s.transferDeliveryOrderDetail.Count > 0)
                        .ToList(),

                    _LastModifiedUtc = result._LastModifiedUtc
                })
                .Where(s => s.TransferDeliveryOrderItem.Count > 0);

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            List<TransferDeliveryOrder> Data = Query.ToList<TransferDeliveryOrder>();

            return Data;

        }
    }
}
