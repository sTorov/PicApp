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
        private const string NEW_PASS_MESS = "Придумайте 4-х значный пароль";
        private const string ENTER_PASS_MESS = "Введите пароль для входа";
        private const string REPEAT_PASS_MESS = "Подтвердите пароль";
        private const string INCORRECT_DATA_INFO = "Введите корректные данные!";
        private const string WRONG_PASS_INFO = "Неверный пароль!";
        private const string REMOVE_PASS_INFO = "Пароль сброшен!";

        private string _pin; 

        public MainPage()
        {
            InitializeComponent();            
        }

        protected override void OnAppearing()
        {
            pinEntry.Text = null;
            messageLabel.Text = null;
            _pin = Preferences.Get("PIN", null);
            titleLabel.Text = _pin == null ? NEW_PASS_MESS : ENTER_PASS_MESS;

            base.OnAppearing();
        }

        private async void CheckButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(pinEntry.Text) || pinEntry.Text.Length != 4)
            {
                messageLabel.Text = INCORRECT_DATA_INFO;
                return;
            }
            
            if (_pin == null)
            {
                _pin = pinEntry.Text;
                pinEntry.Text = null;
                titleLabel.Text = REPEAT_PASS_MESS;
            }
            else if (_pin == pinEntry.Text)
            {
                if(Preferences.Get("PIN", null) == null) Preferences.Set("PIN", pinEntry.Text);
                
                await Navigation.PushAsync(new ImagesPage());
                titleLabel.Text = ENTER_PASS_MESS;
            }
            else
            {
                pinEntry.Text = null;
                messageLabel.Text = WRONG_PASS_INFO;
            }
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

        private async void ClearPassButton_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert(null, "Сбросить пароль?", "Yes", "No");

            if(answer)
            {
                Preferences.Remove("PIN");
                _pin = null;
                pinEntry.Text = null;
                titleLabel.Text = NEW_PASS_MESS;
                messageLabel.Text = REMOVE_PASS_INFO;
            }
        }

        private void PinEntry_TextChanged(object sender, TextChangedEventArgs e) => messageLabel.Text = null;
    }
}