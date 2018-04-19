using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels
{
    public class ExternalTransferOrderItemViewModel : BasicViewModel
    {
        public int ExternalTransferOrderId { get; set; }
        public int InternalTransferOrderId { get; set; }
        public string InternalTransferOrderNo { get; set; }
        public int TransferRequestId { get; set; }
        public string TransferRequestNo { get; set; }

        public List<ExternalTransferOrderDetailViewModel> ExternalTransferOrderDetails { get; set; }
    }
}
