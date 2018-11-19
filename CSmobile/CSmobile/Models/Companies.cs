using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class City
    {
        public string id { get; set; }
        public string name { get; set; }
    }

    public class Address
    {
        public string id { get; set; }
        public City city { get; set; }
        public int cityID { get; set; }
        public string country { get; set; }
        public object region { get; set; }
        public object regionID { get; set; }
        public string street { get; set; }
        public string zip { get; set; }
    }

    public class Companies
    {
        public string Id { get; set; }
        public object addition { get; set; }
        public Address address { get; set; }
        public int addressID { get; set; }
        public object changedOn { get; set; }
        public string code { get; set; }
        public int createdBy { get; set; }
        public DateTime createdOn { get; set; }
        public object email { get; set; }
        public string fax { get; set; }
        public bool inactive { get; set; }
        public string name { get; set; }
        public string taxNumber { get; set; }
        public object taxRate { get; set; }
        public string telephone { get; set; }
        public string website { get; set; }
        public string customProperties { get; set; }
        public int id { get; set; }
    }

    public class Content
    {
        public IList<Companies> items { get; set; }
        public int total { get; set; }
    }
}


