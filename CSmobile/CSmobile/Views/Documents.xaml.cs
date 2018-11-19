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
            MainPage main = new MainPage();
            Application.Current.MainPage = main;
        }
    }
}