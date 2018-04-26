using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.ExternalTransferOrderModel
{
    public class ExternalTransferOrderItem : StandardEntity, IValidatableObject
    {
        public int ETOId { get; set; }
        public int ITOId { get; set; }
        public string ITONo { get; set; }
        public int TRId { get; set; }
        public string TRNo { get; set; }
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public virtual ExternalTransferOrder ExternalTransferOrder { get; set; }
        public virtual ICollection<ExternalTransferOrderDetail> ExternalTransferOrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}