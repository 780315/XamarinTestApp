using CSmobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CSmobile.Service
{
    public class ApiServices
    {
        HttpClient client;
        public bool loginStatus { get; set; }

        public async Task LoginAsync(User user)//Login
        {
            var uri = new Uri(string.Format(Constants.LoginUrl, string.Empty));
            client = new HttpClient();
            var userInfo = new { username = user.username, password = user.password };
            var json = JsonConvert.SerializeObject(userInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri,content);            
            
            if (response.IsSuccessStatusCode)
            {
                await TokenValidation(response);                
                loginStatus = true;
            }
            else
            {                
                loginStatus = false;
            }
            

        }
        public async Task TokenValidation(HttpResponseMessage response)
        {
            string requestBody = await response.Content.ReadAsStringAsync();//server response

            var bodyResponse = requestBody.Split(new string[] { "}{" }, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim('{', '}'))
                .Select(s => s.Split(','))
                .ToDictionary(s => s[0], s => s[0]);            

            var tokenValue = JsonConvert.SerializeObject(bodyResponse.Values); 
            var split2 = tokenValue.Split(new string[] { "}{" }, StringSplitOptions.RemoveEmptyEntries)              
                .Select(s => s.Split(':'))
                .ToDictionary(s => s[0], s => s[1]);

            string tokenString = JsonConvert.SerializeObject(split2.Values);
            char[] charsToTrim = { '[','"',']', '\"', '/' };

            string token = tokenString.Trim(charsToTrim).Replace(@"\", string.Empty).Trim('"');

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);                                 
        }

        public async Task GetAccountInfo() // Dummy Method to check Token Validation
        {
            var uri = new Uri(string.Format(Constants.AccountUrl, string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var items = JsonConvert.DeserializeObject(content);
            }
        }
    }
}
