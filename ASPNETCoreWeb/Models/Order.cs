using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetCoreWeb.Models
{
    using System.Security.AccessControl;

    public class Order
    {
        public int OrderId { get; set; }

        public string CompanyName { get; set; }

        public string Description { get; set; }

        public decimal OrderTotal { get; set; }

        public List<OrderProduct> OrderProducts { get; set; }

    }


    public class OrderProduct
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    
        public int Quantity { get; set; }

        public decimal Price { get; set; }

    }

    public class Product
    {
        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}