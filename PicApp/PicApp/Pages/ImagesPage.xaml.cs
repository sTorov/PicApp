using PicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace PicApp.Pages
{
    public partial class ImagesPage : ContentPage
    {
        public ObservableCollection<Picture> Pictures { get; set; }

        public ImagesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            //@"/storage/emulated/0/DCIM/Camera" @"/storage/emulated/0/Pictures"
            var fileList = new DirectoryInfo(@"/storage/emulated/0/Pictures").GetFiles();
            var pictureList = App.Mapper.Map<Picture[]>(fileList);

            Pictures = new ObservableCollection<Picture>(pictureList);
            //BindingContext = this;

            CreateGallery();

            base.OnAppearing();
        }

        private void CreateGallery()
        {
            var grid = new Grid
            {
                ColumnDefinitions = 
                { 
                    new ColumnDefinition{ Width = GridLength.Star },
                    new ColumnDefinition{ Width = GridLength.Star },
                }
            };

            var countRows = Pictures.Count / grid.ColumnDefinitions.Count;

            for(int i = 1; i < countRows + 1; i++)
                grid.RowDefinitions.Add(new RowDefinition {  Height = GridLength.Star });

            int currentColumn = 0;
            int currentRow = 0;

            foreach(Picture picture in Pictures)
            {
                var card = new StackLayout
                {
                    
                    Children =
                    {
                        new Image{ Source = picture.FullPath, HeightRequest = 150 },
                        new Label{ Text = picture.Name }
                    }
                };
                grid.Children.Add(card, currentColumn, currentRow);

                var recognizer = new TapGestureRecognizer();
                recognizer.Tapped += async (sender, e) => await Navigation.PushAsync(new ViewImagePage(picture));
                card.GestureRecognizers.Add(recognizer);

                currentColumn++;
                if(currentColumn == 2)
                {
                    currentRow++;
                    currentColumn = 0;
                }
            }

            scrollView.Content = grid;
        }
    }
}