using CoreApi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DesktopClient 
{
    public class OrderModel : INotifyPropertyChanged
    {
        public List<Product> goods { get; set; }
        public double total { get
            {
                double res = 0;
                foreach (var item in goods)
                {
                    res += item.TotalPrice;
                }
                return res;
            } }

        public OrderModel()
        {
            goods = new List<Product>();
        }

        public void Fire()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(total)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
