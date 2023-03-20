using AutoMapper;
using PicApp.Models;
using PicApp.Pages;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp
{
    /// <summary>
    /// Основной класс приложения
    /// </summary>
    public partial class App : Application
    {
        public static IMapper Mapper { get; private set; }

        public App()
        {
            InitializeComponent();

            Mapper = GetMapper();

            MainPage = new NavigationPage(new MainPage());
        }

        /// <summary>
        /// Получение объекта-маппера
        /// </summary>
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
            MainPage.Navigation.PushAsync(new MainPage(true));
        }

        protected override void OnResume()
        {
        }
    }
}
