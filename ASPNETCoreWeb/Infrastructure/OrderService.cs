using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASPNetCoreWeb.Infrastructure
{
    using System.Data;
    using Models;

    public class OrderService
    {
        //public List<Order> GetOrdersForCompany(int CompanyId)
        //{

        //    var database = new Database();

        //    // Get the orders
        //    var sql1 =
        //        "SELECT c.name, o.description, o.order_id FROM company c INNER JOIN [order] o on c.company_id=o.company_id";

        //    var reader1 = database.ExecuteReader(sql1);

        //    var values = new List<Order>();

        //    while (reader1.Read())
        //    {
        //        var record1 = (IDataRecord) reader1;

        //        values.Add(new Order()
        //        {
        //            CompanyName = record1.GetString(0),
        //            Description = record1.GetString(1),
        //            OrderId = record1.GetInt32(2),
        //            OrderProducts = new List<OrderProduct>()
        //        });

        //    }

        //    reader1.Close();

        //    //Get the order products
        //    var sql2 =
        //        "SELECT op.price, op.order_id, op.product_id, op.quantity, p.name, p.price FROM orderproduct op INNER JOIN product p on op.product_id=p.product_id";

        //    var reader2 = database.ExecuteReader(sql2);

        //    var values2 = new List<OrderProduct>();

        //    while (reader2.Read())
        //    {
        //        var record2 = (IDataRecord)reader2;

        //        values2.Add(new OrderProduct()
        //        {
        //            OrderId = record2.GetInt32(1),
        //            ProductId = record2.GetInt32(2),
        //            Price = record2.GetDecimal(0),
        //            Quantity = record2.GetInt32(3),
        //            Product = new Product()
        //            {
        //                Name = record2.GetString(4),
        //                Price = record2.GetDecimal(5)
        //            }
        //        });
        //     }

        //    reader2.Close();

        //    foreach (var order in values)
        //    {
        //        foreach (var orderproduct in values2)
        //        {
        //            if (orderproduct.OrderId != order.OrderId)
        //                continue;

        //            order.OrderProducts.Add(orderproduct);
        //            order.OrderTotal = order.OrderTotal + (orderproduct.Price * orderproduct.Quantity);
        //        }
        //    }

        //    return values;
        //}

        private class QueryRow
        {
            public int OrderId { get; set; }
            public string OrderDescription { get; set; }
            public int CompanyId { get; set; }
            public string CompanyName { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public Decimal ProductPrice { get; set; }
            public Decimal OrderItemPrice { get; set; }
            public int OrderItemQuantity { get; set; }
        }

        public List<Order> GetOrdersForCompany(int requestedCompanyId)
        {
            Database databaseHelper = new Database();
            List<Order> orders = new List<Order>();

            var orderSQL =
                $@"SELECT o.order_id, o.description, c.company_id, c.name company_name, p.product_id, p.name product_name, p.price product_price, op.price order_item_price, op.quantity order_item_quantity
                FROM[dbo].[Order] o
                INNER JOIN[dbo].[orderproduct] op ON O.order_id = op.order_id
                INNER JOIN[dbo].[product] p ON op.product_id = p.product_id
                INNER JOIN[dbo].[company] c ON o.company_id = c.company_id
                WHERE o.company_id = {requestedCompanyId}
                ORDER BY company_id, order_id, product_id";

            var reader = databaseHelper.ExecuteReader(orderSQL);

            List<QueryRow> queryRows = new List<QueryRow>();
            while (reader.Read())
            {
                var newQueryRow = new QueryRow()
                {
                    OrderId = reader.GetInt32(reader.GetOrdinal("order_id")),
                    OrderDescription = reader.GetString(reader.GetOrdinal("description")),
                    CompanyId = reader.GetInt32(reader.GetOrdinal("company_id")),
                    CompanyName = reader.GetString(reader.GetOrdinal("company_name")),
                    ProductId = reader.GetInt32(reader.GetOrdinal("product_id")),
                    ProductName = reader.GetString(reader.GetOrdinal("product_name")),
                    ProductPrice = reader.GetDecimal(reader.GetOrdinal("product_price")),
                    OrderItemPrice = reader.GetDecimal(reader.GetOrdinal("order_item_price")),
                    OrderItemQuantity = reader.GetInt32(reader.GetOrdinal("order_item_quantity"))
                };

                queryRows.Add(newQueryRow);
            }

            var newOrders =
                queryRows
                .GroupBy(qr => (qr.CompanyId, qr.OrderId))
                .Select(qrg => new Order()
                {
                    OrderId = qrg.Key.Item2,
                    CompanyName = qrg.First().CompanyName,
                    Description = qrg.First().OrderDescription,
                    OrderTotal = qrg.Sum(qr => qr.OrderItemPrice * qr.OrderItemQuantity),
                    OrderProducts = qrg.Select(qr => new OrderProduct()
                    {
                        OrderId = qr.OrderId,
                        ProductId = qr.ProductId,
                        Product = new Product()
                        {
                            Name = qr.ProductName,
                            Price = qr.ProductPrice
                        },
                        Quantity = qr.OrderItemQuantity,
                        Price = qr.OrderItemPrice
                    }).ToList()
                }).ToList();

            return newOrders;
        }
    }
}