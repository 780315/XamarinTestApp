﻿using CSmobile.Models;
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
                BusyIndicator.IsVisible = true;
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
            if (listShowAll.IsVisible == false)
            {
                MainPage main = new MainPage();
                Application.Current.MainPage = main;
            }
            if (listShowAll.IsVisible == true)
            {
                listview.IsVisible = true;
                nameSearch.Text = string.Empty;
                listview.ItemsSource = App.ApiServices.Companies;
                listShowAll.IsVisible = false;
                Results.IsVisible = true;
                if (listview.ItemsSource != null)
                {
                    var result = App.ApiServices.Companies.Count();
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