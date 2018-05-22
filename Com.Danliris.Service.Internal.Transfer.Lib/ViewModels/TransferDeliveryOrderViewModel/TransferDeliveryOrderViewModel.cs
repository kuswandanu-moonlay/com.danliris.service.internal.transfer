using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferDeliveryOrderModel;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.TransferDeliveryOrderService;
using Com.Danliris.Service.Internal.Transfer.Lib.Services.ExternalTransferOrderServices;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferDeliveryOrderViewModel
{
    public class TransferDeliveryOrderViewModel : BasicViewModel, IValidatableObject
    {
        public string DONo { get; set; }

        public DateTime DODate { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public DivisionViewModel Division { get; set; }

        public string Remark { get; set; }

        public bool IsPosted { get; set; }

        public List<TransferDeliveryOrderItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)

        {

            if (this.DODate == null || this.DODate == DateTime.MinValue)

                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "DODate" });

            if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier.name))

                yield return new ValidationResult("Unit Pengirim harus diisi", new List<string> { "Supplier" });

            if (this.Division == null || string.IsNullOrWhiteSpace(this.Division.name))

                yield return new ValidationResult("Divisi Pemesan harus diisi", new List<string> { "Division" });

            int itemErrorCount = 0;
            int detailErrorCount = 0;

            if (items == null || items.Count.Equals(0))
            {
                yield return new ValidationResult("Items Harus Diisi", new List<string> { "TransferDeliveryOrderItemsCount" });
            }
            else
            {
                string transferDeliveryOrderItemError = "[";

                foreach (TransferDeliveryOrderItemViewModel Item in items)
                {
                    transferDeliveryOrderItemError += "{ ";

                    if (string.IsNullOrWhiteSpace(Item.ETONo))
                    {
                        itemErrorCount++;
                        transferDeliveryOrderItemError += "ETONo: 'Nomor TO Eksternal Harus Diisi', ";
                    }
                    else
                    {
                        TransferDeliveryOrderItemService transferDeliveryOrderItemService = (TransferDeliveryOrderItemService)validationContext.GetService(typeof(TransferDeliveryOrderItemService));
                        //List<ExternalTransferOrderItem> itemsData = externalTransferOrderItemService.ReadModel(Filter: "{ InternalTransferOrderNo : '" + Item.InternalTransferOrderNo + "' }").Item1;
                        //itemsData = itemsData.Where(w => w.ExternalTransferOrderId != this.Id).ToList();
                        List<TransferDeliveryOrderItem> itemsData = transferDeliveryOrderItemService.DbSet.Where(
                                w => w.DOId != this.Id && w.ETONo.Equals(Item.ETONo)
                            )
                            .ToList();
                        if (itemsData.Count > 0)
                        {
                            itemErrorCount++;
                            transferDeliveryOrderItemError += "ETONo: 'Nomor TO Eksternal Sudah Digunakan', ";
                        }

                    }

                    string transferDeliveryOrderDetailError = "[";
                    if (Item.details != null)
                    {
                        foreach (TransferDeliveryOrderDetailViewModel Detail in Item.details)
                        {
                            transferDeliveryOrderDetailError += "{ ";

                           // ExternalTransferOrderDetailService externalTransferOrderDetailService = (ExternalTransferOrderDetailService)validationContext.GetService(typeof(ExternalTransferOrderDetailService));
                           // List<ExternalTransferOrderDetail> detailsData = externalTransferOrderDetailService.DbSet.Where(
                           //    w => w.DOId != this.Id && w.ETONo.Equals(Item.ETONo)
                           //)
                           //.ToList();

                            //if (Detail.DOQuantity > Detail.RemainingQuantity)
                            //{
                            //    detailErrorCount++;
                            //    transferDeliveryOrderDetailError += "DOQuantity: 'Jumlah DO tidak boleh lebih dari (Jumlah pada RemainingQuantity di tabel ExternalTransferOrderDetails)', ";
                            //}

                            transferDeliveryOrderDetailError += " }, ";
                        }
                    }
                    transferDeliveryOrderDetailError += "]";

                    if (detailErrorCount > 0)
                    {
                        itemErrorCount++;
                        transferDeliveryOrderItemError += string.Concat("details: ", transferDeliveryOrderDetailError);
                    }

                    transferDeliveryOrderItemError += " }, ";
                }

                transferDeliveryOrderItemError += "]";

                if (itemErrorCount > 0)
                    yield return new ValidationResult(transferDeliveryOrderItemError, new List<string> { "items" });
            }

        }
    }
}