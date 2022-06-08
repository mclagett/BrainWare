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
        private readonly IOrderService _orderService = null;
        private readonly ILogger<OrderController> _logger;

        public OrderController([FromServices] IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public IEnumerable<Order> GetOrders(int id = 1)
        {
            var orders = _orderService.GetOrdersForCompany(id);
            return orders;
        }
    }
}
