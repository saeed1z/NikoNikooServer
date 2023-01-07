using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OnlineServices.Utilities
{
    public class SMS
    {
        public static void Send(string smsToNo, string peygiriCodeString)
        {

            try
            {
                //string domain = "https://www.saharsms.com/api/Jzo1LQ8T9dNXeNVxkAgoi0zeZ2EDmXn6/json/SendVerify?";
                //var data = "receptor=" + smsToNo + "&template=MedicineLogin-20670&token=" + peygiriCodeString;
                string domain = "https://www.saharsms.com/api/7jOJamcDKhtW2HApLRWoqSBDa3Bwo6dL/json/SendVerify?";
                var data = "receptor=" + smsToNo + "&template=activation-24772&token=" + peygiriCodeString;
                string requestUrl = domain + data;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
                request.Method = "Get";
                request.ContentType = "application/json";
                WebResponse webResponse = request.GetResponse();
                using (Stream webStream = webResponse.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string responseStream = responseReader.ReadToEnd();

                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
            }
        }
    }
}

