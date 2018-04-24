using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels
{
    public class ExternalTransferOrderItemViewModel : BasicViewModel
    {
        public int ETOId { get; set; }
        public int ITOId { get; set; }
        public string ITONo { get; set; }
        public int TRId { get; set; }
        public string TRNo { get; set; }
        public UnitViewModel Unit { get; set; }

        public List<ExternalTransferOrderDetailViewModel> ExternalTransferOrderDetails { get; set; }
    }
}
