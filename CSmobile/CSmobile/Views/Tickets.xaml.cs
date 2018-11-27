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
using Telerik.XamarinForms.DataControls.ListView;

namespace CSmobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tickets : ContentPage
    {
        public int ticketId { get; set; }
        public List<Ticket> list { get; set; }      
        private MediaFile _mediaFile;
        public Ticket ticket { get; set; }

        public string Ttitle { get; set; }
        public string Tdescription { get; set; }
        public int results { get; set; }
        public long img { get; set; }
        public string imgName { get; set; }
        public string path { get; set; }
        public Image image = new Image();

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
                if (FileImage.Children.Contains(image) == true)
                {
                    await App.ApiServices.DocumentCommit(path, imgName, App.ApiServices.id.ToString(), "Ticket");
                }
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
                if (FileImage.Children.Contains(image) == true)
                {
                    await App.ApiServices.DocumentCommit(path, imgName, ticket.id.ToString(), "Ticket");
                }
                await DisplayAlert("Ticket", "Ticket Edited", "Ok");
            }
            else
            {
                await DisplayAlert("Ticket", "Ticket Not Edited", "Ok");
            }
            HideCreateView();
            Search.Text = string.Empty;
            Search.IsVisible = true;
        }


        private void ClearFields()
        {
            title.Text = string.Empty;
            description.Text = string.Empty;           
            FileImage.Children.Clear();
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

            var source = ImageSource.FromStream(() =>
            {
                return _mediaFile.GetStream();
            });
            
            image = new Image { Source = source, WidthRequest = 50, HeightRequest = 50};
            FileImage.Children.Add(image);
            
            using (var memoryStream = new MemoryStream())
            {
                _mediaFile.GetStream().CopyTo(memoryStream);
                img = memoryStream.Length;
            }
            path = Convert.ToString(_mediaFile.Path);

            imgName = path.Substring(path.LastIndexOf('/') + 1);

            await App.ApiServices.DocumentInit(path, imgName, "0", "Ticket");
        }

        private void Back_Clicked(object sender, EventArgs e)
        {
            createLayout.IsVisible = false;
            Create.IsVisible = false;
            lblticket.IsVisible = true;
            Search.IsVisible = true;
            listview.IsVisible = true;
            CreateView.IsVisible = true;
        }

        async void titleSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search.Text = e.NewTextValue;
            string searchFilter = Search.Text;
            if (searchFilter.Length >= 3)
            {
                BusyIndicator.IsVisible = true;
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetTickets(searchFilter);
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
            if (string.IsNullOrEmpty(searchFilter))
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                Results.Text = "";
            }
            BusyIndicator.IsVisible = false;
        }

        private void CreateView_Clicked(object sender, EventArgs e)
        {
            Results.Text = "";
            Search.Text = "";
            lblticket.IsVisible = false;
            Search.IsVisible = false;
            listview.IsVisible = false;
            listview.ItemsSource = null;
            CreateView.IsVisible = false;
            createLayout.IsVisible = true;
            Create.IsVisible = true;
            Label1.IsVisible = true;
            editLbl.IsVisible = false;
            ClearFields();
            titleLabel.Text = string.Empty;
            descriptionLabel.Text = string.Empty;
            //docLbl.IsVisible = true;

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
            scrollView.ScrollToAsync(scrollView, ScrollToPosition.Start, false);
            Label1.IsVisible = false;
            editLbl.IsVisible = true;
            editLbl.Focus();
            document.IsVisible = true;
            FileImage.IsVisible = true;
            Search.IsVisible = false;
            //docLbl.IsVisible = false;

        }

        private void GoToMenu(object sender, EventArgs e)
        {
            if (createLayout.IsVisible == false && listShowAll.IsVisible == false)
            {
                MainPage main = new MainPage();
                Application.Current.MainPage = main;
            }
            if (createLayout.IsVisible == false && listShowAll.IsVisible == true)
            {
                listview.IsVisible = true;
                Search.Text = string.Empty;
                listview.ItemsSource = App.ApiServices.Tickets;
                listShowAll.IsVisible = false;
                Results.IsVisible = true;
                if (listview.ItemsSource != null)
                {
                    var result = App.ApiServices.Tickets.Count();
                    Results.Text = "Results: " + result.ToString();
                }
            }
            if (createLayout.IsVisible == true)
            {
                HideCreateView();
                Search.Text = string.Empty;
                Search.IsVisible = true;
            }
        }

        private void LogOut(object sender, EventArgs e)
        {
            App.ApiServices.Logout();
        }

        private void editSwipe(object sender, ItemSwipeCompletedEventArgs e)
        {            
            if (e.Offset <= -70)
            {
                var value = e.Item;
                ticket = (Ticket)value;
                title.Text = ticket.title;
                description.Text = ticket.description;
                ShowCreateView();
                listShowAll.ItemsSource = null;
                listShowAll.IsVisible = false;
                Create.IsVisible = false;
                Edit.IsVisible = true;
                scrollView.ScrollToAsync(scrollView, ScrollToPosition.Start, false);
                Label1.IsVisible = false;
                editLbl.IsVisible = true;
                editLbl.Focus();
                document.IsVisible = true;
                FileImage.IsVisible = true;
                Search.IsVisible = false;
                titleLabel.IsVisible = true;
                descriptionLabel.IsVisible = true;
                //docLbl.IsVisible = false;
            }
            listview.EndItemSwipe();
        }
    }
}