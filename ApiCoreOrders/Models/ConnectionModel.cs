using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreOrders.Models
{
    public enum ConnectionType : int
    {
        NONE = 0,
        MSSQL = 1,
        MYSQL = 2,
    }
}
