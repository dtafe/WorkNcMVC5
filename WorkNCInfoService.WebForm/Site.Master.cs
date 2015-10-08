using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm
{
    public partial class SiteMaster : MasterPage
    {

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillDropDownCompany();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["CurrentLanguage"];
            if (!IsPostBack && cookie != null && cookie.Value != null)
            {
                if (cookie.Value.IndexOf("en-") >= 0)
                {
                    imgBtnEn.Enabled = false;
                    imgBtnJp.Enabled = true;
                }
                else
                {
                    imgBtnEn.Enabled = true;
                    imgBtnJp.Enabled = false;
                }
            }

            HttpCookie cookie1 = Request.Cookies["CurrentLanguage"];
            if (cookie1 != null && cookie1.Value != null)
            {
                Page.UICulture = cookie1.Value;
            }
            imgBanner.ImageUrl = "~/Images/banner.jpg";
            Initialize();
            InitializeLinkTitle();

            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                lblUserName.Visible = true;
                lblUserName.Text = HttpContext.Current.User.Identity.Name;
                lBtnLogout.Visible = true;
                lBtnLogin.Visible = false;
                panelMenuAbout.Visible = panelHome.Visible = true;  

                bool IsSystemAccount = Common.isAdminAccount(HttpContext.Current.User.Identity.Name);
                string strWebPermission = "";
                if (!IsSystemAccount)
                {
                    UserPermission objPermission = UserPermission.GetUserPermission(HttpContext.Current.User.Identity.Name);
                    if (objPermission != null)
                    {
                        strWebPermission = objPermission.WebPermission;

                        string page = Path.GetFileNameWithoutExtension(Request.Path).ToLower();
                        if (page != "default" && page != "login" && page != "accessdenied")
                        {
                            if (string.IsNullOrEmpty(strWebPermission) || strWebPermission == Constant.PERMISSION_MEMBER)
                            {
                                if (page == "register" || page == "manageuser" || page == "mstcompany")
                                    Response.Redirect("~/accessdenied.aspx");
                            }
                            else if (strWebPermission == Constant.PERMISSION_CHIEF)
                            {
                                if (page == "mstcompany")
                                    Response.Redirect("~/accessdenied.aspx");
                            }

                        }
                    }
                }
                if (IsSystemAccount || strWebPermission == Constant.PERMISSION_CHIEF)  
                {
                    panelMenuWorkZone.Visible = panelMenuMaster.Visible = panelMenuUser.Visible = true;
                    panelRegisterCompany.Visible = false;
                    if (IsSystemAccount)
                        panelRegisterCompany.Visible = true;
                    else if (cboCompanyName.Items.Count == 0) //remove company 
                    {
                        panelMenuWorkZone.Visible = panelMenuMaster.Visible = panelMenuUser.Visible = false;
                    }
                }
                else if (strWebPermission == Constant.PERMISSION_MEMBER)
                {
                    panelMenuWorkZone.Visible = panelMenuMaster.Visible = true;
                    panelMenuUser.Visible = false;
                    if (cboCompanyName.Items.Count == 0) //remove company 
                    {
                        panelMenuWorkZone.Visible = panelMenuMaster.Visible = false;
                    }
                }
                else
                {
                    panelMenuWorkZone.Visible = panelMenuMaster.Visible = panelMenuUser.Visible = false;
                }
            }
            else
            {
                lblUserName.Text = string.Empty;
                lblUserName.Visible = false;
                lBtnLogout.Visible = false;
                lBtnLogin.Visible = true;
                panelHome.Visible = panelMenuWorkZone.Visible = panelMenuMaster.Visible = panelMenuUser.Visible = panelMenuAbout.Visible =  false;
            }
        }

        private void Initialize()
        {
            lBtnLogin.Text = Common.GetResourceString("lBtnLogin");
            lBtnLogout.Text = Common.GetResourceString("lBtnLogout");
            lblMenuHome.Text = Common.GetResourceString("lblMenuHome");

            lblMenuWorkZone.Text = Common.GetResourceString("lblWorkZone");
            lblMenuMaster.Text = Common.GetResourceString("lblMenuMaster");
            lblUser.Text = Common.GetResourceString("lblUser");
            lblRegisterCompany.Text = Common.GetResourceString("lblRegisterCompany");
            lblMenuUser.Text = Common.GetResourceString("lblMenuUser");
            lblRegisterUser.Text = Common.GetResourceString("lblRegisterUser");
            lblMenuAbout.Text = Common.GetResourceString("lblMenuAbout");
            lblMenuFactory.Text = Common.GetResourceString("lblMenuFactory");
            lblMenuMachine.Text = Common.GetResourceString("lblMenuMachine");
            lblMenuContact.Text = Common.GetResourceString("lblMenuContact");
          
        }
        
        private void InitializeLinkTitle()
        {
            string title = Path.GetFileNameWithoutExtension(Request.Path).ToLower();
            if (title == "default") hplTitle.Text = lblMenuHome.Text;
            if (title == "mstfactory") hplTitle.Text = lblMenuFactory.Text;
            if (title == "mstmachine") hplTitle.Text = lblMenuMachine.Text;
            if (title == "manageuser") hplTitle.Text = lblMenuUser.Text;
            if (title == "upload") hplTitle.Text = Common.GetResourceString("lblMenuUpload");//lblMenuUpload.Text;
            if (title == "contact") hplTitle.Text = lblMenuContact.Text;
            if (title == "mstcompany") hplTitle.Text = lblRegisterCompany.Text;
            if (title == "register") hplTitle.Text = lblRegisterUser.Text;
            if (title == "changepassword") hplTitle.Text = Common.GetResourceString("ChangePassWord");
            if (title == "accessdenied") hplTitle.Text = string.Empty;
            if (title == "workzonelist") hplTitle.Text = Common.GetResourceString("lblWorkZone");
            if (title == "workzonelistdetail") hplTitle.Text = Common.GetResourceString("lblWorkZoneDetail");
            hplTitle.NavigateUrl = Request.Url.AbsoluteUri;
        }

        protected void imgBtnJp_Click(object sender, ImageClickEventArgs e)
        {
            
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "ja-JP";
            Calendar.flag = "ja";
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void imgBtnEn_Click(object sender, ImageClickEventArgs e)
        {
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "en-US";
            Calendar.flag = "en";
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        protected void lBtnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            DeleteCompanyCookies();
            Response.Redirect("~/default.aspx");
        }

        protected void lBtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/login.aspx");
        }

        #region Fill Company 
        private void DeleteCompanyCookies()
        {
            //delete cookie OfficeCd
            if (Request.Cookies["CurrentCompany"] != null)
            {
                HttpCookie cookie = Request.Cookies["CurrentCompany"];
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }
        }
        private void FillDropDownCompany()
        {
            //make new table Permission 
            cboCompanyName.Items.Clear();
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var listCompany = UserPermission.GetListCompanyFromUser(HttpContext.Current.User.Identity.Name);
                foreach (Company i in listCompany)
                {
                    cboCompanyName.Items.Add(new ListItem(i.CompanyName, i.CompanyId.ToString()));
                }
                if (cboCompanyName.Items.Count == 0)
                { 
                     panelMenuWorkZone.Visible = panelMenuMaster.Visible = panelMenuUser.Visible = false;
                }
                HttpCookie cookie = Request.Cookies["CurrentCompany"];
                if (cookie != null)
                {
                    cboCompanyName.SelectedValue = cookie.Value;
                }
            }
        }
        protected void cboCompanyName_SelectedIndexChanged(object sender, EventArgs e)
        {
            HttpCookie cookie = Request.Cookies["CurrentCompany"];
            if (cookie == null)
                cookie = new HttpCookie("CurrentCompany");

            cookie.Value = cboCompanyName.SelectedValue;
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
        }

        #endregion 
    }
}