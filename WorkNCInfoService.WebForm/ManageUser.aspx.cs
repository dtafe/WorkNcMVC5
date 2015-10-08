using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Utilities;
using WorkNCInfoService.Domain;
using System.IO;

namespace WorkNCInfoService.WebForm
{
    public partial class ManageUser : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    InitLanguage();
                    Initialize();
                    Search();
                }
                catch (Exception ex)
                {

                    logger.Error("Error Page_Load", ex);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error.Text"), ex.Message) + "\");", true);
                }
            }
        }

        private void Search()
        {
            try
            {
                int companyCode = int.Parse(GetCompany());

                List<MembershipUser> listUser = new List<MembershipUser>();


                List<UserPermission> listAllPermisson = UserPermission.GetAll();
              
                foreach (MembershipUser m in Membership.GetAllUsers())
                {
                    if (Common.isAdminAccount(m.UserName))
                        continue;
                    UserPermission findUser = listAllPermisson.FirstOrDefault(p => p.Username == m.UserName); 
                    if(findUser == null || findUser.CompanyId == companyCode)
                    {
                        listUser.Add(m);
                    }
                }
                listUser = listUser.Where(l => (l.UserName.Contains(txtUserName.Text))).ToList();
                List<MembershipUser> onlineUser = new List<MembershipUser>();
                foreach (MembershipUser u in listUser)
                {
                    if (u.IsOnline) onlineUser.Add(u);
                }

                if (cbxOnlineUser.Checked)
                {
                    if (onlineUser.Count == 0)
                    {
                        lblNoRecord.Visible = true;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("UserName");
                        dt.Columns.Add("Email");
                        dt.Columns.Add("IsApproved");
                        DataRow r = dt.NewRow();
                        r["UserName"] = string.Empty;
                        r["Email"] = string.Empty;
                        r["IsApproved"] = true;
                        dt.Rows.Add(r);
                        grvUser.DataSource = dt;
                        grvUser.DataBind();
                        grvUser.Rows[0].Visible = false;
                    }
                    else
                    {
                        lblNoRecord.Visible = false;
                        grvUser.DataSource = onlineUser;
                        grvUser.DataBind();
                    }

                }
                else
                {
                    if (listUser.Count == 0)
                    {
                        lblNoRecord.Visible = true;
                        DataTable dt = new DataTable();
                        dt.Columns.Add("UserName");
                        dt.Columns.Add("Email");
                        dt.Columns.Add("IsApproved");
                        DataRow r = dt.NewRow();
                        r["UserName"] = string.Empty;
                        r["Email"] = string.Empty;
                        r["IsApproved"] = true;
                        dt.Rows.Add(r);

                        grvUser.DataSource = dt;
                        grvUser.DataBind();
                        grvUser.Rows[0].Visible = false;
                    }
                    else
                    {
                        lblNoRecord.Visible = false;
                        grvUser.DataSource = listUser;
                        grvUser.DataBind();
                    }
                }


                LinkButton lBtn = new LinkButton();
                LinkButton lBtnResetPass = new LinkButton();
                foreach (GridViewRow r in grvUser.Rows)
                {
                    lBtn = (LinkButton)r.Cells[3].FindControl("lBtnLockUnLock");
                    lBtnResetPass = (LinkButton)r.Cells[4].FindControl("lBtnResetPassword");
              
                    if (r.Cells[2].Text == "False")
                    {
                        r.Cells[2].Text = string.Empty;
                        lBtn.Text = GetResource("Approve");
                    }
                    else
                    {
                        r.Cells[2].Text = "√";
                        lBtn.Text = GetResource("Disapprove");
                    }
                    if (r.Cells[0].Text == this.User.Identity.Name) lBtn.Visible = false;

                    lBtnResetPass.Text = GetResource("ResetPassword");

                    lBtn.Attributes["onclick"] = "javascript:return confirm('" + string.Format(GetResource("msDisapproveAccount"), lBtn.Text, Common.GetRowString(r.Cells[0].Text)) + "');";
                    lBtnResetPass.Attributes["onclick"] = "javascript:return confirm('" + string.Format(GetResource("msResetPassword"), Common.GetRowString(r.Cells[0].Text)) + "');";


                    UserPermission user = UserPermission.GetUserPermission(r.Cells[0].Text);
                    if (user != null)
                    {
                        DropDownList cbx = (DropDownList)r.Cells[5].FindControl("cbxPermission");
                        if (!string.IsNullOrEmpty(user.WebPermission))
                            cbx.SelectedValue = user.WebPermission;
                        else
                            cbx.SelectedIndex = 0;

                        CheckBox chkApp = (CheckBox)r.Cells[6].FindControl("cboAppPermission");
                        chkApp.Checked = user.AppPermission;
                    }
                }
            }
            catch (Exception ex)
            {

                logger.Error("Error Search", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");", true);
            }
        }

        private void Initialize()
        {
            lblNoRecord.Text = GetResource("lblNoRecord");
            btnSearch.Text = GetResource("btnSearch");
            lblUserName.Text = GetResource("lblUserName");
            cbxOnlineUser.Text = GetResource("cbxOnlineUser");
        }

        protected void grvUser_RowCreated(object sender, GridViewRowEventArgs e)
        {
            InitLanguage();
            if (e.Row.RowType == DataControlRowType.Header)
            {
                GridView grid = (GridView)sender;
                GridViewRow gridrow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
                TableCell tblcell = new TableHeaderCell();



                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.Text = GetResource("lblUserName"); ;
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(100);
                gridrow.Cells.Add(tblcell);

                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.Text = GetResource("Email"); ;
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(250);
                gridrow.Cells.Add(tblcell);

                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.Text = GetResource("Aprroved");
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(80);
                gridrow.Cells.Add(tblcell);

                //add user permission column
                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.Text = GetResource("lblWebPermission"); ;
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(150);
                gridrow.Cells.Add(tblcell);


                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.Text = GetResource("lblAppPermission"); ;
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(100);
                gridrow.Cells.Add(tblcell);


                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(80);
                gridrow.Cells.Add(tblcell);

                tblcell = new TableHeaderCell();
                tblcell.ColumnSpan = 1;
                tblcell.CssClass = "td_header";
                tblcell.Width = Unit.Pixel(100);
                gridrow.Cells.Add(tblcell);
                
                //Add header
                grid.Controls[0].Controls.AddAt(0, gridrow);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }
        protected void grvUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grvUser.PageIndex = e.NewPageIndex;
            Search();
        }

        protected void lBtnLockUnLock_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow r = (GridViewRow)((LinkButton)sender).NamingContainer;
                MembershipUser u = Membership.GetUser(r.Cells[0].Text);
                if (r.Cells[2].Text != string.Empty)
                {
                    u.IsApproved = false;
                    Membership.UpdateUser(u);
                }
                else
                {
                    u.IsApproved = true;
                    Membership.UpdateUser(u);
                }
                Search();
            }
            catch (Exception ex)
            {

                logger.Error("Error lBtnLockUnLock_Click", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error.Text"), ex.Message) + "\");", true);
            }
        }

        protected void lBtnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewRow r = (GridViewRow)((LinkButton)sender).NamingContainer;
                MembershipUser u = Membership.GetUser(r.Cells[0].Text);
                string newPassword = string.Empty;
                newPassword = u.ResetPassword();
                string replacePass = Membership.GeneratePassword(8, 4);
                bool changedPass = u.ChangePassword(newPassword, replacePass);

                if (newPassword != string.Empty && changedPass == true)
                {

                    string mailBodyTemplate =  Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(System.Web.HttpContext.Current.Request.ApplicationPath), "Template/" +  Common.AppSettingKey(Constant.MAIL_RESETPW_BODY));
                    logger.Debug("mail boyd template = " + mailBodyTemplate);
                    List<ParamMailContent> listParameter = ParamMailContent.GetListParamailContext(u.UserName ,replacePass , u.Email);

                    listParameter.Add(new ParamMailContent("{ACCOUNT_ID}", u.UserName));
                    listParameter.Add(new ParamMailContent("{ACCOUNT_EMAIL}", u.Email));
                    listParameter.Add(new ParamMailContent("{ACCOUNT_PASS}", replacePass));

                    MailInfo.SendMail( Common.GetRowString(r.Cells[1].Text),  Common.AppSettingKey(Constant.MAIL_RESETPW_SUBJECT), mailBodyTemplate, listParameter);

                    RegisterStartupScript("alert(\"" + GetJSMessage(GetResource("msResetPasswordSuccess"), string.Format(GetResource("msResetPasswordDetail"), Common.GetRowString(r.Cells[0].Text), replacePass)) + "\");");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("msResetPasswordFail")) + "\");", true);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error lBtnResetPassword_Click", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");", true);
            }
        }

        protected void cbxOnlineUser_CheckedChanged(object sender, EventArgs e)
        {
            Search();
        }

        protected void cbxPermission_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow r = (GridViewRow)((DropDownList)sender).NamingContainer;
            ChangePermission(r, true);

        }
        protected void cboAppPermission_CheckChanged(object sender, EventArgs e)
        {
            GridViewRow r = (GridViewRow)((CheckBox)sender).NamingContainer;
            ChangePermission(r, false);
        }
        private void ChangePermission( GridViewRow r , bool webPermission)
        {
            try
            {
                UserPermission userper = UserPermission.GetUserPermission(r.Cells[0].Text);
                if (userper == null)
                {
                    userper = new UserPermission();
                }
                userper.CompanyId = int.Parse(GetCompany());
                if (webPermission == true)
                {
                    DropDownList cbx = new DropDownList();
                    cbx = (DropDownList)r.Cells[0].FindControl("cbxPermission");
                    if (cbx.SelectedValue == string.Empty)
                        userper.WebPermission = null;
                    else 
                        userper.WebPermission = cbx.SelectedItem.Value;
                } 
                else
                {
                    CheckBox cbx = new CheckBox();
                    cbx = (CheckBox)r.Cells[0].FindControl("cboAppPermission");
                    userper.AppPermission = cbx.Checked;
                }

                if (userper.Username == null)
                {
                    userper.Username = r.Cells[0].Text;
                    userper.CreateAccount = this.User.Identity.Name;
                    userper.Insert();
                }
                else
                {
                    userper.ModifiedAccount = this.User.Identity.Name;
                    userper.Update();
                }
                Search();
            }
            catch (Exception ex)
            {
                logger.Error("Error ChangePermission ", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");", true);
            }
        }


      
        
    }
}