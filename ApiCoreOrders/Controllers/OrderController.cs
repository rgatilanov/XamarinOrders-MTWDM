using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreOrders.Helpers.Datos;
using ApiCoreOrders.Interfaces;
using ApiCoreOrders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ApiCoreOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
#if false
        [HttpGet]
        public ActionResult<List<Order>> GetOrders()
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Models.Order()
            {
                OrderID = 10007,
                OrderDate = DateTime.Now,
                ShippedDate = DateTime.Now,
                CustomerID = "RGAV",
                EmployeeID = 4,
                Freight = 6.6,
                ShipCity = "Madrid",
                Verified = true,
                ShipName = "Queen Cozinha",
                ShipCountry = "Brazil",
                ShipAddress = "Avda. Azteca 123"
            });
            orders.Add(new Models.Order()
            {
                OrderID = 10008,
                OrderDate = DateTime.Now,
                ShippedDate = DateTime.Now,
                CustomerID = "LDAV",
                EmployeeID = 5,
                Freight = 8.6,
                ShipCity = "CDMX",
                Verified = true,
                ShipName = "Reina Isabel",
                ShipCountry = "Mexico",
                ShipAddress = "Avda. Estadio Azteca 123"
            });

            return orders;
        }
#endif
        readonly IConfiguration _configuration;
        string ConnectionStringAzure;
        string ConnectionStringLocal;
        public OrderController(IConfiguration configuration)
        {
            _configuration = configuration;
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                ConnectionStringAzure = _configuration.GetConnectionString("ConnectionStringAzure");
                //ConnectionStringAzure = _configuration.GetValue<string>("ConnectionStringAzure");
            }
            else
                ConnectionStringLocal = _configuration.GetValue<string>("ConnectionStringLocal");
        }

        public ActionResult<List<Order>> GetOrders()
        {
#if true
            using (IOrder Login = Factorizador.CrearConexionServicio(ConnectionType.MSSQL, ConnectionStringLocal))
            {
                List<Order> objords = Login.GetOrders();
                return objords;
            }
#endif
            #region Este código es para sacar la ip del cliente que intenta conectarse a sql server en azure
#if false
            List<Order> orders = new List<Order>();
            SqlConexion sql = new SqlConexion()
            {
                _conn = new SqlConnection(ConnectionStringAzure)
            };

            try
            {
                sql._conn.Open();
            }
            catch (SqlException sqlEx)
            {
                orders.Add(new Order()
                {
                    ShipAddress = sqlEx.Message,
                });
            }
            catch (Exception ex)
            {
                orders.Add(new Order()
                {
                    ShipAddress = ex.Message,
                });
            }

            return orders;
#endif
            #endregion
        }
    }
}
