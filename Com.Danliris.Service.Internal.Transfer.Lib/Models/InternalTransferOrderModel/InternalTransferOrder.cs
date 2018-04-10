using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel
{
    public class InternalTransferOrder : StandardEntity, IValidatableObject
    {
     
        public string ITONo { get; set; }

        public int TRId { get; set; }

        public string TRNo { get; set; }

        public DateTime PRDate { get; set; }

        public DateTime RequestedArrivalDate { get; set; }

        public string UnitId { get; set; }

        public string UnitCode { get; set; }

        public string UnitName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string DivisonId { get; set; }

        public string DivisonCode { get; set; }

        public string DivisonName { get; set; }

        public string Remarks { get; set; }

        public bool IsPost { get; set; }

        public bool IsCanceled { get; set; }

        public int AutoIncrementNumber { get; set; }

        public virtual ICollection<InternalTransferOrderDetail> InternalTransferOrderDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
