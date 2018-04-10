using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using Com.Danliris.Service.Internal.Transfer.Lib.Models.InternalTransferOrderModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.InternalTransferOrderViewModels
{
    public class InternalTransferOrderViewModel : BasicViewModel, IValidatableObject
    {
        public string InternalTransferOrderNo { get; set; }

        public string TransferRequestNo { get; set; }

        public string UnitId { get; set; }

        public List<InternalTransferOrderDetailViewModel> InternalTransferOrderDetails { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
