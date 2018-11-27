using CSmobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telerik.XamarinForms.DataControls.ListView;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSmobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Documents : ContentPage
	{       
        public IList<Document> list { get; set; }        
        public int results { get; set; }

        public Documents ()
		{
			InitializeComponent ();
		}

        private async void nameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameSearch.Text = e.NewTextValue;
            string searchFilter = nameSearch.Text;
            if (searchFilter.Length > 3)
            {
                BusyIndicator.IsVisible = true;
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetDocuments(searchFilter);
                list = App.ApiServices.Documents;
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
            if (string.IsNullOrEmpty(searchFilter))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                Results.Text = "";
            }
            BusyIndicator.IsVisible = false;
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

        private void listview_ItemTapped(object sender, ItemTapEventArgs e)
        {
            if (list.Count != 0)
            {
                var value = e.Item;
                Document document = (Document)value;
                list = new List<Document>();
                list.Add(document);
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
            if (listShowAll.IsVisible == false)
            {
                MainPage main = new MainPage();
                Application.Current.MainPage = main;
            }
            if (listShowAll.IsVisible == true)
            {
                listview.IsVisible = true;
                nameSearch.Text = string.Empty;
                listview.ItemsSource = App.ApiServices.Documents;
                listShowAll.IsVisible = false;
                Results.IsVisible = true;
                if (listview.ItemsSource != null)
                {
                    var result = App.ApiServices.Documents.Count();
                    Results.Text = "Results: " + result.ToString();
                }
                
            }
        }

        private void LogOut(object sender, EventArgs e)
        {
            App.ApiServices.Logout();
        }
                
    }
}