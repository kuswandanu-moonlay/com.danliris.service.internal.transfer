using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel
{
    public class TransferRequestViewModel
    {
        public string trNo { get; set; }
        public string trDate { get; set; }
        public string requestedArrivalDate { get; set; }
        public UnitViewModel unit { get; set; }
        public CategoryViewModel category { get; set; }
        public bool isPosted { get; set; }
        public bool isCanceled { get; set; }
        public string remark { get; set; }
        public List<TransferRequestDetailViewModel> details { get; set; }
    }
}
