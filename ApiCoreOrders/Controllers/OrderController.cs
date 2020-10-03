using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCoreOrders.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiCoreOrders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
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
    }
}
