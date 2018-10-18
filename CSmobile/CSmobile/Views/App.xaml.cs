using CSmobile.Service;
using CSmobile.Views;
using System;
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
            MainPage = new Login();             
            //MainPage = new Tickets();
            //SetTimer();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
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

            var aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += ChangePage;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }
        public static void ChangePage(Object source, ElapsedEventArgs e)
        {
            Login login = new Login();
            Application.Current.MainPage = login;
        }

    }
}
