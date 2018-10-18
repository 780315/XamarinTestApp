using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class Contacts
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string telephone { get; set; }
        public string mobile { get; set; }
        public string fax { get; set; }
        public string skype { get; set; }
        public string facebook { get; set; }
        public string xing { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public string street { get; set; }
        public string FirstLastName { get { return firstName + " " + lastName; } }

        public Contacts() { }

        public Contacts(string firstName, string lastName, string email, string telephone, string mobile, string fax, string skype, string facebook, string xing, 
            string country, string region, string city, string zip, string street)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.telephone = telephone;
            this.mobile = mobile;
            this.fax = fax;
            this.skype = skype;
            this.facebook = facebook;
            this.xing = xing;
            this.country = country;
            this.region = region;
            this.city = city;
            this.zip = zip;
            this.street = street;
        }
    }
}
