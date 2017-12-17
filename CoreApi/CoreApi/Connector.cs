using System;
using System.IO;
using System.Net;

namespace CoreApi
{
    public class Connector
    {
        private const string Url = "http://82.146.48.26:8080/api/";
        private const string UrlPictures = "http://82.146.48.26:8080/";

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
        public static byte[] GetBytesAnswer(string path, AuthToken token)
        {
            var t = UrlPictures + path +"?"+ token.ToParametr();
            WebRequest req = WebRequest.Create(t);
            WebResponse resp = req.GetResponse();
            var str = resp.GetResponseStream();
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = str.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                buffer = ms.ToArray();
            }
            if (buffer.Length == 11) throw new TokenExeption();
            return buffer;
        }
        public static Uri GetPhotoUri(string path, AuthToken token)
        {
            return new Uri(UrlPictures + path + "?" + token.ToParametr());
        }
    }
}