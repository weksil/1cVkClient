namespace CoreApi
{
    public class AuthToken
    {
        public string token { get; set; }
        public string ToParametr() { return "token=" + token; }
    }
}