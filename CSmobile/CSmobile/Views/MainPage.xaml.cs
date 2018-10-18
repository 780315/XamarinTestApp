using CSmobile.Views;
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
       
               
        private void Tickets(object sender, EventArgs e)
        {
            Tickets ticketPage = new Tickets(); 
            Application.Current.MainPage = ticketPage;
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
    }
}
