using CSmobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
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
        public bool ticketPost { get; set; }
        public string token { get; set; }        
        public List<Ticket> Tickets { get; set; }
        public List<Contacts> Contacts { get; set; }
        public IList<Companies> Companies { get; set; }

        private JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        };

        public async Task LoginAsync(User user)//Login
        {
            var uri = new Uri(string.Format(Constants.LoginUrl, string.Empty));
            client = new HttpClient();
            var userInfo = new { username = user.username, password = user.password };
            var json = JsonConvert.SerializeObject(userInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);

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
            char[] charsToTrim = { '[', '"', ']', '\"', '/' };

            token = tokenString.Trim(charsToTrim).Replace(@"\", string.Empty).Trim('"');

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }
        
        public async Task GetTickets(string search)
        {
            var uri = new Uri(string.Format(Constants.TicketsUrl + search));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();                
                Tickets = JsonConvert.DeserializeObject<List<Ticket>>(content, settings);
            }
        }

        public async Task PostTickets(Ticket ticket)
        {
            var uri = new Uri(string.Format(Constants.TicketsUrl, string.Empty));
            client = new HttpClient();
            var ticketInfo = new
            {
                id = ticket.id,
                assignedUserId = ticket.assignedUserId,
                title = ticket.title,
                description = ticket.description,
                descriptionHtml = ticket.descriptionHtml,
                priority = ticket.priority,
                priorityName = ticket.priorityName,
                status = ticket.status,
                statusName = ticket.statusName,
                ticketID = ticket.ticketID,
                resume = ticket.resume,
                estimatedDate = ticket.estimatedDate,
                changedOn = ticket.changedOn,
                createdOn = ticket.createdOn,
                estimatedOn = ticket.estimatedOn
            };
            var json = JsonConvert.SerializeObject(ticketInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                ticketPost = true;
            }
            else
            {
                ticketPost = false;
            }
        }

        public async Task GetContacts(string search)
        {
            var uri = new Uri(string.Format(Constants.ContactsUrl + search));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Contacts = JsonConvert.DeserializeObject<List<Contacts>>(content, settings);
            }
        }

        public async Task GetCompanies(string search)
        {
            var uri = new Uri(string.Format(Constants.CompaniesUrl + search));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Companies = JsonConvert.DeserializeObject<Content>(content, settings).content;
            }
        }

        public async Task Logout()
        {
            var uri = new Uri(string.Format(Constants.LogoutUrl, string.Empty));

        }


    }
}
