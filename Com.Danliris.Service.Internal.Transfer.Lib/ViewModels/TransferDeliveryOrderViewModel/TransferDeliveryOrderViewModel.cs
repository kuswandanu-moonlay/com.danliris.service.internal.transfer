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

        public DateTime ArrivalDate { get; set; }

        public SupplierViewModel Supplier { get; set; }

        public DivisionViewModel Division { get; set; }

        public string Remark { get; set; }

        public List<TransferDeliveryOrderItemViewModel> items { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)

        {

            if (string.IsNullOrWhiteSpace(this.DONo))

                yield return new ValidationResult("Nomor Surat Jalan harus diisi", new List<string> { "Code" });

            if (this.DODate == null || this.DODate == DateTime.MinValue)

                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "DeliveryOrderDate" });

            if (this.ArrivalDate == null || this.ArrivalDate == DateTime.MinValue)

                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "ArrivalDate" });

            if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier._id))

                yield return new ValidationResult("Suplier harus diisi", new List<string> { "SupplierId" });

        }
    }
}