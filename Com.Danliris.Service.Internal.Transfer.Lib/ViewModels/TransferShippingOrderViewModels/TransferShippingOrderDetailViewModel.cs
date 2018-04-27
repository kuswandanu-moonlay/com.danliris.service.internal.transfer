using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels
{
    public class TransferShippingOrderDetailViewModel : BasicViewModel
    {
        public int SOItemId { get; set; }
        public int DODetailId { get; set; }
        public int ETODetailId { get; set; }
        public int ITODetailId { get; set; }
        public int TRDetailId { get; set; }
        public ProductViewModel Product { get; set; }
        public UomViewModel Uom { get; set; }
        public string Grade { get; set; }
        public double DOQuantity { get; set; }
        public double DeliveryQuantity { get; set; }
        public double ReceiptQuantity { get; set; }
        public double RemainingQuantity { get; set; }
        public string ProductRemark { get; set; }
    }
}
