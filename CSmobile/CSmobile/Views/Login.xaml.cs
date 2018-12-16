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
using System.Threading;

namespace CSmobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        MainPage main = new MainPage();
        public Login()
        {
            InitializeComponent();
            LogIn.IsEnabled = false;
            Lbl_Click.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => ForgotPass()),
            });
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

        //private async void getServers(object sender, TextChangedEventArgs e)
        //{
        //    username.Text = e.NewTextValue;
        //    string email = username.Text;
        //    if (email.Contains("@") && email.Contains(".") && username.IsFocused == false)
        //    {
        //        await App.ApiServices.GetServers(email);
        //        if (App.ApiServices.Servers.Count > 0)
        //        {
        //            serverList.ItemsSource = App.ApiServices.Servers;
        //            serverList.IsVisible = true;
        //            serverList.Focus();
        //        }
        //        else
        //        {
        //            serverList.ItemsSource = null;
        //        }
        //    }
        //    else
        //    {
        //        LogIn.IsEnabled = false;
        //        try
        //        {
        //            serverList.SelectedIndex = -1;
        //        }
        //        catch (NullReferenceException)
        //        {

        //        }
        //        serverList.ItemsSource = null;
        //        App.ApiServices.serverURL = string.Empty;
        //        Application.Current.Properties["serverURL"] = App.ApiServices.serverURL;
        //    }
        //}

        private void selectedServer(object sender, EventArgs e)
        {
            int num = serverList.SelectedIndex; 
            if (num != -1)
            {
                serverList.SelectedIndex = num;
                ServerList server = new ServerList();
                server = serverList.SelectedItem as ServerList;
                App.ApiServices.serverURL = server.backEndUrl;
                Application.Current.Properties["serverURL"] = App.ApiServices.serverURL;
                LogIn.IsEnabled = true;
                password.IsVisible = true;
                showPass.IsVisible = true;
                password.Focus();
            }
        }

        private async void Username_Unfocused(object sender, FocusEventArgs e)
        {            
            if (username.Text != string.Empty && username.Text.Contains("@") && username.Text.Contains("."))
            {
                await App.ApiServices.GetServers(username.Text);
                if (App.ApiServices.Servers.Count > 0)
                {
                    serverList.ItemsSource = App.ApiServices.Servers;
                    serverList.IsVisible = true;
                    serverList.Focus();
                }
                else
                {
                    serverList.ItemsSource = null;
                }
            }
            else
            {
                LogIn.IsEnabled = false;
                try
                {
                    serverList.SelectedIndex = -1;
                }
                catch (NullReferenceException)
                {

                }
                serverList.ItemsSource = null;
                App.ApiServices.serverURL = string.Empty;
                Application.Current.Properties["serverURL"] = App.ApiServices.serverURL;
            }
        }

        private void ShowPassword(object sender, EventArgs e)
        {            
            if (password.IsPassword == true)
            {
                password.IsPassword = false;
            }
            else
            {
                password.IsPassword = true;
            }
            
        }

        private async void ForgotPass()
        {
            await DisplayAlert("Password Forgotten", "Method to be implemented", "Ok");
        }
    }
}