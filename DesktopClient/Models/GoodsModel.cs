using CoreApi;
using System;
using System.ComponentModel;

namespace DesktopClient
{
    public class GoodsModel: INotifyPropertyChanged
    {
        public GoodsModel(Product currentProduct)
        {
            CurrentProduct = currentProduct;
        }

        public GoodsModel(Product currentProduct,AuthToken token)
        {
            CurrentProduct = currentProduct;
            ProductUri = Connector.GetPhotoUri(currentProduct.photo.link,token);
        }

        public Product CurrentProduct { get; set; }
        public Uri ProductUri { get; private set; }
        public int id { get { return CurrentProduct.id; }  }
        public string title { get { return CurrentProduct.title; } set { CurrentProduct.title = value; } }
        public double price { get { return CurrentProduct.price; } set { CurrentProduct.price = value; } }
        public int quantity { get { return CurrentProduct.quantity; } set { CurrentProduct.quantity = value; } }
        public double TotalPrice { get { return CurrentProduct.TotalPrice; } }
        public int stock { get { return CurrentProduct.stock; }  }
        public string vendor_code { get { return CurrentProduct.vendor_code; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Fire()
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(nameof(stock)));
        }
        

        public override string ToString()
        {
            return CurrentProduct.ToString();
        }
    }
}
