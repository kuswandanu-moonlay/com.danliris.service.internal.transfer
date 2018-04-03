using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels
{
    public class TransferDeliveryOrderViewModel : BasicViewModel, IValidatableObject
    {
        public string Code { get; set; }

        public DateTime DeliveryOrderDate { get; set; }

        public DateTime ArrivedDate { get; set; }

        public SupplierVM Supplier { get; set; }

        public class SupplierVM
        {
            public string _id { get; set; }
            public string code { get; set; }
            public string name { get; set; }
        }

        public string Remark { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)

        {

            if (string.IsNullOrWhiteSpace(this.Code))

                yield return new ValidationResult("Nomor Surat Jalan harus diisi", new List<string> { "Code" });

            if (this.DeliveryOrderDate == null || this.DeliveryOrderDate == DateTime.MinValue)

                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "DeliveryOrderDate" });

            if (this.ArrivedDate == null || this.ArrivedDate == DateTime.MinValue)

                yield return new ValidationResult("Tanggal harus diisi", new List<string> { "ArrivedDate" });

            if (this.Supplier == null || string.IsNullOrWhiteSpace(this.Supplier._id))

                yield return new ValidationResult("Suplier harus diisi", new List<string> { "SupplierId" });

        }
    }
}