using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MagicConch.Helper
{
    internal static class LoadFontHelper
    {
        /// <summary>
        /// App Resourece에 Font를 Key 이름과 함께 등록합니다.
        /// </summary>
        /// <param name="fontPath"></param>
        /// <param name="fontName"></param>
        /// <returns></returns>
        public static async Task LoadFont(string fontPath, string fontName)
        {
            await FontFamily.LoadFontAsync(fontPath);

            var font = new FontFamily(fontPath);
            Application.Current.Resources[fontName] = font;
        }
    }
}
