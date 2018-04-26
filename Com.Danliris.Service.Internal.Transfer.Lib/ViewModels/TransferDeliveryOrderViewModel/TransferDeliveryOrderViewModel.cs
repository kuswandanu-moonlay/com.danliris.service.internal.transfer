using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

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

            if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier._id))

                yield return new ValidationResult("Suplier harus diisi", new List<string> { "SupplierId" });

        }
    }
}