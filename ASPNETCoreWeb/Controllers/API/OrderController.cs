using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCoreWeb.Controllers
{
    using Infrastructure;
    using Models;

    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService = null;
        
        public OrderController([FromServices] IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            var orders = _orderService.GetOrdersForCompany(id);
            return orders;
        }
    }
}
