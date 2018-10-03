using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(string UserName, string Password)
        {
            this.UserName = UserName;
            this.Password = Password;

        }

        public bool CheckInfo()
        {

            if (!this.UserName.Equals("") && !this.Password.Equals(""))

                return true;
            else
                return false;


        }
    }
}
