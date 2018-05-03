using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

            if (TransferShippingOrderItems == null || TransferShippingOrderItems.Count.Equals(0))
            {
                yield return new ValidationResult("Transfer Shipping Order Item is required", new List<string> { "TransferShippingOrderItemsCount" });
            }
        }
    }
}
