using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using WorkNCInfoService.Utilities;
using WorkNCInfoService.Domain;
using log4net;
using System.IO;

namespace WorkNCInfoService.WebForm
{
    public partial class Register : WorkNCPortalModuleBase
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Register).Name);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    InitLanguage();
                    Initialize();
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error Page_Load", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error.Text"), ex.Message) + "\");", true);
            }
        }
        private void Initialize()
        {
            lblUserName.Text = GetResource("lblUserName");
            lblPassWord.Text = GetResource("lblPassWord");
            lblConfirmPassWord.Text = GetResource("lblConfirmPassWord");
            btnRegister.Text = GetResource("btnRegister");
            lblEmail.Text = GetResource("Email");
            ReqValidatorUserName.ErrorMessage = GetResource("ReqValidatorUserName");
            ReqValidatorPassWord.ErrorMessage = GetResource("ReqValidatorPassWord");
            CompValidatorPassWord.Text = GetResource("CompValidatorPassWord");
            RegValidatorEmail.ErrorMessage = GetResource("RegValidatorEmail");
            RegValidatorPassWordLength.ErrorMessage = GetResource("RegValidatorPassWordLenght");
            ReqValidatorEmail.ErrorMessage = GetResource("ReqValidatorEmail");
            RegValidatorUserLenght.ErrorMessage = GetResource("RegValidatorUserLenght");
            ReqValidatorPassWordConfirm.ErrorMessage = GetResource("ReqValidatorPassWordConfirm");

            FillDropDownCompany();

            cboPermission.Items.Clear();
            cboPermission.Items.Add("");
            cboPermission.Items.Add(Constant.PERMISSION_MEMBER);
            cboPermission.Items.Add(Constant.PERMISSION_CHIEF);

            this.btnRegister.Attributes.Add("onclick", "javascript:if (Page_ClientValidate()){ this.disabled=true;}" + Page.ClientScript.GetPostBackEventReference(btnRegister, "").ToString());
        }
        protected void btnRegister_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipCreateStatus status;
                Membership.CreateUser(txtUserName.Text.Trim(), txtPassWord.Text, txtEmail.Text, null, null, true, out status);
                if (status.ToString() == "Success")
                {
                    UserPermission userpr = new UserPermission();
                    userpr.Username = txtUserName.Text;
                    userpr.CompanyId = int.Parse(cboCompanyName.SelectedValue);
                    userpr.AppPermission = chkAppPermission.Checked;
                    if (cboPermission.SelectedValue == "")
                        userpr.WebPermission = null; 
                    else
                        userpr.WebPermission = cboPermission.SelectedValue;

                    userpr.CreateAccount = this.User.Identity.Name;
                    userpr.Insert();

                 

                    //Send email created user 
                    string mailBodyTemplate = Server.MapPath("~/Template/" + Common.AppSettingKey(Constant.MAIL_REGISTER_BODY));
                 
                    List<ParamMailContent> listParameter = new List<ParamMailContent>();
                    string urlPotal = Common.AppSettingKey(Constant.PORTAL_CONFIG);

                    listParameter.Add(new ParamMailContent("{ACCOUNT_ID}", userpr.Username));
                    listParameter.Add(new ParamMailContent("{ACCOUNT_EMAIL}", txtEmail.Text));
                    listParameter.Add(new ParamMailContent("{ACCOUNT_PASS}", txtPassWord.Text));
                    listParameter.Add(new ParamMailContent("{WORKNC_URL}", urlPotal.Replace("Portal" , "")));

                    
                    MailInfo.SendMail(txtEmail.Text ,  Common.AppSettingKey(Constant.MAIL_REGISTER_SUBJECT), mailBodyTemplate,  listParameter);
                 
                    //Reset data
                    txtUserName.Text = txtEmail.Text = string.Empty;
                    cboPermission.SelectedIndex = 0;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + (GetResource("RegisterUserSuccess") + "\");"), true);
                  
                }
                if (Membership.GetUser(txtUserName.Text).UserName != string.Empty)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("RegisterUserFail")) + "\");", true);
                }
            }
            catch (Exception ex)
            {
                btnRegister.Enabled = true;
                this.btnRegister.Attributes.Add("onclick", "javascript:if (Page_ClientValidate()){ this.disabled=true;}" + Page.ClientScript.GetPostBackEventReference(btnRegister, "").ToString());
               
                logger.Error("Error Register", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");", true);
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
            }
            cboCompanyName.SelectedValue = GetCompany();
        }
    }
}