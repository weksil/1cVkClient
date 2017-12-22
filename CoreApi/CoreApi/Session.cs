using Newtonsoft.Json;

namespace CoreApi
{
    public class Session
    {
        public AuthToken token;

        public Session(string login, string password)
        {
            var answer = Connector.GetJsonAnswer(Request.AuthRequest(login, password));
            JsonConvert.DeserializeObject<Error>(answer).Init();
            token = JsonConvert.DeserializeObject<AuthToken>(answer);
        }

        public T GetAllItems<T>(string comm)
        {
            var req = new Request(comm, token);
            return Parse<T>(Connector.GetJsonAnswer( req));
        }

        private static T Parse<T>(string source)
        {
            JsonConvert.DeserializeObject<Error>(source).Init();
            return JsonConvert.DeserializeObject<T>(source);
        }
        public void CreateGoods(string title, double price, int photo)
        {
            if (title.Length == 0) throw new ParamsExeption();
            if (price <= 0) throw new ParamsExeption();
            var req = new Request(Request.comCreateGoods, token);
            req.SetParametr("title", title);
            req.SetParametr("price", price.ToString().Replace(",", "."));
            req.SetParametr("photo", photo);
            var answ = Connector.GetJsonAnswer(req);
            Parse<Goods_id>(answ);
        }
        public void RemoveGoods( int id)
        {
            var req = new Request(Request.comRemoveGoods, token);
            req.SetParametr("id", id);
            var answ = Connector.GetJsonAnswer(req);
            Parse<Goods_id>(answ);
        }
        public void CreateOrder(SendOrder order)
        {
            var req = new Request(Request.comCreateOrder, token);
            var json = JsonConvert.SerializeObject(order);
            req.SetContent(json);
            var res = Connector.PostJsonAnswer(req);
            Parse<Error>(res);
        }
        public void UpdateOrder(Order order, Status status)
        {
            if (order.status.id == status.id) return;
            order.status = status;
            var req = new Request(Request.comUpdateOrder, token);
            req.SetParametr("order_id",order.id);
            req.SetParametr("status_id", status.id);
            Parse<Error>(Connector.GetJsonAnswer(req));
        }
        public void AddStokGoods(Product product, int addStock)
        {
            if (addStock < 1) return;
            product.stock += addStock;
            var req = new Request(Request.comUpdateGoods, token);
            req.SetParametr("good_id", product.id);
            req.SetParametr("stock", product.stock);
            Parse<Error>(Connector.GetJsonAnswer(req));
        }
    }
    public class Goods_id
    {
        public long good_id { get; set; }
    }


}