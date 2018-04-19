using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel
{
    public class ExternalTransferOrderDetail : StandardEntity, IValidatableObject
    {
        public int ExternalTransferOrderItemId { get; set; }
        public int InternalTransferOrderDetailId { get; set; }
        public int TransferRequestDetailId { get; set; }

        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }

        public double DefaultQuantity { get; set; }
        public string DefaultUomId { get; set; }
        public string DefaultUomUnit { get; set; }

        public double DealQuantity { get; set; }
        public string DealUomId { get; set; }
        public string DealUomUnit { get; set; }

        public double ReceivedQuantity { get; set; }
        public double RemainingQuantity { get; set; }
        public double Convertion { get; set; }
        public double Price { get; set; }
        public string Grade { get; set; }
        public string ProductRemark { get; set; }

        public virtual ExternalTransferOrderItem ExternalTransferOrderItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
