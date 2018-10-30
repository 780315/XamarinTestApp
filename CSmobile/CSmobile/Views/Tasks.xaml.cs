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
    public partial class Tasks : ContentPage
    {
        public Tasks()
        {
            InitializeComponent();
            datePicker.MinimumDate = DateTime.Now;
            datePicker.MaximumDate = DateTime.MaxValue;
            datePicker.Date = DateTime.Now;
        }
        public List<Models.Tasks> list { get; set; }
        public int results { get; set; }

        async void CreateTask(object sender, EventArgs e)
        {
            Models.Tasks tasks = new Models.Tasks(0, title.Text, description.Text, "", 0, datePicker.Date.ToShortDateString(),0,0,0,0,0,true,0,"",0,DateTime.Today.ToShortDateString(),0,"",0) ;
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

        private void ClearFields()
        {
            title.Text = "";
            description.Text = "";
            datePicker.Date = DateTime.Now;
        }
    }
}