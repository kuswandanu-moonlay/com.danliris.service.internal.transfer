using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferDeliveryOrderViewModel
{
    public class TransferDeliveryOrderItemViewModel : BasicViewModel, IValidatableObject
    {
        public int DOId { get; set; }
        public string ETOId { get; set; }
        public string ETONo { get; set; }
        public string TRId { get; set; }
        public string TRNo { get; set; }
        public string ITOId { get; set; }
        public string ITONo { get; set; }

        public List<TransferDeliveryOrderDetailViewModel> details { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return new List<ValidationResult>();
        }
    }
}