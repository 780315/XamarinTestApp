using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{
    public class ServerList
    {
        public int id { get; set; }
        public string clientName { get; set; }
        public string frontEndUrl { get; set; }
        public string backEndUrl { get; set; }
    }
}
