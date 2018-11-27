using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public static string LoginUrl = "/api/v1/Account/Login";
        public static string LogoutUrl = "/api/v1/Account/Logout";
        public static string TicketsUrl = "/api/v1/Tickets?Title=";
        public static string ContactsUrl = "/api/v1/Contacts";
        public static string CompaniesUrl = "/api/v1/Companies";        
        public static string TasksUrl = "/api/v1/Tasks";
        public static string DocumentSearch = "/api/v1/Documents?EntityId=";
        public static string DocumentInit = "/api/v1/Documents/.init";
        public static string DocumentCommit = "/api/v1/Documents/.commit";

        public static string Username;
        public static string Password;
        public User user = new User(Username, Password);
    }
}
