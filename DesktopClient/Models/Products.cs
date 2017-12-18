using CoreApi;
using System.Collections.Generic;

namespace DesktopClient
{
    class Products
    {
        public List<GoodsModel> Goods { get; private set; }
        public Products(GoodsCollection goods,AuthToken token)
        {
            Goods = new List<GoodsModel>();
            if (goods.goods is null) return;
            foreach (var item in goods.goods)
            {
                Goods.Add(new GoodsModel(item, token));
            }
        }
    }

}
