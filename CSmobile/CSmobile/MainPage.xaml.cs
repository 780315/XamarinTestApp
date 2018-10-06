using System;
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
        private void GoToMenu(object sender, EventArgs e)
        {
            Login login = new Login();
            Application.Current.MainPage = login;
            //await DisplayAlert("Click","Button has been clicked","OK");
        }
    }
}
