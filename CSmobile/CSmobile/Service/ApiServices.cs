using Android.Content;
using Android.Views.InputMethods;
using CSmobile.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Android.Service.Autofill;
using Java.IO;
using Xamarin.Forms;
using Console = System.Console;

namespace CSmobile.Service
{
    public class ApiServices
    {
        HttpClient client;
        public bool loginStatus { get; set; }
        public bool ticketPost { get; set; }
        public string tokenSave { get; set; }
        public bool taskPost { get; set; }
        public bool TokenValid { get; set; }
        private bool taskDeleted { get; set; }
        public bool loginSkip { get; set; }
        public string token { get; set; }
        public List<Ticket> Tickets { get; set; }
        public IList<Tasks> Tasks { get; set; }
        public IList<Contacts> Contacts { get; set; }
        public IList<Companies> Companies { get; set; }
        public IList<Document> Documents { get; set; }
        public object docInfo { get; set; }
        public int id { get; set; }
        public bool taskPut { get; set; }
        public bool ticketPut { get; set; }

        private JsonSerializerSettings settings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            MissingMemberHandling = MissingMemberHandling.Ignore
        }; // settings to deserialize Json and ignore null values        

        public async Task LoginAsync(User user) // Login
        {
            var uri = new Uri(string.Format(Constants.LoginUrl, string.Empty));
            client = new HttpClient();
            var userInfo = new {username = user.username, password = user.password};
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

        public async Task TokenValidation(HttpResponseMessage response) // Token retrieval and validation
        {
            string requestBody = await response.Content.ReadAsStringAsync(); //server response

            var bodyResponse = requestBody.Split(new string[] {"}{"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim('{', '}'))
                .Select(s => s.Split(','))
                .ToDictionary(s => s[0], s => s[0]);

            var tokenValue = JsonConvert.SerializeObject(bodyResponse.Values);
            var split2 = tokenValue.Split(new string[] {"}{"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(':'))
                .ToDictionary(s => s[0], s => s[1]);

            string tokenString = JsonConvert.SerializeObject(split2.Values);
            char[] charsToTrim = {'[', '"', ']', '\"', '/'};

            token = tokenString.Trim(charsToTrim).Replace(@"\", string.Empty).Trim('"');
            Application.Current.Properties["token"] = token;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
        }

        public async Task GetTickets(string search) // Ticket search
        {
            var uri = new Uri(string.Format(Constants.TicketsUrl + search));
            HttpResponseMessage response = null;
            client = new HttpClient();
            LoginSkip(client);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Tickets = JsonConvert.DeserializeObject<List<Ticket>>(content, settings);
            }
            else
            {
                Tickets = new List<Ticket>();
                RedirectLogin(response);
            }
        }

        public async Task PostTickets(Ticket ticket) // Ticket creation
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
            LoginSkip(client);
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                GetId(response);
                ticketPost = true;
            }
            else
            {
                ticketPost = false;
                RedirectLogin(response);
            }
        }

        public async Task PutTickets(Ticket ticket) // Ticket creation
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
            LoginSkip(client);
            response = await client.PutAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                GetId(response);
                ticketPut = true;
            }
            else
            {
                ticketPut = false;
                RedirectLogin(response);
            }
        }

        public async Task GetContacts(string search) // new search
        {
            var uri = new Uri(string.Format("http://contentsharewebapi.soltystudio.com/api/v1/Search?EntityType=contactmodel&Query=" + search + "&Size=100"));
            HttpResponseMessage response = null;
            client = new HttpClient();
            LoginSkip(client);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Contacts = JsonConvert.DeserializeObject<ContactsResult>(content, settings).items;
            }
            else
            {
                Contacts = new List<Contacts>();
                RedirectLogin(response);
            }
        }

        public async Task GetCompanies(string search) // new search
        {
            var uri = new Uri(string.Format("http://contentsharewebapi.soltystudio.com/api/v1/Search?EntityType=companymodel&Query=" + search + "&Size=100"));
            HttpResponseMessage response = null;
            client = new HttpClient();
            LoginSkip(client);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Companies = JsonConvert.DeserializeObject<Content>(content, settings).items;
            }
            else
            {
                Companies = new List<Companies>();
                RedirectLogin(response);
            }
        }

        public async Task GetDocuments(string search) // new search
        {
            var uri = new Uri(string.Format("http://contentsharewebapi.soltystudio.com/api/v1/Search?EntityType=documentmodel&Query=" + search + "&Size=100")); // change url in constants
            HttpResponseMessage response = null;
            client = new HttpClient();
            LoginSkip(client);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Documents = JsonConvert.DeserializeObject<Example>(content, settings).items;
            }
            else
            {
                Documents = new List<Document>();
                RedirectLogin(response);
            }
        }

        public async Task GetTask(string search)// new search
        {
            var uri = new Uri(string.Format("http://contentsharewebapi.soltystudio.com/api/v1/Search?EntityType=taskmodel&Query=" + search + "&Size=100"));
            HttpResponseMessage response = null;
            client = new HttpClient();
            LoginSkip(client);
            response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Tasks = JsonConvert.DeserializeObject<Items>(content, settings).items;                             
            }
            else
            {
                Tasks = new List<Tasks>();
                RedirectLogin(response);
            }
        } // no filter

        public async Task PutTask(Tasks tasks)
        {
            var uri = new Uri(string.Format(Constants.TasksUrl, string.Empty));
            client = new HttpClient();
            var tasksInfo = new
            {
                id = tasks.id,
                title = tasks.title,
                description = tasks.description,
                descriptionHtml = tasks.descriptionHtml,
                assignedUserId = tasks.assignedUserId,
                deadline = tasks.deadline,
                deadlineType = tasks.deadlineType,
                clientId = tasks.clientId,
                contactPersonId = tasks.contactPersonId,
                status = tasks.status,
                reminderInterval = tasks.reminderInterval,
                inCalendar = tasks.inCalendar,
                parentTask = tasks.parentTask,
                fromDate = tasks.fromDate,
                createdBy = tasks.createdBy,
                createdOn = tasks.createdOn,
                changedBy = tasks.changedBy,
                changedOn = tasks.changedOn,
                version = tasks.version
            };
            var json = JsonConvert.SerializeObject(tasksInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            LoginSkip(client);
            response = await client.PutAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                taskPut = true;
            }
            else
            {
                taskPut = false;
                RedirectLogin(response);
            }
        } // task edit

        public async Task PostTasks(Tasks tasks) // Tasks Creation
        {
            var uri = new Uri(string.Format(Constants.TasksUrl, string.Empty));
            client = new HttpClient();
            var tasksInfo = new
            {
                id = tasks.id,
                title = tasks.title,
                description = tasks.description,
                descriptionHtml = tasks.descriptionHtml,
                assignedUserId = tasks.assignedUserId,
                deadline = tasks.deadline,
                deadlineType = tasks.deadlineType,
                clientId = tasks.clientId,
                contactPersonId = tasks.contactPersonId,
                status = tasks.status,
                reminderInterval = tasks.reminderInterval,
                inCalendar = tasks.inCalendar,
                parentTask = tasks.parentTask,
                fromDate = tasks.fromDate,
                createdBy = tasks.createdBy,
                createdOn = tasks.createdOn,
                changedBy = tasks.changedBy,
                changedOn = tasks.changedOn,
                version = tasks.version
            };
            var json = JsonConvert.SerializeObject(tasksInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            LoginSkip(client);
            response = await client.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                taskPost = true;
            }
            else
            {
                taskPost = false;
                RedirectLogin(response);
            }
        }

        private void LoginSkip(HttpClient client)
        {
            if (loginSkip == true)
            {
                tokenSave = Application.Current.Properties["token"].ToString();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", tokenSave);
            }
            else
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                tokenSave = string.Empty;
            }
        } // Skip login and token saving

        private async void RedirectLogin(HttpResponseMessage response)
        {
            int code = (int) response.StatusCode;
            if (code == 401)
            {
                await App.Current.MainPage.DisplayAlert("Login", "Authentication Failed, Login Again", "Ok");
                Logout();
            }
            else
            {
                MainPage main = new MainPage();
                await App.Current.MainPage.DisplayAlert("Error", "Error, try again later", "Ok");
                Application.Current.MainPage = main;
            }
        } // Token validation and login redirection

        public void Logout() // Logout
        {
            Login login = new Login();
            if (Application.Current.Properties.ContainsKey("token"))
            {
                Application.Current.Properties["token"] = string.Empty;
                Application.Current.MainPage = login;
            }

            Application.Current.MainPage = login;
        }

        public async Task DocumentInit(string name, string type, Stream stream) // Not working
        {
            #region previousCode
            /*var uri = new Uri(string.Format(Constants.DocumentInit, string.Empty));
                client = new HttpClient();
                //docInfo = new { entityType = type, entityId = 0, documentName = name, size = img, savedAsTemp = true, createdOn = DateTime.Now.ToShortDateString(), checkSum = checkSum };
                //var json = JsonConvert.SerializeObject(docInfo);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                var values = new Dictionary<string, object>
                {
                   { "entityType" , type },
                   { "entityId" , "0" },
                   { "documentName" , name },
                   {"files",  stream}
                };

                var content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = null;
                LoginSkip(client);
                response = await client.PostAsync(uri, content);


                var responseString = await response.Content.ReadAsStringAsync();
                */ 
            #endregion

            try
            {
                byte[] data = new byte[10* 1024 * 1024];
                using (MemoryStream ms = new MemoryStream())
                {
                    int read;
                    while ((read = stream.Read(data, 0, data.Length)) > 0)
                    {
                        ms.Write(data, 0, read);
                    }
                    ms.ToArray();
                }
                
                HttpWebRequest wr = (HttpWebRequest) WebRequest.Create(Constants.DocumentInit);
                wr.ContentType = "multipart/form-data";
                wr.ContentLength = data.Length;
                wr.Method = "POST";
                wr.KeepAlive = true;
                //wr.Headers.Add("UserAgent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");
                wr.Headers.Add("entityType", type);
                wr.Headers.Add("entityId", "0");
                wr.Headers.Add("documentName", name);
                wr.Headers.Add("Authentication", "bearer " + Application.Current.Properties["token"].ToString());
               
                using (Stream postStream = wr.GetRequestStream())
                {   
                    // Send the data.
                    postStream.Write(data, 0, data.Length);
                    postStream.Close();
                    var webResponse = wr.GetResponse(); // 502 bad gateway error
                }
            }
            catch (Exception ex)
            {
                //Log exception here...
                Console.Write("");
            }
        }
       
        public async Task DocumentCommit(string name, int img, string Type) // Not working
        {
            var uri = new Uri(string.Format(Constants.DocumentCommit, string.Empty));
            client = new HttpClient();
            docInfo = new {entityType = Type, entityId = id, documentName = name, files = img};
            var json = JsonConvert.SerializeObject(docInfo);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await client.PostAsync(uri, content);
        }

        private async void GetId(HttpResponseMessage response) //working
        {
            string request = await response.Content.ReadAsStringAsync();
            var bodyResponse = request.Split(new string[] {"}{"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim('{', '}'))
                .Select(s => s.Split(','))
                .ToDictionary(s => s[0], s => s[0]);

            var requestValue = JsonConvert.SerializeObject(bodyResponse.Values);
            var split2 = requestValue.Split(new string[] {"}{"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Split(':'))
                .ToDictionary(s => s[0], s => s[1]);

            string idValue = JsonConvert.SerializeObject(split2.Values);
            char[] charsToTrim = {'[', '"', ']', '\"', '/'};

            id = Convert.ToInt32(idValue.Trim(charsToTrim).Replace(@"\", string.Empty).Trim('"'));
        }
    }
}