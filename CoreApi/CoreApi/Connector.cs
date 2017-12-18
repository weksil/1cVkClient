using System;
using System.IO;
using System.Net;
using System.Text;

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
            req.Method = "GET";
            WebResponse resp = req.GetResponse();
            return resp.GetResponseStream();
        }
        
        public static Uri GetPhotoUri(string path, AuthToken token)
        {
            return new Uri(UrlPictures + path + "?" + token.ToParametr());
        }

        private static string PostJsonAnswer(Request request)
        {
            WebRequest req = WebRequest.Create(Url + request.GetString());
            req.Method = "POST";
            req.Timeout = 1000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.UTF8.GetBytes(request.GetContent());
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream stream = res.GetResponseStream();
            string Out;
            using (StreamReader sr = new StreamReader(stream))
            {
                Out = sr.ReadToEnd();
                sr.Close();
            }
            return Out;
        }
    }
}