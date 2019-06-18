using olightvn.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace olightvn
{
    public partial class keepsessionalive : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //int timeOut = GeneralFuncs.GetSettingByKey("TimeOut");

            if (User.Identity.IsAuthenticated)
            {
                // Refresh this page 60 seconds before session timeout, effectively resetting the session timeout counter.
                
                Configuration config = WebConfigurationManager.OpenWebConfiguration("~/Web.Config");
                SessionStateSection section = (SessionStateSection)config.GetSection("system.web/sessionState");
                int timeout = (int)section.Timeout.TotalMinutes * 60;
                idtimeout.Text = timeout.ToString();
                Label1.Text = (Session.Timeout * 60).ToString();
                MetaRefresh.Attributes["content"] = Convert.ToString(timeout - 60) + ";url=/KeepSessionAlive.aspx?q=" + DateTime.Now.Ticks;
            }
        }
    }
}