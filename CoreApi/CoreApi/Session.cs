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
        public GroupCustomers GetAllCustomers()
        {
            var req = new Request(Request.comGetCustomers, token);
            var answer = Connector.GetJsonAnswer(req);
            JsonConvert.DeserializeObject<Error>(answer).Init();
            return JsonConvert.DeserializeObject<GroupCustomers>(answer);
        }
        public Album GetAllPhotos()
        {
            var req = new Request(Request.comGetPhotos, token);
            var answer = Connector.GetJsonAnswer(req);
            JsonConvert.DeserializeObject<Error>(answer).Init();
            return JsonConvert.DeserializeObject<Album>(answer);
        }
        
    }
}