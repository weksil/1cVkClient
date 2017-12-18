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

    }
    public class Goods_id
    {
        public long good_id { get; set; }
    }


}