using System;
using System.Timers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CSmobile
{
    public partial class App : Application
    {
        public App() 
        {
            InitializeComponent();
            MainPage = new MainPage(); //Change main page for testing
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
    }
}
