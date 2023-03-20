using PicApp.Extensions;
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

        private StackLayout _selectedItem;

        public ImagesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            _selectedItem = null;

            #region SimplePath

            //@"/storage/emulated/0/DCIM/Camera" @"/storage/emulated/0/Pictures" @"/storage/emulated/0/Pictures/Screenshots"

            #endregion

            var fileList = new DirectoryInfo(@"/storage/emulated/0/Pictures/Screenshots").GetFiles();
            var pictureList = App.Mapper.Map<Picture[]>(fileList);

            Pictures = new ObservableCollection<Picture>(pictureList.Where(p => p.Name.IsPicture()));

            CreateGallery();
            Pictures.CollectionChanged += (sender, e) => CreateGallery();

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

            var countRows = Pictures.Count / grid.ColumnDefinitions.Count > 3 
                ? Pictures.Count / grid.ColumnDefinitions.Count : 3;

            for(int i = 0; i < countRows; i++)
                grid.RowDefinitions.Add(new RowDefinition {  Height = GridLength.Star });

            int currentColumn = 0;
            int currentRow = 0;

            foreach (Picture picture in Pictures)
            {
                var card = new StackLayout
                {
                    Children =
                    {
                        new Image { Source = picture.FullPath, HeightRequest = 150 },
                        new Label { Text = picture.Name }
                    },
                    Style = (Style)App.Current.Resources["StackLayoutVisualState"],
                    Padding = new Thickness(10)
                };
                grid.Children.Add(card, currentColumn, currentRow);

                var recognizer = new TapGestureRecognizer();
                recognizer.Tapped += TappedImage;
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

        private void TappedImage(object sender, EventArgs e)
        {
            var stack = (StackLayout)sender;
            if(stack.Equals(_selectedItem))
                return;

            VisualStateManager.GoToState(stack, "Selected");
            if (_selectedItem != null) VisualStateManager.GoToState(_selectedItem, "Normal");
            _selectedItem = stack;
        }

        private async void Go(object sender, EventArgs e)
        {
            if (_selectedItem == null)
                return;

            var imgPath = new ImageSourceConverter().ConvertToInvariantString((_selectedItem.Children[0] as Image).Source);
            var picture = App.Mapper.Map<Picture>(new FileInfo(imgPath));

            await Navigation.PushAsync(new ViewImagePage(picture));
        }

        private void Delete(object sender, EventArgs e)
        {
            if (_selectedItem == null)
                return;

            var imgPath = new ImageSourceConverter().ConvertToInvariantString((_selectedItem.Children[0] as Image).Source);
            if (File.Exists(imgPath))
                File.Delete(imgPath);
            

            var deletedPic = App.Mapper.Map<Picture>(new FileInfo(imgPath));
            Pictures.Remove(deletedPic);
            _selectedItem = null;
        }
    }
}