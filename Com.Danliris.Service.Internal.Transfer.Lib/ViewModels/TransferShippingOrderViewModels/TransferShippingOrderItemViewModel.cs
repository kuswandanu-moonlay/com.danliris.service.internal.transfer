using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels
{
    public class TransferShippingOrderItemViewModel : BasicViewModel
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

        public List<TransferShippingOrderDetailViewModel> TransferShippingOrderDetails { get; set; }
    }
}

