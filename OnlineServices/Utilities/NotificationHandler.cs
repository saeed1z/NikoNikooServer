using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace OnlineServices.Utilities
{
    public static class NotificationHandler
    {
        private static readonly string Baseurl = "https://fcm.googleapis.com/fcm/send";
        private static readonly string UserApikey = "key=AAAAZJ4d5ok:APA91bHhZrtYYUO_7oylqmjwXYt13O1Ed7vMgfSeNsv_0K-lPy0BDxTUSBstYhH4rKvJtZ-_iwOsz78nDYt3tP50pt5y9NUJ3eKieQumUOxrPT1dMt8Hl4PVwUeS90VYYFx1Q4GmQiwn";
        private static readonly string EmployeeApikey = "key=AAAA47APuh0:APA91bE4_DPRaGQ1yyrWKy4bX6oBPqyPd8vNJGYe_yCQfpUma1n0GHiPlGSS-UEWZF0H83Y1cmB9UxETCKrpwFtUyNABKp0Sh_GR8e2Te906AnoXWBvB7E8lG01DKcRqMpqc7C3Tplq5";
        //public static void SendToAndroidClients(string message, string type = "0", string id = "0",bool ENLang=false,string body="")
        //{
        //    var dictData = new Dictionary<string, string>()
        //    {
        //        {"TypeId", type},
        //        { "Id",id}
        //    };
        //    var model = new FireBaseNotifPostModel()
        //    {
        //        registration_ids = GetAllUsers().ToList(),
        //        data = dictData,
        //        notification = new NotifBodyPostModel()
        //        {
        //            title = message,
        //            body = body.Length>200? body.Substring(0, 200) + "...": body
        //        },
        //        priority = 10,
        //        badge = "1"
        //    };
        //    var request = WebRequest.Create(Baseurl) as HttpWebRequest;

        //    request.KeepAlive = true;
        //    request.Method = "POST";
        //    request.ContentType = "application/json; charset=utf-8";

        //    request.Headers.Add("Authorization", EmployeeApikey);

        //    var strModel = JsonConvert.SerializeObject(model);
        //    var byteArray = Encoding.UTF8.GetBytes(strModel);

        //    string responseContent = null;

        //    try
        //    {
        //        using (var writer = request.GetRequestStream())
        //        {
        //            writer.Write(byteArray, 0, byteArray.Length);
        //        }

        //        using (var response = request.GetResponse() as HttpWebResponse)
        //        {
        //            using (var reader = new StreamReader(response.GetResponseStream()))
        //            {
        //                responseContent = reader.ReadToEnd();
        //            }
        //        }
        //        return;
        //    }
        //    catch (WebException ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine(ex.Message);
        //        System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
        //        return;
        //    }
        //}
        public static void SendToUserClient(string title, string body, bool isEmployee, List<string> clientId, Dictionary<string, string> data)
        {
            var model = new FireBaseNotifPostModel()
            {
                registration_ids = clientId,
                data = data,
                notification = new NotifBodyPostModel()
                {
                    title = title,
                    body = body
                },
                priority = 10,
                badge = "1"
            };
            var request = WebRequest.Create(Baseurl) as HttpWebRequest;

            request.KeepAlive = true;
            request.Method = "POST";
            request.ContentType = "application/json; charset=utf-8";

            if (isEmployee)
                request.Headers.Add("Authorization", EmployeeApikey);
            else
                request.Headers.Add("Authorization", UserApikey);

            var strModel = JsonConvert.SerializeObject(model);
            var byteArray = Encoding.UTF8.GetBytes(strModel);

            string responseContent = null;

            try
            {
                using (var writer = request.GetRequestStream())
                {
                    writer.Write(byteArray, 0, byteArray.Length);
                }

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
                return;
            }
            catch (WebException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                System.Diagnostics.Debug.WriteLine(new StreamReader(ex.Response.GetResponseStream()).ReadToEnd());
                return;
            }
        }
        public static void SendToUser(string title, string Notifkey, bool isEmployee, string body, Dictionary<string, string> data)
        {
            SendToUserClient(title, body, isEmployee, new List<string>() { Notifkey }, data);
        }
        //public static string[] GetAllUsers()
        //{
        //    using (var db = new DB_PhotoContestEntities())
        //    {
        //        var userToken = db.TUserClient.Where(u=>!string.IsNullOrEmpty(u.ClientId)&&u.ClientId.Length>50).Select(u=>u.ClientId).ToArray();
        //        return userToken;
        //    }
        //}

        public class FireBaseNotifPostModel
        {
            public List<string> registration_ids { get; set; }
            public NotifBodyPostModel notification { get; set; }
            public int priority { get; set; }
            public Dictionary<string, string> data { get; set; }
            public string badge { get; set; }
        }
        public class NotifBodyPostModel
        {
            public string title { get; set; }
            public string body { get; set; }
        }
        public class NotifBody
        {
            public string body { get; set; }
        }

    }
}