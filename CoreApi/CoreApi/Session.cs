using System.Runtime.Serialization.Json;

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
            var req = new Request(Request.comGetCustomers, token);
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(GroupCustomers));
            var answer = Connector.GetStreamAnswer(req);
            GroupCustomers res = jsonFormatter.ReadObject(answer) as GroupCustomers;
            return res;
        }
    }
}