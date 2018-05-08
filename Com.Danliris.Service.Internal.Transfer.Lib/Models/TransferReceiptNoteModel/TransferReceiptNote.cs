using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferReceiptNoteModel
{
    public class TransferReceiptNote : StandardEntity, IValidatableObject
    {
        public string TRNNo { get; set; }
        public string DivisionId { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public int SOId { get; set; }
        public string SONo { get; set; }
        public DateTime ReceiptDate { get; set; }
        public bool IsStorage { get; set; }
        public string StorageId { get; set; }
        public string StorageCode { get; set; }
        public string StorageName { get; set; }
        public string Remark { get; set; }

        public virtual ICollection<TransferReceiptNoteDetail> TransferReceiptNoteDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
