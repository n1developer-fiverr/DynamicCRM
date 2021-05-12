using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DCrm.ViewModels;
using Xamarin.Forms;

namespace DCrm
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ((MainPageViewModel)BindingContext).Navigation = Navigation;

        }


        private async void ClickGestureRecognizer_OnClicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            //Application.Current.MainPage.DisplayAlert("Title", "sasas", "OK");
        }
    }
}
