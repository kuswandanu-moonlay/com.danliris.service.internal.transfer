using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferDeliveryOrderViewModel
{
    public class TransferDeliveryOrderItemViewModel : BasicViewModel
    {
        public int DOId { get; set; }
        public int ETOId { get; set; }
        public string ETONo { get; set; }
        public int TRId { get; set; }
        public string TRNo { get; set; }
        public int ITOId { get; set; }
        public string ITONo { get; set; }
        public string UnitId { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }

        public List<TransferDeliveryOrderDetailViewModel> details { get; set; }
 
    }
}