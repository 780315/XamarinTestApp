using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class User
    {
        [JsonProperty]
        public string username { get; set; }
        [JsonProperty]
        public string password { get; set; }

        public User() { }
        public User(string Username, string Password)
        {
            this.username = Username;
            this.password = Password;    
        }

        public bool CheckInformation()
        {
            if (String.IsNullOrEmpty(this.username) && String.IsNullOrEmpty(this.password))
                return false;
            else
                return true;
        }
    }
}
