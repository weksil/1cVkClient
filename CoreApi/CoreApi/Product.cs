using System.Collections.Generic;

namespace CoreApi
{
    public class Product
    {
        public int id { get; set; }
        public string title { get; set; }
        public double price { get; set; }
        public Photo photo { get; set; }
    }

    public class GoodsCollection
    {
        public int count { get; set; }
        public List<Product> goods { get; set; }
    }
}
