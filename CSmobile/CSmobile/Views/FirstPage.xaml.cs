using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CSmobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class FirstPage : ContentPage
	{
		public FirstPage ()
		{
			InitializeComponent ();
            //RotateElement();
        }
        private async Task RotateElement()
        {
            while (true)
            {
                await img.RotateTo(360, 1500);
                await img.RotateTo(0, 0);                
            }
        }
	}
}