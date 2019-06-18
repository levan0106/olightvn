using olightvn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace olightvn.Common
{
    public static class Header
    {
        public static void LoadMetaTagWebsite(Page page, string Title, string Keywords, string Description, string Url, string ImagePath)
        {
            HtmlMeta metaKeywords = new HtmlMeta();
            metaKeywords.Name = "keywords";
            metaKeywords.Content = Keywords;
            page.Header.Controls.Add(metaKeywords);

            HtmlMeta metaDescription = new HtmlMeta();
            metaDescription.Name = "description";
            metaDescription.Content = Description;
            page.Header.Controls.Add(metaDescription);

            HtmlMeta tag = new HtmlMeta();
            tag.Attributes.Add("property", "og:title");
            tag.Content = Title;
            page.Header.Controls.Add(tag);

            HtmlMeta tag1 = new HtmlMeta();
            tag1.Attributes.Add("property", "og:description");
            tag1.Content = Description;
            page.Header.Controls.Add(tag1);

            HtmlMeta tagurl = new HtmlMeta();
            tagurl.Attributes.Add("property", "og:url");
            tagurl.Content = Url;
            page.Header.Controls.Add(tagurl);

            HtmlMeta tagimg = new HtmlMeta();
            tagimg.Attributes.Add("property", "og:image");
            tagimg.Content = ImagePath;
            page.Header.Controls.Add(tagimg);
        }
        public static void BindMetaTags(string requestUrl, string baseUrl)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(WebConfigurations.UrlFormat);
            var v = regex.Match(requestUrl);
            string id = v.Groups[1].ToString();
            if (string.IsNullOrEmpty(id))
                return;
            //var url = HttpContext.Request.Url.Authority;
            Product data = GetDataMeta(int.Parse(id));
            SessionManager.CurrentSite = SessionManager.CurrentSite;
            SessionManager.CurrentSite.Title = data.Name;
            SessionManager.CurrentSite.Description = data.Description;
            SessionManager.CurrentSite.Url = baseUrl + requestUrl;
            SessionManager.CurrentSite.Image = baseUrl + WebConfigurations.ImageFullPath + data.Thumbnail;

        }
        private static Product GetDataMeta(int id)
        {
            Product data = new olightvn.Repositories.ProductRepository().GetInfo(id);
            return data;
        }
        
    }
}