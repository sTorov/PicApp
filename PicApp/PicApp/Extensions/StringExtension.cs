using System;
using System.Collections.Generic;
using System.Text;

namespace PicApp.Extensions
{
    /// <summary>
    /// Расширения для строк
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Метод для проверки имени полученного файла (является ли файл изображением)
        /// </summary>
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
