using CSmobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CSmobile.Service
{
    public class ApiServices
    {
        HttpClient client;        
        //public ApiServices()
        //{
        //    client = new HttpClient();
        //    client.MaxResponseContentBufferSize = 256000;            
        //}
        public async Task LoginAsync(User user)
        {
            var uri = new Uri(string.Format(Constants.LoginUrl, string.Empty));
            client = new HttpClient();
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", user.Username),
                new KeyValuePair<string, string>("password", user.Password)
            };
            var json = JsonConvert.SerializeObject(keyValues);
            var content = new StringContent(json, Encoding.UTF8, "application/json-patch+json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri,content);
            if (response.IsSuccessStatusCode)
            {
                Debug.WriteLine(@"Success."+ response.ToString());
            }
            else
            {
                Debug.WriteLine(@"Failed." + response.ToString());
            }

        }
        
    }
}
