using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels.TransferRequestViewModel
{
    public class TransferRequestViewModel : BasicViewModel
    {
        public string trNo { get; set; }
        public DateTime trDate { get; set; }
        public DateTime requestedArrivalDate { get; set; }
        public UnitViewModel unit { get; set; }
        public string unitId { get; set; }
        public string  unitCode{get;set;}
        public string unitName { get; set; }
        public string divisionId { get; set; }
        public string divisionCode { get; set; }
        public string divisionName { get; set; }
        public CategoryViewModel category { get; set; }
        public string categoryId { get; set; }
        public string categoryCode { get; set; }
        public string categoryName { get; set; }
        public bool isPosted { get; set; }
        public bool isCanceled { get; set; }
        public string remark { get; set; }
        public List<TransferRequestDetailViewModel> details { get; set; }
    }
}
