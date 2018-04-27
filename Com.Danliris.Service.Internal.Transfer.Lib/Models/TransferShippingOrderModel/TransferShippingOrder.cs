using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferShippingOrderModel
{
    public class TransferShippingOrder : StandardEntity, IValidatableObject
    {
        public string SONo { get; set; }
        public DateTime SODate { get; set; }
        public string SupplierId { get; set; }
        public string SupplierCode { get; set; }
        public string SupplierName { get; set; }
        public string Remark { get; set; }
        public bool IsPosted { get; set; }
        public virtual ICollection<TransferShippingOrderItem> TransferShippingOrderItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
