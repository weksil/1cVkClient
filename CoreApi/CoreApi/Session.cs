using System.Runtime.Serialization.Json;
using System;

namespace CoreApi
{
    public class Session
    {
        public AuthToken token;

        public Session(string login, string password)
        {
            var answer = Connector.GetStreamAnswer(Request.AuthRequest(login, password));
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(AuthToken));
            token = jsonFormatter.ReadObject(answer) as AuthToken;
        }
        public GroupCustomers GetAllCustomers()
        {
            var req = new Request(Request.comGetCustomers, new AuthToken() { token = "9f7fcd8b757948de6fa89a35825c59ec" });
            //var req = new Request(Request.comGetCustomers, token);
            var answer = Connector.GetStreamAnswer(req);
            DataContractJsonSerializer jsonFormatter;
            jsonFormatter = new DataContractJsonSerializer(typeof(Error));
            var error = jsonFormatter.ReadObject(answer) as Error;
            error?.Init();
            jsonFormatter = new DataContractJsonSerializer(typeof(GroupCustomers));
            return jsonFormatter.ReadObject(answer) as GroupCustomers;
        }
    }
}