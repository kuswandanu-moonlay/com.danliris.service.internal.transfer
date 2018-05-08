using Com.Moonlay.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferReceiptNoteModel
{
    public class TransferReceiptNoteDetail : StandardEntity, IValidatableObject
    {
        public int TRNId { get; set; }
        public int SOId { get; set; }
        public int SODetailId { get; set; }
        public int DOId { get; set; }
        public string DONo { get; set; }
        public int DODetailId { get; set; }
        public int ETOId { get; set; }
        public string ETONo { get; set; }
        public int ETODetailId { get; set; }
        public int TRId { get; set; }
        public string TRNo { get; set; }
        public int TRDetailId { get; set; }
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Grade { get; set; }
        public double DeliveryQuantity { get; set; }
        public double ReceiptQuantity { get; set; }
        public string UomId { get; set; }
        public string UomUnit { get; set; }
        public string ProductRemark { get; set; }

        public virtual TransferReceiptNote TransferReceiptNote { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
