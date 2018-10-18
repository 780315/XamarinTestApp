using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSmobile.Models
{      
    public class Companies
    {       
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string addition { get; set; }
        public string taxNumber { get; set; }
        public string taxRate { get; set; }
        public string telephone { get; set; }
        public string fax { get; set; }
        public string website { get; set; }
        public int createdBy { get; set; }
        public string createdOn { get; set; }
        public int changedBy { get; set; }
        public string changedOn { get; set; }
        public int version { get; set; }
        public int addressId { get; set; }
        public string email { get; set; }
        public bool inactive { get; set; }
        public string iban { get; set; }
        public string bic { get; set; }
        public string address { get; set; }
        public IList<object> customerSupplierRelation { get; set; }


        public Companies() { }

        public Companies(int id, string code, string name, string addition, string taxNumber, string taxRate, string telephone, string fax, string website, int createdBy, 
            string createdOn, int changedBy, string changedOn, int version, int addressId, string email, bool inactive, string iban, string bic, string address, IList<object> customerSupplierRelation)
        {
            this.id = id;
            this.code = code;
            this.name = name;
            this.addition = addition;
            this.taxNumber = taxNumber;
            this.taxRate = taxRate;
            this.telephone = telephone;
            this.fax = fax;
            this.website = website;
            this.createdBy = createdBy;
            this.createdOn = createdOn;
            this.changedBy = changedBy;
            this.changedOn = changedOn;
            this.version = version;
            this.addressId = addressId;
            this.email = email;
            this.inactive = inactive;
            this.iban = iban;
            this.bic = bic;
            this.address = address;
            this.customerSupplierRelation = customerSupplierRelation;
        }
        
    }
    public class Content
    {
        public IList<Companies> content { get; set; }
        public int status { get; set; }
    }
}
