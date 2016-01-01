using System;
using System.Linq;
using System.Collections.Generic;

namespace Anatoli.PMC.Business.Proxy.Interfaces
{
    public interface IAnatoliProxy<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        TOut Convert(TSource data);
        TSource ReverseConvert(TOut data);
        TSource ReverseConvert(string id, Guid PrivateOwnerId);

        List<TOut> Convert(List<TSource> data);
        List<TSource> ReverseConvert(List<TOut> data);
    }
}