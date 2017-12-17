using System;

namespace CoreApi
{
    public class Customer
    {
        public int id { get; set; }
        public int id_vk { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string birth_day { get; set; }
        public CustomerSex sex { get; set; }
        public string photo { get; set; }
        public DateTime Birth_day { get
            {
                if (bday.Year != 1) return bday;
                var t = new string[] { "1","1","1"};
                var p = birth_day.Split('.');
                switch (p.Length)
                {
                    case 2:
                        t[0] = p[0];
                        t[1] = p[1];
                        break;
                    case 3:
                        t[0] = p[0];
                        t[1] = p[1];
                        t[2] = p[2];
                        break;
                    default:
                        break;
                }
                bday = new DateTime(int.Parse(t[2]), int.Parse(t[1]), int.Parse(t[0]));
                return bday;
            } }
        private DateTime bday = new DateTime(1,1,1);
    }

    public enum CustomerSex
    {
        NaN = 0,
        Female,
        Male
    }
    public class GroupCustomers
    {
        public int count { get; set; }
        public Customer[] customers { get; set; }
    }
}
