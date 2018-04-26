using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferShippingOrderViewModels
{
    public class TransferShippingOrderViewModel : BasicViewModel
    {
        public string SONo { get; set; }
        public DateTime SODate { get; set; }
        public SupplierViewModel Supplier { get; set; }
        public string Remark { get; set; }
        public bool IsPosted { get; set; }

        public List<TransferShippingOrderItemViewModel> TransferShippingOrderItems { get; set; }

    }
}
