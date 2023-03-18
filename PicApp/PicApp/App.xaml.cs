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

        public App()
        {
            InitializeComponent();

            Mapper = GetMapper();

            MainPage = new NavigationPage(new MainPage());
        }

        private static IMapper GetMapper()
        {
            var config = new MapperConfiguration(conf =>
            {
                conf.CreateMap<FileInfo, Picture>()
                    .ForMember("FullPath", opt => opt.MapFrom(p => p.FullName));
            });

            return config.CreateMapper();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
