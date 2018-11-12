using CSmobile.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using static CSmobile.Service.ApiServices;

namespace CSmobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            DependencyService.Get<CSmobile.Models.IForceKeyboardDismissalService>().DismissKeyboard();            
        }       
               
        private void Tickets(object sender, EventArgs e)
        {
            Tickets tickets = new Tickets();
            Application.Current.MainPage = tickets;
        }

        private void Contacts(object sender, EventArgs e)
        {
            Contacts contactsPage = new Contacts();
            Application.Current.MainPage = contactsPage;
        }

        private void Companies(object sender, EventArgs e)
        {
            Companie companiePage = new Companie();
            Application.Current.MainPage = companiePage;
        }

        private void Tasks(object sender, EventArgs e)
        {
            Tasks tasks = new Tasks();
            Application.Current.MainPage = tasks;
        }

        private void Logout(object sender, EventArgs e)
        {
            App.ApiServices.Logout();
        }

        private void DocumentSearch(object sender, EventArgs e)
        {
            Documents documents = new Documents();
            Application.Current.MainPage = documents;
        }
    }
}
