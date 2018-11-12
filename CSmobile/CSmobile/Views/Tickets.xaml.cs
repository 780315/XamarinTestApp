using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CSmobile.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using System.Security.Cryptography;

namespace CSmobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tickets : ContentPage
    {
        public int ticketId { get; set; }
        public List<Ticket> list { get; set; }
        string ticketTitle = "?Title=";
        string ticketDescription = "?Description=";
        private MediaFile _mediaFile;
        public Ticket ticket { get; set; }

        public string Ttitle { get; set; }
        public string Tdescription { get; set; }
        public int results { get; set; }
        public long img { get; set; }
        public string imgName { get; set; }
        public string checkSum { get; set; }

        public Tickets()
        {
            InitializeComponent();
        }

        async void CreateTicket(object sender, EventArgs e)
        {
            Ticket ticket = new Ticket(0, 0, title.Text, description.Text, "", 0, "", 0, "", "", "", "", "", "", "");
            await App.ApiServices.PostTickets(ticket);
            if (App.ApiServices.ticketPost == true)
            {
                await App.ApiServices.DocumentCommit(imgName, Convert.ToInt32(img), "Ticket");
                await DisplayAlert("Ticket", "Ticket Created", "Ok");
                ClearFields();
            }
            else
            {
                await DisplayAlert("Ticket", "Ticket Not Created", "Ok");
            }
        }

        async void EditTicketSave(object sender, EventArgs e)
        {
            Ticket ticketVal = new Ticket(ticket.id, ticket.assignedUserId, title.Text, description.Text, ticket.descriptionHtml, ticket.priority, ticket.priorityName, ticket.status, ticket.statusName,
                ticket.ticketID, ticket.resume, ticket.estimatedDate, "", ticket.createdOn, ticket.estimatedOn);
            await App.ApiServices.PutTickets(ticketVal);
            if (App.ApiServices.ticketPut == true)
            {
                await DisplayAlert("Ticket", "Ticket Edited", "Ok");
            }
            else
            {
                await DisplayAlert("Ticket", "Ticket Not Edited", "Ok");
            }
            HideCreateView();
            titleSearch.Text = string.Empty;
            descriptionSearch.Text = string.Empty;
            searchTickets.IsVisible = true;
        }


        private void ClearFields()
        {
            title.Text = string.Empty;
            description.Text = string.Empty;
            document.Text = string.Empty;
        }

        private async void DocumentUpload(object sender, EventArgs e)// method working ?
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await App.Current.MainPage.DisplayAlert("Document", "Unsupported file", "Ok");
                return;
            }
            _mediaFile = await CrossMedia.Current.PickPhotoAsync();

            if (_mediaFile == null)
            {
                return;
            }

            FileImage.Source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });

            using (var memoryStream = new MemoryStream())
            {
                _mediaFile.GetStream().CopyTo(memoryStream);
                img = memoryStream.Length;
            }
            string str = Convert.ToString(_mediaFile.Path);

            imgName = str.Substring(str.LastIndexOf('/') + 1);

            await App.ApiServices.DocumentInit(imgName, "Ticket", _mediaFile.GetStream());
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
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketTitle + titleSearch.Text);
                list = App.ApiServices.Tickets;
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
            if (titleSearch.Text.Length >= 0 && !string.IsNullOrEmpty(Tdescription) && Tdescription.Length >= 3)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketTitle + titleSearch.Text + "&Description=" + Tdescription);
                list = App.ApiServices.Tickets;
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
            if (string.IsNullOrEmpty(titleSearch.Text) && string.IsNullOrEmpty(Tdescription))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
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
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketDescription + descriptionSearch.Text);
                list = App.ApiServices.Tickets;
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
            if (!string.IsNullOrEmpty(Ttitle) && Ttitle.Length >= 3 && descriptionSearch.Text.Length >= 0)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetTickets(ticketTitle + Ttitle + "&Description=" + descriptionSearch.Text);
                list = App.ApiServices.Tickets;
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
            if (string.IsNullOrEmpty(descriptionSearch.Text) && string.IsNullOrEmpty(Ttitle))
            {
                listShowAll.ItemsSource = null;
                listview.ItemsSource = null;
                Results.Text = "";
            }
        }

        private void CreateView_Clicked(object sender, EventArgs e)
        {
            Results.Text = "";
            titleSearch.Text = "";
            descriptionSearch.Text = "";
            lblticket.IsVisible = false;
            searchTickets.IsVisible = false;
            listview.IsVisible = false;
            listview.ItemsSource = null;
            CreateView.IsVisible = false;
            createLayout.IsVisible = true;
            Create.IsVisible = true;
            Label1.IsVisible = true;
            editLbl.IsVisible = false;
            docLbl.IsVisible = true;
            Back.IsVisible = true;
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
                Ticket ticket = (Ticket)value;
                list = new List<Ticket>();
                list.Add(ticket);
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

        private void ShowCreateView()
        {
            Results.Text = "";
            lblticket.IsVisible = false;
            listview.IsVisible = false;
            listview.ItemsSource = null;
            CreateView.IsVisible = false;
            createLayout.IsVisible = true;
            Create.IsVisible = true;
        }
        private void HideCreateView()
        {
            Results.Text = "";
            lblticket.IsVisible = true;
            listview.IsVisible = true;
            listview.ItemsSource = null;
            CreateView.IsVisible = true;
            createLayout.IsVisible = false;
            Create.IsVisible = false;
            Edit.IsVisible = false;
        }

        private void EditTicket(object sender, EventArgs e)
        {
            var value = listview.SelectedItem;
            ticket = (Ticket)value;
            title.Text = ticket.title;
            description.Text = ticket.description;
            ShowCreateView();
            listShowAll.ItemsSource = null;
            listShowAll.IsVisible = false;
            Create.IsVisible = false;
            Edit.IsVisible = true;
            scrollView.ScrollToAsync(createLayout, ScrollToPosition.Start, false);
            Label1.IsVisible = false;
            editLbl.IsVisible = true;
            editLbl.Focus();
            document.IsVisible = false;
            FileImage.IsVisible = false;
            searchTickets.IsVisible = false;
            docLbl.IsVisible = false;
            Back.IsVisible = false;
        }

        private void GoToMenu(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            Application.Current.MainPage = main;
        }
    }
}