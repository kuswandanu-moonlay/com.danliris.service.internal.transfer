using Com.Danliris.Service.Internal.Transfer.Lib.Helpers;

namespace Com.Danliris.Service.Internal.Transfer.Lib.ViewModels
{
    public class CurrencyViewModel : BasicViewModel
    {
        public string _id { get; set; }
        public string code { get; set; }
        public string symbol { get; set; }
        public string rate { get; set; }
        public string description { get; set; }
    }
}