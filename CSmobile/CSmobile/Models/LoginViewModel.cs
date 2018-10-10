using CSmobile.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CSmobile.Models
{
    public class LoginViewModel
    {
        private ApiServices _apiServices = new ApiServices();
        public string username { get; set; }
        public string password { get; set; }


        //public ICommand LoginCommand
        //{
        //    get
        //    {
        //        return new Command(async () =>
        //        {
        //            await _apiServices.LoginAsync(username, password);
        //        });
        //    }
        //}
    }
}
