using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CSmobile.Models;

namespace CSmobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tickets : ContentPage
    {
        public int ticketId { get; set; }
        public List<Ticket> list { get; set; }
        string ticketTitle = "?Title=";
        string ticketDescription = "?Description=";
        public string Ttitle { get; set; }
        public string Tdescription { get; set; }
        public int results { get; set; }

        public Tickets()
        {
            InitializeComponent();
        }

        async void CreateTicket(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket(0, 0, title.Text, description.Text, "", Convert.ToInt32(priority.Text), "", 0, "", "", resume.Text, "", "", "", "");
            await App.ApiServices.PostTickets(ticket);
            if (App.ApiServices.ticketPost == true)
            {
                await DisplayAlert("Ticket", "Ticket Created", "Ok");
                ClearFields();
            }
            else
            {
                await DisplayAlert("Ticket", "Ticket Not Created", "Ok");
            }
        }
        private void ClearFields()
        {
            title.Text = string.Empty;
            description.Text = string.Empty;
            priority.Text = string.Empty;
            resume.Text = string.Empty;
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            createLayout.IsVisible = false;
            Create.IsVisible = false;
            lblticket.IsVisible = true;
            searchTickets.IsVisible = true;
            listview.IsVisible = true;
            CreateView.IsVisible = true;
        }

        async void titleSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            titleSearch.Text = e.NewTextValue;
            Ttitle = titleSearch.Text;
            if (Ttitle.Length >= 3 && string.IsNullOrEmpty(Tdescription))
            {
                listview.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketTitle + titleSearch.Text);
                list = App.ApiServices.Tickets;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                CountResults();
            }
            if (titleSearch.Text.Length >= 3 && !string.IsNullOrEmpty(Tdescription) && Tdescription.Length >= 3)
            {
                listview.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketTitle + titleSearch.Text + "&Description=" + Tdescription);
                list = App.ApiServices.Tickets;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                CountResults();
            }
            if (string.IsNullOrEmpty(titleSearch.Text) && string.IsNullOrEmpty(Tdescription))
            {
                listview.ItemsSource = null;
                listview.IsVisible = false;
                listview.IsVisible = true;
                Results.Text = "";
            }
        }

        async void descriptionSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            descriptionSearch.Text = e.NewTextValue;
            Tdescription = descriptionSearch.Text;
            if (descriptionSearch.Text.Length >= 3 && string.IsNullOrEmpty(Ttitle))
            {
                listview.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketDescription + descriptionSearch.Text);
                list = App.ApiServices.Tickets;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                CountResults();
            }
            if (!string.IsNullOrEmpty(Ttitle) && Ttitle.Length >= 3 && descriptionSearch.Text.Length >= 3)
            {
                listview.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketTitle + Ttitle + "&Description=" + descriptionSearch.Text);
                list = App.ApiServices.Tickets;
                listview.ItemsSource = list;
                listview.IsVisible = false;
                listview.IsVisible = true;
                CountResults();
            }
            if (string.IsNullOrEmpty(descriptionSearch.Text) && string.IsNullOrEmpty(Ttitle))
            {
                listview.ItemsSource = null;
                listview.IsVisible = false;
                listview.IsVisible = true;
                Results.Text = "";
            }
        }

        private void CreateView_Clicked(object sender, EventArgs e)
        {
            lblticket.IsVisible = false;
            searchTickets.IsVisible = false;
            listview.IsVisible = false;
            listview.ItemsSource = null;
            CreateView.IsVisible = false;
            createLayout.IsVisible = true;
            Create.IsVisible = true;
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


        //private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string lblText = picker.SelectedItem.ToString();
        //    var label = new Label
        //    {
        //        Text = lblText,
        //        FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
        //        Margin = 5,
        //    };
        //    var entry = new Entry
        //    {
        //        ClassId = lblText,
        //    };            
        //    createLayout.Children.Add(label);
        //    createLayout.Children.Add(entry);


        //}
    }
}