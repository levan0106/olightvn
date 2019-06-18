using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace olightvn.Common
{
    public class WebConfigurations
    {
       
        private static string GetAppSetting(string str)
        {
            return System.Configuration.ConfigurationManager.AppSettings.Get(str);
        }
        public static string KeySecure
        {
            get { return GeneralFuncs.GetSettingByKey("KeySecure"); }
        }
        public static string ImageFullPath
        {
            get { return GeneralFuncs.GetSettingByKey("ImageFullPath"); }
        }
        public static string ThumbPath
        {
            get { return GeneralFuncs.GetSettingByKey("ImageThumbPath"); }
        }
        public static string ThumbPathDefault
        {
            get { return ThumbPath + "image.jpg"; }
        }
        public static string MapPath
        {
            get { return GeneralFuncs.GetSettingByKey("MapPath"); }
        }
        public static string BannerPath
        {
            get { return GeneralFuncs.GetSettingByKey("BannerPath"); }
        }
        public static string SliderPath
        {
            get { return GeneralFuncs.GetSettingByKey("ImageSlidePath"); }
        }
        public static int DefaultPageSize
        {
            get { return int.Parse(GeneralFuncs.GetSettingByKey("DefaultPageSize")); }
        }
        public static int SearchPageSize
        {
            get { return int.Parse(GeneralFuncs.GetSettingByKey("SearchPageSize")); }
        }
        public static ImageSize ImageFullSize
        {
            get
            {
                return new ImageSize().Get("ImageFullSize");
            }
        }
        public static ImageSize ImageThumbSize
        {
            get
            {
                return new ImageSize().Get("ImageThumbSize");
            }
        }
        public static ImageSize ImageSlideSize
        {
            get
            {
                return new ImageSize().Get("ImageSlideSize");
            }
        }
        public static string UrlFormat
        {
            get
            {
                return GetAppSetting("URLFormat");
            }
        }
    }
    public class ImageSize
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public ImageSize Get(string imageSize)
        {
            string size = GeneralFuncs.GetSettingByKey(imageSize);
            if (size != null)
            {
                string[] sizes = size.Split(',');
                return new ImageSize { Width = int.Parse(sizes[0]), Height = int.Parse(sizes[1]) };
            }
            return new ImageSize() { Width = 960, Height = 600 };
        }
    }
}