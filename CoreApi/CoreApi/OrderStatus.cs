using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi
{
    public class OrderStatus
    {
        public int id { get; set; }
        public string name { get; set; }
        public override string ToString()
        {
            return name;
        }
    }
}
