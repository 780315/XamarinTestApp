using CSmobile.Models;
using CSmobile.Service;
using CSmobile.Views;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CSmobile
{
    public partial class App : Application
    {
        static ApiServices apiServices;

        public App()
        {
            InitializeComponent();
            MainPage = new FirstPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts   
            //SetTimer();
            Logged();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public static ApiServices ApiServices
        {
            get
            {
                if (apiServices == null)
                {
                    apiServices = new ApiServices();
                }
                return apiServices;
            }
        }

        private static void SetTimer()
        {
            var aTimer = new System.Timers.Timer(10);
            // Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += Logged;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }
        public static void ChangePage(Object source, ElapsedEventArgs e)
        {
            Login login = new Login();
            Application.Current.MainPage = login;
        }

        private static void Logged()
        {
            if (Application.Current.Properties.ContainsKey("token") && Application.Current.Properties.ContainsKey("serverURL"))
            {
                App.ApiServices.tokenSave = Application.Current.Properties["token"] as string;
                App.ApiServices.serverURL = Application.Current.Properties["serverURL"] as string;
                App.ApiServices.loginSkip = true;
                if (App.ApiServices.tokenSave != string.Empty)
                {
                    MainPage main = new MainPage();
                    Application.Current.MainPage = main;
                }
                else
                {
                    Login login = new Login();
                    Application.Current.MainPage = login;
                }
            }
            else
            {
                App.ApiServices.loginSkip = false;
                Login login = new Login();
                Application.Current.MainPage = login;
            }
        }

    }
}
