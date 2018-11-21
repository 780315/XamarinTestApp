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
            LblClick();            
        }
        
        void LblClick()
        {
            Lbl_Click.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    DisplayAlert("Password Forgotten", "Password Reset", "OK");
                })
            });
        }

        async void LogInProcedure(object sender, EventArgs e)
        {
            User user = new User(username.Text, password.Text);
            if (user.CheckInformation())
            {                
                await App.ApiServices.LoginAsync(user);                
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
       
    }
}