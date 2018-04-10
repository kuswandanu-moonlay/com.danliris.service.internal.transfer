using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel
{
    public class TransferRequestDetailViewModel : BasicViewModel
    {
        public ProductViewModel product { get; set; }
        public double quantity { get; set; }
        public UomViewModel uom { get; set; }
        public string productRemark { get; set; }
    }
}
