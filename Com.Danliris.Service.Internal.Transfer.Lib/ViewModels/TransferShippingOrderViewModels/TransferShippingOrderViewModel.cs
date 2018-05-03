using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels
{
    public class TransferShippingOrderViewModel : BasicViewModel, IValidatableObject
    {
        public string SONo { get; set; }
        public DateTime SODate { get; set; }
        public SupplierViewModel Supplier { get; set; }
        public string Remark { get; set; }
        public bool IsPosted { get; set; }

        public List<TransferShippingOrderItemViewModel> TransferShippingOrderItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier._id))
                yield return new ValidationResult("Supplier is required", new List<string> { "Supplier" });

            int itemErrorCount = 0;
            int detailErrorCount = 0;

            if (TransferShippingOrderItems == null || TransferShippingOrderItems.Count.Equals(0))
            {
                yield return new ValidationResult("Transfer Shipping Order Item is required", new List<string> { "TransferShippingOrderItemsCount" });
            }
            else
            {
                string transferShippingOrderItemError = "[";

                foreach (TransferShippingOrderItemViewModel Item in TransferShippingOrderItems)
                {
                    transferShippingOrderItemError += "{ ";

                    if (string.IsNullOrWhiteSpace(Item.DONo))
                    {
                        itemErrorCount++;
                        transferShippingOrderItemError += "TransferDeliveryOrder: 'TransferDeliveryOrder is required', ";
                    }

                    string transferShippingOrderDetailError = "[";

                    foreach (TransferShippingOrderDetailViewModel Detail in Item.TransferShippingOrderDetails)
                    {
                        transferShippingOrderDetailError += "{ ";

                        if (Detail.DeliveryQuantity <= 0)
                        {
                            detailErrorCount++;
                            transferShippingOrderDetailError += "DeliveryQuantity: 'DeliveryQuantity should be more than 0', ";
                        }

                        if (Detail.Uom == null)
                        {
                            detailErrorCount++;
                            transferShippingOrderDetailError += "Uom: 'Uom is required', ";
                        }

                        transferShippingOrderDetailError += " }, ";
                    }

                    transferShippingOrderDetailError += "]";

                    if (detailErrorCount > 0)
                    {
                        itemErrorCount++;
                        transferShippingOrderItemError += string.Concat("TransferShippingOrderDetails: ", transferShippingOrderDetailError);
                    }

                    transferShippingOrderItemError += " }, ";
                }

                transferShippingOrderItemError += "]";

                if (itemErrorCount > 0)
                    yield return new ValidationResult(transferShippingOrderItemError, new List<string> { "TransferShippingOrderItems" });
            }

        }
    }
}
