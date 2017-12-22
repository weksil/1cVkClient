using CoreApi;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

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

        public void Add(Product item, int quantity)
        {
            item.quantity += quantity;
            if (quantity == 0) return;
            if (goods.Contains(item))
            {
                if (item.quantity == 0) goods.Remove(item);
                return;
            }
            goods.Add(item);
        }

        public void Fire()
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(total)));
        }
        public SendOrder CreateJson(Customer customer)
        {
            var res = new SendOrder();
            res.goods = new List<Good>();
            foreach (var item in goods)
            {
                res.goods.Add(new Good() { id = item.id, quantity = item.quantity });
            }
            res.customer = customer.id;
            return res;
        }
        public void Clear()
        {
            foreach (var item in goods)
            {
                item.quantity = 0;
            }
            goods.Clear();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
