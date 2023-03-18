using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp.Pages
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            string pin = Preferences.Get("PIN", null);
            if (pin != null)
                titleLabel.Text = pin;

            base.OnAppearing();
        }

        private void CheckButton_Clicked(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(pinEntry.Text) && pinEntry.Text.Length == 4)
            {
                Preferences.Set("PIN", pinEntry.Text);
                titleLabel.Text = pinEntry.Text;
            }
            else
                titleLabel.Text = "Введите корректные данные!";
        }

        private void NumberButton_Clicked(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            pinEntry.Text += btn.Text;
        }

        private void RemoveButton_Clicked(object sender, EventArgs e)
        {           
            if(!string.IsNullOrEmpty(pinEntry.Text))
                pinEntry.Text = pinEntry.Text.Substring(0, pinEntry.Text.Length - 1);
        }
    }
}