using AutoMapper;
using PicApp.Models;
using PicApp.Pages;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp
{
    public partial class App : Application
    {
        public static IMapper Mapper { get; private set; }

        private NavigationPage _navigationPage;

        public App()
        {
            InitializeComponent();

            Mapper = GetMapper();
            _navigationPage = new NavigationPage(new MainPage());

            MainPage = _navigationPage;
        }

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(conf =>
            {
                conf.CreateMap<FileInfo, Picture>()
                    .ForMember("FullPath", opt => opt.MapFrom(p => p.FullName))
                    .ForMember("CreateTime", opt => opt.MapFrom(p => p.CreationTime));
            });

            return config.CreateMapper();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            _navigationPage.CurrentPage.Navigation.PushAsync(new MainPage(true));
        }

        protected override void OnResume()
        {
        }
    }
}
