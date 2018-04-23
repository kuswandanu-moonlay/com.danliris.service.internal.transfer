using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel
{
    public class TransferRequestViewModel : BasicViewModel, IValidatableObject
    {
        public string trNo { get; set; }
        public DateTime trDate { get; set; }
        public DateTime requestedArrivalDate { get; set; }
        public UnitViewModel unit { get; set; }
        public string unitId { get; set; }
        public string  unitCode{get;set;}
        public string unitName { get; set; }
        public string divisionId { get; set; }
        public string divisionCode { get; set; }
        public string divisionName { get; set; }
        public CategoryViewModel category { get; set; }
        public string categoryId { get; set; }
        public string categoryCode { get; set; }
        public string categoryName { get; set; }
        public bool isPosted { get; set; }
        public bool isCanceled { get; set; }
        public string remark { get; set; }
        public List<TransferRequestDetailViewModel> details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            int Count = 0;

            if (this.trDate == null || this.trDate == DateTime.MinValue)
                yield return new ValidationResult("Tanggal TR harus diisi", new List<string> { "trDate" });

            if (this.requestedArrivalDate == null || this.requestedArrivalDate == DateTime.MinValue)
                yield return new ValidationResult("Tanggal diminta datang harus diisi", new List<string> { "requestedArrivalDate" });
            else if (this.trDate > this.requestedArrivalDate)
                yield return new ValidationResult("Tanggal diminta datang tidak boleh kurang dari Tanggal TR", new List<string> { "requestedArrivalDate" });

            if (this.unit == null || string.IsNullOrWhiteSpace(this.unit._id))
                yield return new ValidationResult("Unit harus diisi", new List<string> { "unit" });

            if (this.category == null || string.IsNullOrWhiteSpace(this.category._id))
                yield return new ValidationResult("Kategori harus diisi", new List<string> { "category" });

            if (this.details == null || this.details.Count.Equals(0))
            {
                yield return new ValidationResult("Detail harus diisi", new List<string> { "details" });
            }
            else
            {
                string transferRequestDetailError = "[";

                foreach (TransferRequestDetailViewModel transferRequestDetail in this.details)
                {
                    transferRequestDetailError += "{ ";
                   

                    if (transferRequestDetail.quantity == null || transferRequestDetail.quantity <= 0)
                    {
                        Count++;
                        transferRequestDetailError += "quantity: 'Jumlah harus lebih besar dari 0', ";
                    }
                    if (transferRequestDetail.product == null ||  string.IsNullOrWhiteSpace(transferRequestDetail.product._id))
                    {
                        Count++;
                        transferRequestDetailError += "product: 'Barang harus diisi', ";
                    }

                    transferRequestDetailError += "}, ";
                }

                transferRequestDetailError += "]";

                if (Count > 0)
                {
                    yield return new ValidationResult(transferRequestDetailError, new List<string> { "details" });
                }
            }
        }
    }
}
