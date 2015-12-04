using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Anatoli.DataAccess.Interfaces;
using Anatoli.Business.Proxy.Interfaces;

namespace Anatoli.Business
{
    public interface IBusinessDomain<TSource, TOut>
        where TSource : class, new()
        where TOut : class, new()
    {
        IAnatoliProxy<TSource, TOut> Proxy { get; set; }
        IProductRepository ProductRepository { get; set; }
        Guid PrivateLabelOwnerId { get; }

        Task<List<TOut>> GetAll(Guid storeId);
        Task<List<TOut>> GetAllChangedAfter(Guid storeId, DateTime selectedDate);
        Task Publish(List<TOut> productEntities);
        Task Delete(List<TOut> productEntities);
    }
}
