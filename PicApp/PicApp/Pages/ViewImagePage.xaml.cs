using PicApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PicApp.Pages
{
    public partial class ViewImagePage : ContentPage
    {
        public Picture Picture { get; set; }

        public ViewImagePage(Picture picture)
        {
            InitializeComponent();
            Picture = picture;

            BindingContext = this;
        }
    }
}