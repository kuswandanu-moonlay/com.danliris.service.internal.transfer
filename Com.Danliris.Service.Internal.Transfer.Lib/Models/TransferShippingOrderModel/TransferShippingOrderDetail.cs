using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel
{
    public class TransferShippingOrderDetail : StandardEntity, IValidatableObject
    {
        public int SOItemId { get; set; }
        public int DODetailId { get; set; }
        public int ETODetailId { get; set; }
        public int ITODetailId { get; set; }
        public int TRDetailId { get; set; }
        public string ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string UomId { get; set; }
        public string UomUnit { get; set; }
        public string Grade { get; set; }
        public double DOQuantity { get; set; }
        public double DeliveryQuantity { get; set; }
        public double ReceiptQuantity { get; set; }
        public double RemainingQuantity { get; set; }
        public string ProductRemark { get; set; }

        public virtual TransferShippingOrderItem TransferShippingOrderItem { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}

