using Android.Content;
using Android.Views.InputMethods;
using CSmobile.Models;
using CSmobile.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Telerik.XamarinForms.Input;


namespace CSmobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        MainPage main = new MainPage();        
		public Login ()
		{
            InitializeComponent();
            LogIn.IsEnabled = false;           
        }                

        async void LogInProcedure(object sender, EventArgs e)
        {           
            User user = new User(username.Text, password.Text);
            if (user.CheckInformation())
            {                
                await App.ApiServices.LoginAsync(App.ApiServices.serverURL, user);                
                if (App.ApiServices.loginStatus == true)
                {                    
                    await DisplayAlert("Login", "Login Successfull", "Ok");
                    Application.Current.MainPage = main;
                }
                else
                {
                    await DisplayAlert("Login", "Login Failed", "Ok");
                }
            }
            else
            {
                await DisplayAlert("Login", "Login Failed, fill the username and password fields!", "Ok");
            }
        }

        private async void getServers(object sender, TextChangedEventArgs e)
        {
            username.Text = e.NewTextValue;
            string email = username.Text;
            await App.ApiServices.GetServers(email);
            if (App.ApiServices.Servers.Count > 0)
            {
                serverList.ItemsSource = App.ApiServices.Servers;
            }          
        }               

        private void selectedServer(object sender, EventArgs e)
        {
            ServerList server = new ServerList();
            server = serverList.SelectedItem as ServerList;
            App.ApiServices.serverURL = server.backEndUrl;
            Application.Current.Properties["serverURL"] = App.ApiServices.serverURL;
            LogIn.IsEnabled = true;
        }
    }
}