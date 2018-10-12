using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class Constants
    {
        public static bool IsDev = true;
        public static string LoginUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Account/Login";
        public static string AccountUrl = "http://contentsharewebapi.soltystudio.com/api/v1/Account";
        public static string Username;
        public static string Password;
        public User user = new User(Username, Password);
    }
}
