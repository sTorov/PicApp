using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;


namespace PicApp.Extensions
{
    internal class ArgbExtension : IMarkupExtension
    {
        public int R { get; set; }
        public int G { get; set; }
        public int B { get; set; }
        public int A { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider) =>
            Color.FromRgba(R, G, B, A);
            
    }
}
