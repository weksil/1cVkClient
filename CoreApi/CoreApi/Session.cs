using Newtonsoft.Json;

namespace CoreApi
{
    public class Session
    {
        private AuthToken token;

        public Session(string login, string password)
        {
            var answer = Connector.GetJsonAnswer(Request.AuthRequest(login, password));
            JsonConvert.DeserializeObject<Error>(answer).Init();
            token = JsonConvert.DeserializeObject<AuthToken>(answer);
        }
        public T GetAllItems<T>(string comm)
        {
            var req = new Request(comm, token);
            return Parse<T>(req);
        }

        private static T Parse<T>(Request source)
        {
            var answer = Connector.GetJsonAnswer(source);
            JsonConvert.DeserializeObject<Error>(answer).Init();
            return JsonConvert.DeserializeObject<T>(answer);
        }
        
    }
}