using System;
using System.IO;
using System.Net;

namespace CoreApi
{
    public class Connector
    {
        private const string Url = "http://mereng.xyz/api/";
        private const string UrlPictures = "http://mereng.xyz/";

        public static string GetJsonAnswer(Request request)
        {
            Stream stream = GetStreamAnswer(request);
            string Out;
            using (StreamReader sr = new StreamReader(stream))
            {
                Out = sr.ReadToEnd();
                sr.Close();
            }
            return Out;
        }

        public static Stream GetStreamAnswer(Request request)
        {
            WebRequest req = WebRequest.Create(Url + request.GetString());
            WebResponse resp = req.GetResponse();
            return resp.GetResponseStream();
        }
        
        public static Uri GetPhotoUri(string path, AuthToken token)
        {
            return new Uri(UrlPictures + path + "?" + token.ToParametr());
        }
    }
}