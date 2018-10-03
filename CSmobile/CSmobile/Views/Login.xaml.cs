using CSmobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}

        void SignInProcedure(object sender, EventArgs e)
        {
            User user = new User(Entry_username.Text, Entry_password.Text);

            if(user.CheckInfo())
            {
                DisplayAlert("Login", "Login Success", "Oke");
            }
            else
            {
                DisplayAlert("Login", "Login Not Success, empty username or password", "Oke");
            }

        }

        /*private void OnLoginButtonClicked(object sender, EventArgs e)
        {

        } */
    }
}