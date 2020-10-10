using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreOrders.Interfaces
{
    public interface IOrder : IDisposable
    {
        List<Models.Order> GetOrders();
    }
}
