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
        public List<Models.Contacts> list { get; set; }
        string FirstName = "?FirstName=";
        string LastName = "?LastName=";
        public string FName { get; set; }
        public string LName { get; set; }
        public int results { get; set; }
       
        async void firstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            firstname.Text = e.NewTextValue;
            FName = firstname.Text;
            if (firstname.Text.Length >= 3 && string.IsNullOrEmpty(LName))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetContacts(FirstName + firstname.Text);
                list = App.ApiServices.Contacts;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                listShowAll.IsVisible = false;
                CountResults();
            }
            if (firstname.Text.Length >= 0 && !string.IsNullOrEmpty(LName) && LName.Length >= 3)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetContacts(FirstName + firstname.Text + "&LastName=" + LName);
                list = App.ApiServices.Contacts;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                listShowAll.IsVisible = false;
                CountResults();
            }
            if (string.IsNullOrEmpty(firstname.Text) && string.IsNullOrEmpty(LName))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                Results.Text = "";
            }
        }

        async void lastName_TextChanged(object sender, TextChangedEventArgs e)
        {            
            lastname.Text = e.NewTextValue;
            LName = lastname.Text;
            if (lastname.Text.Length >= 3 && string.IsNullOrEmpty(FName))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetContacts(LastName + lastname.Text);
                list = App.ApiServices.Contacts;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                listShowAll.IsVisible = false;
                CountResults();
            }           
            if (!string.IsNullOrEmpty(FName) && FName.Length >= 3 && lastname.Text.Length >= 0)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetContacts(FirstName + FName + "&LastName=" + lastname.Text);
                list = App.ApiServices.Contacts;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                listShowAll.IsVisible = false;
                CountResults();
            }
            if (string.IsNullOrEmpty(lastname.Text) && string.IsNullOrEmpty(FName))
            {
                listShowAll.ItemsSource = null;
                listview.ItemsSource = null;
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