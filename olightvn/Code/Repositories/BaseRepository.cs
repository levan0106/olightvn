using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using T.Core.Infrastructure;
using System.Xml;
using olightvn.Common;

namespace olightvn.Repositories
{
    public class BaseRepository : BaseFactory
    {
            public BaseRepository()
            {
                if (base.ConnectionString == null)
                {
                    //string host = HttpContext.Current.Request.Url.Host;
                    XmlDocument xdoc = new XmlDocument();
                    string path = HttpContext.Current.Server.MapPath("\\") + "App_Data\\connectionstring.xml";
                    if (System.IO.File.Exists(path))
                    {
                        XmlNode node = default(XmlNode);
                        xdoc.Load(path);
                        node = xdoc.SelectSingleNode("connections/connection[@SiteId='" + SessionManager.SiteId + "']");
                        //SessionManager.SiteId = int.Parse(node.Attributes["SiteId"].Value);
                        string connection = node.SelectSingleNode("connectionString[@name='DB']").Attributes["connectionString"].Value;
                        base.ConnectionString = connection;
                    }
                }
            }
            
            
    }
    public static class Parameters
    {
        public static string AddParametersDefaul(this string sql, bool firstParam = false, int? siteId = null)
        {
            string prefix = firstParam ? string.Empty : ",";
            sql += prefix + " @SiteId=" + (siteId == null ? SessionManager.SiteId : siteId);
            return sql;
        }
    }
}