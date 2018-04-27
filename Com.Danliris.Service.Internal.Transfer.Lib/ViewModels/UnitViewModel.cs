using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels
{
    public class UnitViewModel : BasicViewModel
    {
        public string _id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string divisionId { get; set; }
        public string divisionCode { get; set; }
        public string divisionName { get; set; }
        public DivisionViewModel division { get; set; }
    }
}
