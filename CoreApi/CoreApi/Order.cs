using System.Collections.Generic;

namespace CoreApi
{
    public class Order
    {
        public int id { get; set; }
        public List<Product> goods { get; set; }
        public Customer customer { get; set; }
        public OrderStatus status { get; set; }
        public double total { get; set; }
        public string GetString { get { return id + ": " + status.name; } }
    }

    public class OrdersCollection
    {
        public int count { get; set; }
        public List<Order> orders { get; set; }
    }
}
