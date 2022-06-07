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
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        IOrderService _orderService = null;
        
        public OrderController([FromServices] IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            return _orderService.GetOrdersForCompany(id);
        }
    }
}
