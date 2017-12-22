using System.Collections.Generic;

namespace CoreApi
{
    public class Status
    {
        public int id { get; set; }
        public string name { get; set; }
        public override string ToString()
        {
            return name;
        }
    }

    public class StatusCollection
    {
        public int count { get; set; }
        public List<Status> statuses { get; set; }
    }
}
