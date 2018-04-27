using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel
{
    public class TransferShippingOrderItem : StandardEntity, IValidatableObject
    {
        public int SOId { get; set; }
        public int DOId { get; set; }
        public string DONo { get; set; }
        public int ETOId { get; set; }
        public string ETONo { get; set; }
        public int ITOId { get; set; }
        public string ITONo { get; set; }
        public int TRId { get; set; }
        public string TRNo { get; set; }

        public virtual TransferShippingOrder TransferShippingOrder { get; set; }
        public virtual ICollection<TransferShippingOrderDetail> TransferShippingOrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}

