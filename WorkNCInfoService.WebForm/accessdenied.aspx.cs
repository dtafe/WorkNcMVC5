using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WorkNCInfoService.WebForm
{
    public partial class accessdenied : WorkNCPortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            InitLanguage();
            lblAccessDenided.Text = GetResource("lblAccessDenided");
        }
    }
}