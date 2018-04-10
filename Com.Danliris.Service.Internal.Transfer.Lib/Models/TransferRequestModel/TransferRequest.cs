using Com.Moonlay.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Models.TransferRequestModel
{
    public class TransferRequest : StandardEntity, IValidatableObject
    {
        public string TRNo { get; set; }
        public DateTime TRDate { get; set; }
        public DateTime RequestedArrivalDate { get; set; }
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string DivisionId { get; set; }
        public string DivisionCode { get; set; }
        public string DivisionName { get; set; }
        public string CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public bool IsPosted { get; set; }
        public bool IsCanceled { get; set; }
        public string Remark { get; set; }
        public int AutoIncrementNumber { get; set; }
        public ICollection<TransferRequestDetail> TransferRequestDetails { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}
