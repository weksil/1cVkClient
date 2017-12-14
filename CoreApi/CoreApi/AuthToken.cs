using System.Runtime.Serialization;

namespace CoreApi
{
    [DataContract]
    public class AuthToken
    {
        [DataMember]
        public string token { get; set; }
    }
}