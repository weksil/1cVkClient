using System.Collections.Generic;
using System.Text;

namespace CoreApi
{
    public class Request
    {
        private const string comAuth = "auth";
        public const string comGetCustomers = "getCustomer";
        public const string comGetPhotos = "getPhotos";
        public const string comGetGoods = "getGoods";
        public const string comCreateGoods = "cGood"; 
        public const string comGetOrders = "getOrders";
        public const string comRemoveGoods = "deleteGood";
        public const string comCreateOrder = "cOrder";

        private Dictionary<string, string> parametrs;
        private string comand;
        private string content;
        private AuthToken token;

        public Request(string comand, AuthToken token = null)
        {
            this.comand = comand;
            parametrs = new Dictionary<string, string>();
            this.token = token;
        }

        public void SetParametr(string parametr, object value)
        {
            if (parametrs.ContainsKey(parametr))
            {
                parametrs[parametr] = value.ToString();
            }
            else
            {
                parametrs.Add(parametr, value.ToString());
            }
        }
        public void SetContent(string value)
        {
            content = value;
        }

        public string GetContent()
        {
            return content ;
        }

        public string GetString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(comand + "?");
            foreach (var item in parametrs)
            {
                sb.Append(item.Key + "=" + item.Value + "&");
            }
            if (!(token is null))
                sb.Append(token.ToParametr());
            return sb.ToString();
        }

        public static Request AuthRequest(string login, string password)
        {
            var request = new Request(comAuth);
            request.SetParametr("login", login);
            request.SetParametr("password", password);
            var t = request.GetString();
            return request;
        }


    }
}