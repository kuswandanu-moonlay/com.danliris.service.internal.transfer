using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.ExternalTransferOrderViewModels
{
    public class ExternalTransferOrderDetailViewModel : BasicViewModel
    {
        public int ETOItemId { get; set; }
        public int ITODetailId { get; set; }
        public int TRDetailId { get; set; }

        public ProductViewModel Product { get; set; }

        public double DefaultQuantity { get; set; }
        public UomViewModel DefaultUom { get; set; }

        public double DealQuantity { get; set; }
        public UomViewModel DealUom { get; set; }

        public double DOQuantity { get; set; }
        public double RemainingQuantity { get; set; }
        public double Convertion { get; set; }
        public double Price { get; set; }
        public string Grade { get; set; }
        public string ProductRemark { get; set; }
    }
}
