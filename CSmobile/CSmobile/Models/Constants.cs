using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public static string LoginUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Account/Login";
        public static string LogoutUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Account/Logout";
        public static string TicketsUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Tickets?Title=";
        public static string ContactsUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Contacts";
        public static string CompaniesUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Companies";
        //public static string TicketsIdUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Tickets?Title=";
        public static string TasksUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Tasks";
        public static string DocumentSearch = "http://contentsharewebapi.soltystudio.com/api/v1/Documents?EntityId=";
        public static string DocumentInit = "http://contentsharewebapi.soltystudio.com/api/v1/Documents/.init";
        public static string DocumentCommit = "http://contentsharewebapi.soltystudio.com/api/v1/Documents/.commit";

        public static string Username;
        public static string Password;
        public User user = new User(Username, Password);
    }
}
