using olightvn.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Script.Serialization;
using T.Core.Common;

namespace olightvn.Common
{
    public static class Helpers
    {
        public static IHtmlString BrandingTheme()
        {
            string path = string.Format("/Content/themes/{0}/theme.css", SessionManager.CurrentSite.ThemeName);
            DateTime lastWriteTime = File.GetLastWriteTime(HttpContext.Current.Server.MapPath("\\") + path);
            string html = string.Format("<link href=\"{0}?v={1}\" rel=\"stylesheet\">", path,  lastWriteTime.Ticks);
            return new HtmlString(html);
        }

        public static IHtmlString RenderLogo(string cssClass = "")
        {         
            string path = string.Format("/Content/themes/{0}/logo.png", SessionManager.CurrentSite.ThemeName);
            string html = string.Format("<img src=\"{0}\"  class=\"{1}\" />", path, cssClass);
            return new HtmlString(html);            
        }

        public static IHtmlString RenderFavicon()
        {
            string path = string.Format("/Content/themes/{0}/favicon.ico", SessionManager.CurrentSite.ThemeName);
            string html = string.Format("<link rel='shortcut icon' type='image/ico' href='{0}'/>", path);
            return new HtmlString(html);   
        }

        public static string GetCurrentPath(this string path)
        {
            return path.Replace("Views/", string.Format("Views/{0}/", SessionManager.CurrentSite.ThemeName));
        }
        public static bool CheckExistsFile(string filePath)
        {
            string mapPath = HttpContext.Current.Server.MapPath(filePath);
            return System.IO.File.Exists(mapPath);
        }
      
    }
    public static partial class Converter
    {

        public static T JsonConvertToObject<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str);
        }
        public static List<T> JsonConvertToObjectList<T>(this string str)
        {
            return JsonConvert.DeserializeObject<List<T>>(str);
        }
        public static JObject ToJsonObject(this string str)
        {
            if (str == null || str == string.Empty)
                return null;
            return JObject.Parse(str);
        }
        public static string ToJsonObject(this IEnumerable<object> list)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(list);
            return json;
        }
        public static string ToJsonObject(this object obj)
        {
            var jsonSerialiser = new JavaScriptSerializer();
            var json = jsonSerialiser.Serialize(obj);
            return json;
        }
    }
    public static class PartialCustomExtensions
    {
        public static MvcHtmlString PartialCustom(this HtmlHelper helper, string partialViewName)
        {
            return PartialCustom(helper, partialViewName, null);
        }
        public static MvcHtmlString PartialCustom(this HtmlHelper helper, string partialViewName, object model)
        {
            return PartialCustom(helper, partialViewName, model, null);
        }
        public static MvcHtmlString PartialCustom(this HtmlHelper helper, string partialViewName, object model, ViewDataDictionary viewData)
        {
            var response = helper.ViewContext.HttpContext.Response;
            string newPartialViewName = string.Empty;
            if (partialViewName.Contains(".cshtml"))
            {
                newPartialViewName = partialViewName.GetCurrentPath();
                if (!Helpers.CheckExistsFile(newPartialViewName))
                {
                    newPartialViewName = partialViewName;
                }
            }
            else
            {
                newPartialViewName = helper.ViewDataContainer.ToString().GetCurrentPath() + partialViewName + ".cshtml";
                if (!System.IO.File.Exists(newPartialViewName))
                {
                    newPartialViewName = partialViewName;
                }
            }

            //Add the contents of the partial view to the string builder.
           return helper.Partial(newPartialViewName,model,viewData);
        }
        public static List<FileModel> ConvertToFiles(this string fileCollections)
        {
            List<FileModel> files = fileCollections.JsonConvertToObjectList<FileModel>();
            if (files != null)
            {
                foreach (FileModel file in files)
                {
                    if (file.Base64.IsNotNullOrEmpty())
                    {
                        string fileBase64 = file.Base64;
                        int index = fileBase64.IndexOf("base64");
                        file.Base64 = fileBase64.Substring(index + 7);
                    }
                }
            }
            return files;
        }
    }
}