using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreOrders.Interfaces
{
    using Helpers.Datos;
    using Services;
    public static class Factorizador
    {
        public static IOrder CrearConexionServicio(Models.ConnectionType type, string connectionString)
        {
            IOrder nuevoMotor = null; ;
            switch (type)
            {
                case Models.ConnectionType.NONE:
                    break;
                case Models.ConnectionType.MSSQL:
                    SqlConexion sql = SqlConexion.Conectar(connectionString);
                    nuevoMotor = OrderService.CrearInstanciaSQL(sql);
                    break;
                case Models.ConnectionType.MYSQL:
                    break;
                default:
                    break;
            }

            return nuevoMotor;
        }
    }
}
