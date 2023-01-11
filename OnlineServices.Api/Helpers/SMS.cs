using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace OnlineServices.Api.Helpers
{
    public class SMS
    {
        public static void Send(string smsToNo, string peygiriCodeString)
        {

            try
            {
                //string domain = "https://www.saharsms.com/api/Jzo1LQ8T9dNXeNVxkAgoi0zeZ2EDmXn6/json/SendVerify?";
                //var data = "receptor=" + smsToNo + "&template=MedicineLogin-20670&token=" + peygiriCodeString;
                //string domain = "https://www.saharsms.com/api/7jOJamcDKhtW2HApLRWoqSBDa3Bwo6dL/json/SendVerify?";
                //var data = "receptor=" + smsToNo + "&template=activation-24772&token=" + peygiriCodeString;
                string domain = "https://www.saharsms.com/api/LsNUaz5GjtrB2RDk1L70eJS9N03sDa5A/json/SendVerify?";
                var data = "receptor=" + smsToNo + "&template=nikonikoo-30606&token=" + peygiriCodeString;

                string requestUrl = domain + data;

                HttpClient httpClient = new HttpClient();
                var request = httpClient.GetAsync(requestUrl).Result;
                var content = request.Content.ReadAsStringAsync().Result;
            }
            catch (System.Exception ex)
            {
            }
        }
    }
}

