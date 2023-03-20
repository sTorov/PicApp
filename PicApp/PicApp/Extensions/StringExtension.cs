using System;
using System.Collections.Generic;
using System.Text;

namespace PicApp.Extensions
{
    public static class StringExtension
    {
        public static bool IsPicture(this string str) 
        {
            str = str.ToLower();
            var formats = new[] { ".jpg", ".png", ".jpeg", ".bmp", ".svg" };

            foreach ( var format in formats ) 
                if(str.EndsWith(format))
                    return true;
            return false;
        }
    }
}
