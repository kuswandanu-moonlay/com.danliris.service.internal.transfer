using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Danliris.Service.Internal.Transfer.Lib.Interfaces
{
    public interface IReadable<TModel>
    {
        /* List of Data, Total Data, Order Dictionary */
        Tuple<List<TModel>, int, Dictionary<string, string>> Read(int Page = 1, int Size = 25, string Order = "{}", string Keyword = null, string Filter = "{}");
    }
}
