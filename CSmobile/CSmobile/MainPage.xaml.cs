﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace CSmobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }
        async void GetAccount(object sender, EventArgs e)
        {
            await App.ApiServices.GetAccountInfo();
        }
       
    }
}
