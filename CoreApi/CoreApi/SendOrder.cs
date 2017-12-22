using System;
using System.Collections.Generic;
using System.Text;

namespace CoreApi
{
    public class Good
    {
        public int id { get; set; }
        public int quantity { get; set; }
    }

    public class SendOrder
    {
        public List<Good> goods { get; set; }
        public int customer { get; set; }
    }
}
