using System.Collections.Generic;
using System.Text;

namespace CoreApi
{
    public class Request
    {
        private const string comAuth = "auth";
        public const string comGetCustomers = "getCustomer";

        private Dictionary<string, string> parametrs;
        private string comand;
        private AuthToken token;

        public Request(string comand, AuthToken token = null)
        {
            this.comand = comand;
            parametrs = new Dictionary<string, string>();
            this.token = token;
        }

        public void SetParametr(string parametr, string value)
        {
            if (parametrs.ContainsKey(parametr))
            {
                parametrs[parametr] = value;
            }
            else
            {
                parametrs.Add(parametr, value);
            }
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
                sb.Append("token=" + token.token);
            return sb.ToString();
        }

        public static Request AuthRequest(string login, string password)
        {
            var request = new Request(comAuth);
            request.SetParametr("login", login);
            request.SetParametr("password", password);
            return request;
        }
    }
}