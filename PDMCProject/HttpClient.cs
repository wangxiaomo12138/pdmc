using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Windows.Forms;

namespace PDMCProject
{
    class HttpClient

    {
        public static JObject getList(string url, string value)
        {

            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            //string strContent = @"{ ""mmmm"": ""89e"",""nnnnnn"": ""0101943"",""kkkkkkk"": ""e8sodijf9""}";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(value);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            return jo;
        }

        public static JObject Login(string url, string value)
        {

            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(value);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            return jo;
        }


        public static JObject Post(string url, string value)
        {

            string serviceAddress = url;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceAddress);
            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(value);
                dataStream.Close();
            }
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            if (encoding == null || encoding.Length < 1)
            {
                encoding = "UTF-8"; //默认编码  
            }
            StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
            string retString = reader.ReadToEnd();
            //解析josn
            JObject jo = JObject.Parse(retString);
            return jo;
        }


        public static JObject HttpDownloadFile(string url, string path,string value,string fileName,int type)
        {
            // 设置参数
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            //发送请求并获取相应回应数据
            request.Method = "POST";
            request.ContentType = "application/json";
            using (StreamWriter dataStream = new StreamWriter(request.GetRequestStream()))
            {
                dataStream.Write(value);
                dataStream.Close();
            }

            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string isStream = response.Headers.Get("Content-Type");
            //判断返回类型是否为文件流
            if (isStream.Equals("application/octet-stream"))
            {
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流

                //获取当前文件夹路径
                string currPath = Application.StartupPath;
                string subPath = null;
                //检查是否存在文件夹
                if (0 == type)
                {
                    subPath = path;
                }
                else
                {
                    subPath = path + "/temp/" + GetTimeStamp();
                }
                if (false == System.IO.Directory.Exists(subPath))
                {
                    //创建pic文件夹
                    System.IO.Directory.CreateDirectory(subPath);
                }
                //确认创建文件夹是否成功，如果不成功，则直接在当前目录保

                Stream stream = new FileStream(subPath + "/" + fileName, FileMode.Create, FileAccess.ReadWrite);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    stream.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                stream.Close();
                responseStream.Close();
                JObject result = new JObject();
                result.Add("code", "pdmc_download_200");
                result.Add("data", subPath + "/" + fileName);
                return result;
            }
            else
            {
                //普通json格式接受
                string encoding = response.ContentEncoding;
                if (encoding == null || encoding.Length < 1)
                {
                    encoding = "UTF-8"; //默认编码  
                }
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding(encoding));
                string retString = reader.ReadToEnd();
                //解析josn
                JObject jo = JObject.Parse(retString);
                return jo;
            }

            
        }

        public static string get_uft8(string unicodeString)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            Byte[] encodedBytes = utf8.GetBytes(unicodeString);
            String decodedString = utf8.GetString(encodedBytes);
            return decodedString;
        }
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
