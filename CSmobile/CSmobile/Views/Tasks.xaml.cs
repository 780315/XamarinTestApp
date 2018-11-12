﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSmobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Tasks : ContentPage
    {
        public Tasks()
        {
            InitializeComponent();
            datePicker.MinimumDate = DateTime.Now;
            datePicker.MaximumDate = DateTime.MaxValue;
            datePicker.Date = DateTime.Now;
            LoadList();            
        }
        public List<Models.Tasks> list { get; set; }
        public int results { get; set; }
        public int id { get; set; }
        public Models.Tasks tasks { get; set; }

        async void CreateTask(object sender, EventArgs e)
        {
            Models.Tasks tasks = new Models.Tasks(0, title.Text, description.Text, "", 0, datePicker.Date.ToShortDateString(), 0, 0, 0, 0, 0, true, 0, "", 0, DateTime.Today.ToShortDateString(), 0, "", 0);
            await App.ApiServices.PostTasks(tasks);
            if (App.ApiServices.taskPost == true)
            {
                await DisplayAlert("Task", "Task Created", "Ok");
                ClearFields();
            }
            else
            {
                await DisplayAlert("Task", "Task Not Created", "Ok");
            }
        }

        async void EditTaskSave(object sender, EventArgs e)
        {
            Models.Tasks tasksVal = new Models.Tasks(tasks.id, title.Text, description.Text, tasks.descriptionHtml, tasks.assignedUserId, datePicker.Date.ToShortDateString(), tasks.deadlineType,
            tasks.clientId, tasks.contactPersonId, tasks.status, tasks.reminderInterval, tasks.inCalendar, tasks.parentTask, tasks.fromDate, tasks.createdBy, tasks.createdOn, 0, "", tasks.version);
            await App.ApiServices.PutTask(tasksVal);
            if (App.ApiServices.taskPut == true)
            {
                await DisplayAlert("Task", "Task Edited", "Ok");
                ClearFields();
                HideCreateView();
                LoadList();
            }
            else
            {
                await DisplayAlert("Task", "Task Not Edited", "Ok");
                HideCreateView();
                LoadList();
            }
        }

        private void ClearFields()
        {
            title.Text = "";
            description.Text = "";
            datePicker.Date = DateTime.Now;
        }

        private void CreateView_Clicked(object sender, EventArgs e)
        {
            ShowCreateView();
        }
        private void ShowCreateView()
        {
            Results.Text = "";
            lbltask.IsVisible = false;
            listview.IsVisible = false;
            listview.ItemsSource = null;
            CreateView.IsVisible = false;
            createLayout.IsVisible = true;
            Create.IsVisible = true;
            Label1.IsVisible = true;
            editLbl.IsVisible = false;
        }
        private void HideCreateView()
        {
            Results.Text = "";
            lbltask.IsVisible = true;
            listview.IsVisible = true;
            listview.ItemsSource = null;
            CreateView.IsVisible = true;
            createLayout.IsVisible = false;
            Create.IsVisible = false;
            Edit.IsVisible = false;
        }

        private void listview_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (list.Count != 0)
            {
                var value = listview.SelectedItem;
                Models.Tasks tasks = (Models.Tasks)value;
                list = new List<Models.Tasks>();
                list.Add(tasks);
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

        private async void LoadList()
        {
            if (createLayout.IsVisible == false)
            {
                listview.ItemsSource = null;
                listShowAll.ItemsSource = null;
                await App.ApiServices.GetTask();
                list = App.ApiServices.Tasks;
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

        private void EditTask(object sender, EventArgs e)
        {
            var value = listview.SelectedItem;
            tasks = (Models.Tasks)value;
            title.Text = tasks.title;
            description.Text = tasks.description;
            datePicker.Date = Convert.ToDateTime(tasks.createdOn);
            //id = tasks.id;
            ShowCreateView();
            listShowAll.ItemsSource = null;
            listShowAll.IsVisible = false;
            Create.IsVisible = false;
            Edit.IsVisible = true;
            Label1.IsVisible = false;
            editLbl.IsVisible = true;
            editLbl.Focus();
            scrollView.ScrollToAsync(createLayout, ScrollToPosition.Start, false);
        }

        private void GoToMenu(object sender, EventArgs e)
        {
            MainPage main = new MainPage();
            Application.Current.MainPage = main;
        }
    }
}