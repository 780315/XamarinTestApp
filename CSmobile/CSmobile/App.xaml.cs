using CSmobile.Service;
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
    }
}
