using ApiCoreOrders.Helpers.Datos;
using ApiCoreOrders.Interfaces;
using ApiCoreOrders.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCoreOrders.Services
{
    public class OrderService : IOrder, IDisposable
    {
        #region Constructor y Variables
        SqlConexion sql = null;
        ConnectionType type = ConnectionType.NONE;

        OrderService() { }

        public static OrderService CrearInstanciaSQL(SqlConexion sql)
        {
            OrderService log = new OrderService
            {
                sql = sql,
                type = ConnectionType.MSSQL
            };

            return log;
        }

        #endregion
        public List<Order> GetOrders()
        {
            List<Order> list = new List<Order>();
            List<SqlParameter> _Parametros = new List<SqlParameter>();
            try
            {
                sql.PrepararProcedimiento("dbo.[SalesOrderHeader.GetAllJSON]", _Parametros);
                DataTableReader dtr = sql.EjecutarTableReader(CommandType.StoredProcedure);
                if (dtr.HasRows)
                {
                    while (dtr.Read())
                    {
                        var Json = dtr["Orders"].ToString();
                        if (Json != string.Empty)
                        {
                            JArray arr = JArray.Parse(Json);
                            foreach (JObject jsonOperaciones in arr.Children<JObject>())
                            {
                                list.Add(new Order()
                                {
                                    OrderID = Convert.ToInt32(jsonOperaciones["SalesOrderID"].ToString()),
                                    CustomerID = jsonOperaciones["CustomerID"].ToString(),
                                    EmployeeID = Convert.ToInt32(jsonOperaciones["EmployeeID"].ToString()),
                                    Freight = Convert.ToDouble(jsonOperaciones["Freight"].ToString()),
                                    ShipCity = jsonOperaciones["ShipCity"].ToString(),
                                    Verified = true,
                                    OrderDate = DateTime.Parse(jsonOperaciones["OrderDate"].ToString()),
                                    ShipName = jsonOperaciones["ShipName"].ToString(),
                                    ShipCountry = jsonOperaciones["ShipCountry"].ToString(),
                                    ShippedDate = DateTime.Parse(jsonOperaciones["ShipDate"].ToString()),
                                    ShipAddress = jsonOperaciones["ShipToAddress"].ToString(),
                                });

                            }

                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                throw new Exception(sqlEx.Message, sqlEx);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return list;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Para detectar llamadas redundantes

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (sql != null)
                    {
                        sql.Desconectar();
                        sql.Dispose();
                    }// TODO: elimine el estado administrado (objetos administrados).
                }

                // TODO: libere los recursos no administrados (objetos no administrados) y reemplace el siguiente finalizador.
                // TODO: configure los campos grandes en nulos.

                disposedValue = true;
            }
        }

        // TODO: reemplace un finalizador solo si el anterior Dispose(bool disposing) tiene código para liberar los recursos no administrados.
        // ~HidraService()
        // {
        //   // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
        //   Dispose(false);
        // }

        // Este código se agrega para implementar correctamente el patrón descartable.
        public void Dispose()
        {
            // No cambie este código. Coloque el código de limpieza en el anterior Dispose(colocación de bool).
            Dispose(true);
            // TODO: quite la marca de comentario de la siguiente línea si el finalizador se ha reemplazado antes.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
