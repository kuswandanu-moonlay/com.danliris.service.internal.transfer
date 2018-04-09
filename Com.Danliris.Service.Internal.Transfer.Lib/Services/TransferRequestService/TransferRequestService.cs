using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Interfaces;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels;
using Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel;
using Com.Moonlay.NetCore.Lib;
using Com.Moonlay.NetCore.Lib.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferRequestService
{
    public class TransferRequestService : BasicService<InternalTransferDbContext, TransferRequest>, IMap<TransferRequest, TransferRequestViewModel>
    {
        public TransferRequestService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public override Tuple<List<TransferRequest>, int, Dictionary<string, string>, List<string>> ReadModel(int Page = 1, int Size = 25, string Order = "{}", List<string> Select = null, string Keyword = null, string Filter = "{}")
        {
            IQueryable<TransferRequest> Query = this.DbContext.TransferRequests;

            List<string> SearchAttributes = new List<string>()
            {
                "TRNo"
            };

            Query = ConfigureSearch(Query, SearchAttributes, Keyword);

            List<string> SelectedFields = new List<string>()
            {
                "Id", "TRNo", "_CreatedUtc", "CategoryName", "RequestedArrivalDate", "TRDate", "UnitCode","IsPosted","IsCanceled"
            };

            Query = Query
                .Select(tr => new TransferRequest
                {
                    Id = tr.Id,
                    TRNo = tr.TRNo,
                    _CreatedUtc = tr._CreatedUtc,
                    CategoryName = tr.CategoryName,
                    RequestedArrivalDate = tr.RequestedArrivalDate,
                    TRDate = tr.TRDate,
                    _LastModifiedUtc = tr._LastModifiedUtc,
                    UnitCode= tr.UnitCode,
                    IsPosted=tr.IsPosted,
                    IsCanceled=tr.IsCanceled
                });

            Dictionary<string, string> FilterDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Filter);
            Query = ConfigureFilter(Query, FilterDictionary);

            Dictionary<string, string> OrderDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(Order);
            Query = ConfigureOrder(Query, OrderDictionary);

            Pageable<TransferRequest> pageable = new Pageable<TransferRequest>(Query, Page - 1, Size);
            List<TransferRequest> Data = pageable.Data.ToList<TransferRequest>();
            int TotalData = pageable.TotalCount;

            return Tuple.Create(Data, TotalData, OrderDictionary, SelectedFields);
        }

        public TransferRequest MapToModel(TransferRequestViewModel viewModel)
        {
            TransferRequest model = new TransferRequest();

            PropertyCopier<TransferRequestViewModel, TransferRequest>.Copy(viewModel, model);

            model.UnitId = viewModel.unit._id;
            model.UnitCode = viewModel.unit.code;
            model.UnitName = viewModel.unit.name;
            model.DivisionId = viewModel.unit.divisionId;
            model.DivisionCode = viewModel.unit.divisionCode;
            model.DivisionName = viewModel.unit.divisionName;
            model.CategoryCode = viewModel.category.code;
            model.CategoryId = viewModel.category._id;
            model.CategoryName = viewModel.category.name;
            model.Remark = viewModel.remark;

            model.TransferRequestDetails = new List<TransferRequestDetail>();

            foreach (TransferRequestDetailViewModel transferRequestDetailViewModel in viewModel.details)
            {
                TransferRequestDetail transferRequestDetail = new TransferRequestDetail();

                PropertyCopier<TransferRequestDetailViewModel, TransferRequestDetail>.Copy(transferRequestDetailViewModel, transferRequestDetail);


                transferRequestDetail.ProductId = transferRequestDetailViewModel.product._id;
                transferRequestDetail.ProductCode = transferRequestDetailViewModel.product.name;
                transferRequestDetail.ProductName = transferRequestDetailViewModel.product.name;
                transferRequestDetail.Quantity = transferRequestDetailViewModel.quantity;
                transferRequestDetail.UomId = transferRequestDetailViewModel.uom._id;
                transferRequestDetail.UomUnit = transferRequestDetailViewModel.uom.unit;
                transferRequestDetail.ProductRemark = transferRequestDetailViewModel.productRemark;

                model.TransferRequestDetails.Add(transferRequestDetail);
            }

            return model;
        }

        public TransferRequestViewModel MapToViewModel(TransferRequest model)
        {
            TransferRequestViewModel viewModel = new TransferRequestViewModel();

            PropertyCopier<TransferRequest, TransferRequestViewModel>.Copy(model, viewModel);

            UnitViewModel Unit = new UnitViewModel()
            {
                _id = model.UnitId,
                code = model.UnitCode,
                name = model.UnitName,
                divisionId=model.DivisionId,
                divisionCode=model.DivisionCode,
                divisionName=model.DivisionName
            };

            CategoryViewModel Category = new CategoryViewModel()
            {
                _id = model.CategoryId,
                code = model.CategoryName,
                name = model.CategoryName,
            };

            viewModel.trNo = model.TRNo;
            viewModel.unit = Unit;
            viewModel.category = Category;
            viewModel.remark = model.Remark;

            viewModel.details = new List<TransferRequestDetailViewModel>();
            if (model.TransferRequestDetails != null)
            {
                foreach (TransferRequestDetail transferRequestDetail in model.TransferRequestDetails)
                {
                    TransferRequestDetailViewModel transferRequestDetailViewModel = new TransferRequestDetailViewModel();
                    PropertyCopier<TransferRequestDetail, TransferRequestDetailViewModel>.Copy(transferRequestDetail, transferRequestDetailViewModel);

                    UomViewModel Uom = new UomViewModel()
                    {
                        _id = transferRequestDetail.UomId,
                        unit = transferRequestDetail.UomUnit
                    };
                    transferRequestDetailViewModel.uom = Uom;

                    ProductViewModel Product = new ProductViewModel()
                    {
                        _id = transferRequestDetail.ProductId,
                        code = transferRequestDetail.ProductCode,
                        name = transferRequestDetail.ProductName
                    };
                    transferRequestDetailViewModel.product = Product;

                    viewModel.details.Add(transferRequestDetailViewModel);
                }
            }

            return viewModel;
        }

        public async Task<TransferRequest> CustomCodeGenerator(TransferRequest Model)
        {
            var lastData = await this.DbSet.Where(w => string.Equals(w.UnitCode, Model.UnitCode)).OrderByDescending(o => o._CreatedUtc).FirstOrDefaultAsync();

            DateTime Now = DateTime.Now;
            string Year = Now.ToString("yy");

            if (lastData == null)
            {
                Model.AutoIncrementNumber = 1;
                string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                Model.TRNo = $"TR{Model.UnitCode}{Year}{Number}";
            }
            else
            {
                if (lastData._CreatedUtc.Year < Now.Year)
                {
                    Model.AutoIncrementNumber = 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.TRNo = $"TR{Model.UnitCode}{Year}{Number}";
                }
                else
                {
                    Model.AutoIncrementNumber = lastData.AutoIncrementNumber + 1;
                    string Number = Model.AutoIncrementNumber.ToString().PadLeft(4, '0');
                    Model.TRNo = $"TR{Model.UnitCode}{Year}{Number}";
                }
            }

            return Model;
        }

        public override async Task<TransferRequest> ReadModelById(int id)
        {
            return await this.DbSet
                .Where(d => d.Id.Equals(id) && d._IsDeleted.Equals(false))
                .Include(d => d.TransferRequestDetails)
                .FirstOrDefaultAsync();
        }

        public override async Task<int> CreateModel(TransferRequest Model)
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
        public override void OnCreating(TransferRequest model)
        {
            if (model.TransferRequestDetails.Count > 0)
            {
                TransferRequestDetailService transferRequestDetailService = this.ServiceProvider.GetService<TransferRequestDetailService>();

                transferRequestDetailService.Username = this.Username;
                foreach (TransferRequestDetail transferRequestDetail in model.TransferRequestDetails)
                {
                    transferRequestDetailService.OnCreating(transferRequestDetail);
                }
            }

            base.OnCreating(model);
            model._CreatedAgent = "Service";
            model._CreatedBy = this.Username;
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnUpdating(int id, TransferRequest model)
        {
            base.OnUpdating(id, model);
            model._LastModifiedAgent = "Service";
            model._LastModifiedBy = this.Username;
        }

        public override void OnDeleting(TransferRequest model)
        {
            base.OnDeleting(model);
            model._DeletedAgent = "Service";
            model._DeletedBy = this.Username;
        }
    }
}
