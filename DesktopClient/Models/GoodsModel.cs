using CoreApi;
using System;

namespace DesktopClient
{
    class GoodsModel
    {
        public GoodsModel(Product currentProduct,AuthToken token)
        {
            CurrentProduct = currentProduct;
            ProductUri = Connector.GetPhotoUri(currentProduct.photo.link,token);
        }

        public Product CurrentProduct { get; set; }
        public Uri ProductUri { get; private set; }
        public int id { get { return CurrentProduct.id; }  }
        public string title { get { return CurrentProduct.title; } }
        public double price { get { return CurrentProduct.price; } }
    }
}
