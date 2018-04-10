
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

        public override async Task<int> UpdateModel(int Id, InternalTransferOrder Model)
        {
            InternalTransferOrderDetailService internalTransferOrderDetailService = this.ServiceProvider.GetService<InternalTransferOrderDetailService>();
            internalTransferOrderDetailService.Username = this.Username;

            int Updated = 0;
            using (var transaction = this.DbContext.Database.BeginTransaction())
            {
                try
                {
                    HashSet<int> internalTransferOrderDetail = new HashSet<int>(internalTransferOrderDetailService.DbSet
                        .Where(w => w.ITOId.Equals(Id))
                        .Select(s => s.Id));
                    Updated = await this.UpdateAsync(Id, Model);


                    foreach (int detail in internalTransferOrderDetail)
                    {
                        InternalTransferOrderDetail model = Model.InternalTransferOrderDetails.FirstOrDefault(prop => prop.Id.Equals(internalTransferOrderDetail));
                        if (model == null)
                        {
                            await internalTransferOrderDetailService.DeleteModel(detail);
                        }
                        else
                        {
                            await internalTransferOrderDetailService.UpdateModel(detail, model);
                        }
                    }

                    foreach (InternalTransferOrderDetail detail in Model.InternalTransferOrderDetails)
                    {
                        if (detail.Id.Equals(0))
                        {
                            await internalTransferOrderDetailService.CreateModel(detail);
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


                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                }
            }

            return Deleted;
        }

        public Tuple<List<InternalTransferOrder>, int, Dictionary<string, string>, List<string>> ReadModelPosted(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<InternalTransferOrder> Query = this.DbContext.InternalTransferOrders;

            List<string> SearchAttributes = new List<string>()
            {
                "IsPosted"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "TRNo", "TRDate", "UnitId", "UnitCode", "UnitName", "Remark", "DivisionId", "DivisionCode", "DivisionName","CategoryId","CategoryCode","CategoryName"
            };

            Query = Query
                .Select(stn => new InternalTransferOrder
                {
                    Id = stn.Id,
                    TRId = stn.TRId,
                    _CreatedUtc = stn._CreatedUtc,
                    TRNo = stn.TRNo,
                    _LastModifiedUtc = stn._LastModifiedUtc,
                    _CreatedBy = stn._CreatedBy
                }).Where(w => !string.Equals(w._CreatedBy, Username));

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<InternalTransferOrder> pageable = new Pageable<InternalTransferOrder>(Query, Page - 1, Size);
            List<InternalTransferOrder> Data = pageable.Data.ToList();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public override Tuple<List<InternalTransferOrder>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<InternalTransferOrder> Query = this.DbContext.InternalTransferOrders;

            List<string> SearchAttributes = new List<string>()
            {
                "InternalTransferOrderNo"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "InternalTransferOrderNo","Active", "_CreatedUtc", "TransferRequestNo" };

            Query = Query
                .Select(mdn => new InternalTransferOrder
                {
                    Id = mdn.Id,
                    ITONo = mdn.ITONo,
                    Active = true,

                    TRNo = mdn.TRNo,
                    _CreatedUtc = mdn._CreatedUtc,
                    _LastModifiedUtc = mdn._LastModifiedUtc
                });

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
            PropertyCopier<InternalTransferOrderViewModel, InternalTransferOrder>.Copy(viewModel, model);

            model.InternalTransferOrderDetails = new List<InternalTransferOrderDetail>();
            foreach (InternalTransferOrderDetailViewModel detail in viewModel.InternalTransferOrderDetails)
            {
                InternalTransferOrderDetail internalTransferOrderDetail = new InternalTransferOrderDetail();
                PropertyCopier<InternalTransferOrderDetailViewModel, InternalTransferOrderDetail>.Copy(detail, internalTransferOrderDetail);
                internalTransferOrderDetail.Quantity = (double)detail.Quantity;
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

    }
}