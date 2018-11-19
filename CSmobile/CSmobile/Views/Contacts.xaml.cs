using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSmobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contacts : ContentPage
    {
        public Contacts()
        {
            InitializeComponent();
        }
        public IList<Models.Contacts> list { get; set; }        
        public int results { get; set; }
       
        async void firstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            firstname.Text = e.NewTextValue;
            string searchFilter = firstname.Text;
            if (searchFilter.Length > 3)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetContacts(searchFilter);
                list = App.ApiServices.Contacts;
                if (list.Count != 0)
                {
                    listview.ItemsSource = list;
                    listview.IsVisible = false;
                    listview.IsVisible = true;
                    listShowAll.IsVisible = false;
                    CountResults();
                }
                else
                {
                    listview.ItemsSource = null;
                }
            }           
            if (string.IsNullOrEmpty(firstname.Text))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                Results.Text = "";
            }
        }
               
        private void CountResults()
        {
            results = list.Count();
            if (results > 0)
            {
                Results.Text = "Results: " + results.ToString();
            }
            else
            {
                Results.Text = "";
            }
        }

        private void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (list.Count != 0)
            {
                var value = listview.SelectedItem;
                Models.Contacts contact = (Models.Contacts)value;
                list = new List<Models.Contacts>();
                list.Add(contact);
                listview.ItemsSource = null;
                if (list.Count != 0)
                {
                    listShowAll.ItemsSource = list;
                    listShowAll.IsVisible = false;
                    listShowAll.IsVisible = true;
                    listview.IsVisible = false;
                    Results.Text = "";
                }
            }

        }

        private void GoToMenu(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            Application.Current.MainPage = main;
        }
    }
}