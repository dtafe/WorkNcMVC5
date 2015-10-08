using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm
{
    public partial class Login : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            txtUserName.Focus();
            try
            {
                InitLanguage();
                Initialize();
                HyperLink hpl = new HyperLink();
                LinkButton lbtn = new LinkButton();
                hpl = (HyperLink)Master.FindControl("hplTitle");
                hpl.Text = btnLogin.Text;

            }
            catch (Exception ex)
            {
                logger.Error("Error Page_Load", ex);
                RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("Title_Error.Text"), ex.Message) + "\");");
            }
        }

        private void Initialize()
        {
            lblUserName.Text = GetResource("lblUserName");
            lblPassWord.Text = GetResource("lblPassWord");
            cbRememberMe.Text = GetResource("cbRememberMe");
            btnLogin.Text = GetResource("btnLogin");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (Membership.ValidateUser(txtUserName.Text, txtPassWord.Text))
                {
                    if (!string.IsNullOrEmpty(UserPermission.GetWebPermission(txtUserName.Text)) || Common.isAdminAccount(txtUserName.Text))
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, cbRememberMe.Checked ? true : false);
                    else
                    {
                        lblLoginFail.Visible = true;
                        lblLoginFail.Text = GetResource("msPermission");
                        return;
                    }
                }
                else
                {
                    lblLoginFail.Visible = true;
                    lblLoginFail.Text = GetResource("LoginFail");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Login", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error.Text"), ex.Message) + "\");", true);
            }
        }
    }
}