using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels
{
    public class CategoryViewModel : BasicViewModel
    {
        public string _id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
    }
}
