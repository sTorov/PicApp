using PicApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
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
            BindingContext = this;

            base.OnAppearing();
        }
    }
}