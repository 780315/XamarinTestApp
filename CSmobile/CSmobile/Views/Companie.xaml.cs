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
        public string Name { get; set; }
        public IList<Companies> list { get; set; }
        public string companieName = "?Name=";
        public int results { get; set; }

        public Companie ()
		{
			InitializeComponent ();
		}

        async void nameSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            nameSearch.Text = e.NewTextValue;
            Name = nameSearch.Text;
            if (Name.Length >= 3)
            {
                listview.ItemsSource = null;
                await App.ApiServices.GetCompanies(companieName + nameSearch.Text);
                list = App.ApiServices.Companies;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                CountResults();
            }
            if(string.IsNullOrEmpty(Name))
            {
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

    }
}