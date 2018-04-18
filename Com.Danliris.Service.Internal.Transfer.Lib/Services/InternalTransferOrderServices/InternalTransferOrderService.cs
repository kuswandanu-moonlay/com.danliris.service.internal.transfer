
using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Interfaces;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels;
using Com.Moonlay.NetCore.Lib;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.InternalTransferOrderServices
{
    public class InternalTransferOrderService : BasicService<InternalTransferDbContext, InternalTransferOrder>, IMap<InternalTransferOrder, InternalTransferOrderViewModel>
    {
        public InternalTransferOrderService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public async Task<InternalTransferOrder> CustomCodeGenerator(InternalTransferOrder Model)
        {
            var lastData = await this.DbSet.Where(w => string.Equals(w.UnitCode, Model.UnitCode)).OrderByDescending(o => o._CreatedUtc).FirstOrDefaultAsync();

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");
            string Month = Now.ToString("MM");

            if (lastData == null)
            {
                Model.AutoIncrementNumber = 1;
                string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                Model.ITONo = $"ITO{Model.UnitCode}{Month}{Year}{Number}";
            }
            else
            {
                if (lastData._CreatedUtc.Year < Now.Year)
                {
                    Model.AutoIncrementNumber = 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.ITONo = $"ITO{Model.UnitCode}{Month}{Year}{Number}";
                }
                else
                {
                    Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.ITONo = $"ITO{Model.UnitCode}{Month}{Year}{Number}";
                }
            }

            return Model;
        }

        public override async Task<InternalTransferOrder> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id) && d._IsDeleted.Equals(false))
                .Include(d => d.InternalTransferOrderDetails)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> CreateModel(InternalTransferOrder Model)
        {
            int Created = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    
                    Model = await this.CustomCodeGenerator(Model);
                    Created = await this.CreateAsync(Model);
                    foreach (var detail in Model.InternalTransferOrderDetails)
                    {

                        TransferRequestDetail trd = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                        trd.Status = "Sudah diterima Pembelian";
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
        public override async Task<int> DeleteModel(int Id)
        {
            InternalTransferOrderDetailService internalTransferOrderDetailService = this.ServiceProvider.GetService<InternalTransferOrderDetailService>();

            int Deleted = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    InternalTransferOrder Model = await this.ReadModelById(Id);
                    Deleted = await this.DeleteAsync(Id);

                    HashSet<int> internalTransferOrderDetails = new HashSet<int>(internalTransferOrderDetailService.DbSet
                        .Where(p => p.ITOId.Equals(Id))
                        .Select(p => p.Id));

                    internalTransferOrderDetailService.Username = this.Username;

                    foreach (int detail in internalTransferOrderDetails)
                    {
                        await internalTransferOrderDetailService.DeleteModel(detail);
                    }

                    foreach (var detail in Model.InternalTransferOrderDetails)
                    {

                        TransferRequestDetail trd = this.DbContext.TransferRequestDetails.FirstOrDefault(s => s.Id == detail.TRDetailId);
                        trd.Status = "Belum diterima Pembelian";
                    }
                    this.DbContext.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return Deleted;
        }
        public override Tuple<List<InternalTransferOrder>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<InternalTransferOrder> Query = this.DbContext.InternalTransferOrders;

            List<string> SearchAttributes = new List<string>()
            {
                "ITONo", "TRNo"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "ITONo","TRDate","Active", "_CreatedBy", "TRId", "TRNo" ,"RequestedArrivalDate","CategoryName","DivisionName","UnitName","IsPost"};

            Query = Query
                .Select(mdn => new InternalTransferOrder
                {
                    Id = mdn.Id,
                    ITONo = mdn.ITONo,
                    TRDate=mdn.TRDate,
                    RequestedArrivalDate=mdn.RequestedArrivalDate,
                    CategoryName=mdn.CategoryName,
                    UnitName=mdn.UnitName,
                    DivisionName=mdn.DivisionName,
                    _CreatedBy =mdn._CreatedBy,
                    TRId = mdn.TRId,
                    TRNo = mdn.TRNo,
                    _CreatedUtc = mdn._CreatedUtc,
                    _LastModifiedUtc = mdn._LastModifiedUtc
                }).Where(s=>s._IsDeleted == false);

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<InternalTransferOrder> pageable = new Pageable<InternalTransferOrder>(Query, Page - 1, Size);
            List<InternalTransferOrder> Data = pageable.Data.ToList<InternalTransferOrder>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override void OnCreating(InternalTransferOrder model)
        {
            if (model.InternalTransferOrderDetails.Count > 0)
            {
                InternalTransferOrderDetailService internalTransferOrderDetailService = this.ServiceProvider.GetService<InternalTransferOrderDetailService>();
                internalTransferOrderDetailService.Username = this.Username;
                foreach (InternalTransferOrderDetail detail in model.InternalTransferOrderDetails)
                {
                    internalTransferOrderDetailService.OnCreating(detail);
                }
            }

            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, InternalTransferOrder model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(InternalTransferOrder model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }

        public InternalTransferOrder MapToModel(InternalTransferOrderViewModel viewModel)
        {
            InternalTransferOrder model = new InternalTransferOrder();
            model.ITONo = viewModel.ITONo;
            model.TRNo = viewModel.TRNo;
            model.TRId = viewModel.TRId;
            model.RequestedArrivalDate = viewModel.RequestedArrivalDate;
            model.Remarks = viewModel.Remarks;
            model.TRDate = viewModel.TRDate;
            model.UnitId = viewModel.UnitId;
            model.UnitCode = viewModel.UnitCode;
            model.UnitName = viewModel.UnitName;
            model.CategoryId = viewModel.CategoryId;
            model.CategoryCode = viewModel.CategoryCode;
            model.CategoryName = viewModel.CategoryName;
            model.DivisionId = viewModel.DivisionId;
            model.DivisionCode = viewModel.DivisionCode;
            model.DivisionName = viewModel.DivisionName;
            PropertyCopier<InternalTransferOrderViewModel, InternalTransferOrder>.Copy(viewModel, model);

            model.InternalTransferOrderDetails = new List<InternalTransferOrderDetail>();
            foreach (InternalTransferOrderDetailViewModel detail in viewModel.InternalTransferOrderDetails)
            {
                InternalTransferOrderDetail internalTransferOrderDetail = new InternalTransferOrderDetail();
                PropertyCopier<InternalTransferOrderDetailViewModel, InternalTransferOrderDetail>.Copy(detail, internalTransferOrderDetail);
                internalTransferOrderDetail.Quantity = (double)detail.Quantity;
                internalTransferOrderDetail.Status = "TO Internal belum diorder";
                model.InternalTransferOrderDetails.Add(internalTransferOrderDetail);
                
            }

            return model;
        }

        public InternalTransferOrderViewModel MapToViewModel(InternalTransferOrder model)
        {
            InternalTransferOrderViewModel viewModel = new InternalTransferOrderViewModel();

            PropertyCopier<InternalTransferOrder, InternalTransferOrderViewModel>.Copy(model, viewModel);

            viewModel.InternalTransferOrderDetails = new List<InternalTransferOrderDetailViewModel>();
            if (model.InternalTransferOrderDetails != null)
            {
                foreach (InternalTransferOrderDetail detail in model.InternalTransferOrderDetails)
                {
                    InternalTransferOrderDetailViewModel internalTransferOrderDetailViewModel = new InternalTransferOrderDetailViewModel();
                    PropertyCopier<InternalTransferOrderDetail, InternalTransferOrderDetailViewModel>.Copy(detail, internalTransferOrderDetailViewModel);

                    internalTransferOrderDetailViewModel.Quantity = detail.Quantity;
                    viewModel.InternalTransferOrderDetails.Add(internalTransferOrderDetailViewModel);
                }
            }

            return viewModel;
        }

        public async Task<int> SplitUpdate(int Id,InternalTransferOrderViewModel viewModel, InternalTransferOrder Model)
        {
            InternalTransferOrderDetailService internalTransferOrderDetailService = this.ServiceProvider.GetService<InternalTransferOrderDetailService>();
            internalTransferOrderDetailService.Username = this.Username;

            int Updated = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {   
                    foreach (InternalTransferOrderDetailViewModel detail in viewModel.InternalTransferOrderDetails)
                    {
                        InternalTransferOrderDetail internalTransferOrderDetail = this.DbContext.InternalTransferOrderDetails.FirstOrDefault(s=>s.Id== detail.Id);
                        if (internalTransferOrderDetail != null)
                        {
                            if(internalTransferOrderDetail.Quantity != detail.Quantity)
                            internalTransferOrderDetail.Quantity -= detail.Quantity;
                            internalTransferOrderDetail._LastModifiedBy = this.Username;
                            internalTransferOrderDetail._LastModifiedUtc = DateTime.UtcNow;
                            this.DbContext.SaveChanges();
                        }
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                }
            }

            return Updated;
        }
    }
}