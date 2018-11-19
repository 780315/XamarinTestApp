using CSmobile.Models;
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
	public partial class Companie : ContentPage
	{        
        public IList<Companies> list { get; set; }
        public int results { get; set; }

        public Companie ()
		{
			InitializeComponent ();
		}

        async void nameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameSearch.Text = e.NewTextValue;
            string searchFilter = nameSearch.Text;
            if (searchFilter.Length > 0)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetCompanies(searchFilter);
                list = App.ApiServices.Companies;
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
            if(string.IsNullOrEmpty(searchFilter))
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
                Companies companie = (Companies)value;
                list = new List<Companies>();
                list.Add(companie);
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