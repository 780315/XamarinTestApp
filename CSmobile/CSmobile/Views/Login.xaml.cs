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

namespace CSmobile
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
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
            }
            else
            {
                await DisplayAlert("Login", "Login Failed", "Ok");
            }
        }

        
        /*private void OnLoginButtonClicked(object sender, EventArgs e)
{

} */
    }
}