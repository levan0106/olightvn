using System;
using System.Collections.Generic;
using System.Linq;
using olightvn.Models;
using System.Web;
using T.Core.Common;

namespace olightvn.Common
{
    public static class SessionManager
    {
        public static int SiteId
        {
            get
            {
                if (HttpContext.Current.Session["SiteId"] == null)
                {
                    HttpContext.Current.Session["SiteId"] = 103;
                }
                return (int)HttpContext.Current.Session["SiteId"];
            }
        }

        public static Site CurrentSite
        {
            get
            {
                if (HttpContext.Current.Session["CurrentSite"] == null)
                {
                    HttpContext.Current.Session["CurrentSite"] = new olightvn.Repositories.SiteRepository().GetInfo(SiteId);
                }
                return (Site)HttpContext.Current.Session["CurrentSite"];
            }
            set
            {
                HttpContext.Current.Session["CurrentSite"] = value;
            }
        }
        public static Contact ContactInfo
        {
            get
            {
                if (HttpContext.Current.Session["ContactInfo"] == null)
                {
                    HttpContext.Current.Session["ContactInfo"] = new olightvn.Repositories.ContactRepository().GetInfo(SiteId);
                }
                return (Contact)HttpContext.Current.Session["ContactInfo"];
            }
        }
        public static string UserPermissions
        {
            get
            {
                if (HttpContext.Current.Session[Constant.USER_PERMISSIONS] == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Session[Constant.USER_PERMISSIONS].ToString();
            }
            set
            {
                HttpContext.Current.Session[Constant.USER_PERMISSIONS] = value;
            }
        }
        public static Dictionary<string, string> ExtendSettings
        {
            get
            {
                if (HttpContext.Current.Session[Constants.EXTEND_SETTING] == null)
                {
                    HttpContext.Current.Session[Constants.EXTEND_SETTING] = GeneralFuncs.GetExtendSettings();
                }
                return HttpContext.Current.Session[Constants.EXTEND_SETTING] as Dictionary<string, string>;
            }
        }
    }
}