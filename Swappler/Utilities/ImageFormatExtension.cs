using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace Swappler.Utilities
{
    public static class ImageFormatExtension
    {
        private static readonly Hashtable FormatExtensionNameMap = new Hashtable
        {
            {ImageFormat.Bmp, ".bmp"},
            {ImageFormat.Emf, ".emf"},
            {ImageFormat.Exif, ".exif"},
            {ImageFormat.Gif, ".gif"},
            {ImageFormat.Icon, ".ico"},
            {ImageFormat.Jpeg, ".jpeg"},
            {ImageFormat.Png, ".png"},
            {ImageFormat.Tiff, ".tiff"},
            {ImageFormat.Wmf, ".wmf"}
        };
        public static string ExtensionName(this ImageFormat imageFormat)
        {
            return FormatExtensionNameMap[imageFormat].ToString();
        }
    }
}