using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels
{
    public class InternalTransferOrderViewModel : BasicViewModel//, IValidatableObject
    {
        public string ITONo { get; set; }

        public int TRId { get; set; }

        public string TRNo { get; set; }

        public DateTime TRDate { get; set; }

        public DateTime RequestedArrivalDate { get; set; }

        public string UnitId { get; set; }

        public string UnitCode { get; set; }

        public string UnitName { get; set; }

        public string CategoryId { get; set; }

        public string CategoryCode { get; set; }

        public string CategoryName { get; set; }

        public string DivisionId { get; set; }

        public string DivisionCode { get; set; }

        public string DivisionName { get; set; }

        public string Remarks { get; set; }

        public bool IsPost { get; set; }
        
        public List<InternalTransferOrderDetailViewModel> InternalTransferOrderDetails { get; set; }
        ///public List<sourceTransferOrderViewModel> sourceTransferOrder { get; set; }
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
