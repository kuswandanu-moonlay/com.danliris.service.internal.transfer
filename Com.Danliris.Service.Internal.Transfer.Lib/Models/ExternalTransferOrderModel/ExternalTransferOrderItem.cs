using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel
{
    public class ExternalTransferOrderItem : StandardEntity, IValidatableObject
    {
        public int ExternalTransferOrderId { get; set; }
        public int InternalTransferOrderId { get; set; }
        public string InternalTransferOrderNo { get; set; }
        public int TransferRequestId { get; set; }
        public string TransferRequestNo { get; set; }

        public virtual ExternalTransferOrder ExternalTransferOrder { get; set; }
        public virtual ICollection<ExternalTransferOrderDetail> ExternalTransferOrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
