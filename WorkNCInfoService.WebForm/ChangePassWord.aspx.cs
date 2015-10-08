using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;
using System.Web.Security;

namespace WorkNCInfoService.WebForm
{
    public partial class ChangePassWord : WorkNCPortalModuleBase
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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
            btnSave.Text = GetResource("btnSave");
            lblOldPassWord.Text = GetResource("lblOldPassWord");
            lblNewPassWord.Text = GetResource("lblNewPassWord");
            lblConfirmPassWord.Text = GetResource("lblConfirmPassWord");
            ReqValidatorNewPassWord.ErrorMessage = ReqValidatorPassWord.ErrorMessage = GetResource("ReqValidatorPassWord");
            ReqValidatorPassWordConfirm.ErrorMessage = GetResource("ReqValidatorPassWordConfirm");
            RegValidatorPassNewWordLength.ErrorMessage = RegValidatorPassWordLength.ErrorMessage = GetResource("RegValidatorPassWordLenght");
            CompValidatorPassWord.ErrorMessage = GetResource("CompValidatorPassWord");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                MembershipUser u = Membership.GetUser(User.Identity.Name);
                if (u.ChangePassword(txtOldPassWord.Text, txtNewPassWord.Text))
                {
                    txtOldPassWord.Text = txtNewPassWord.Text = txtConfirmPassWord.Text = string.Empty;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetResource("ChangePassSuccess") + "\");", true);
                }
                else
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), GetResource("ChangePassFail")) + "\");", true);
            }
            catch (Exception ex)
            {
                logger.Error("Error Save", ex);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "alert(\"" + GetJSMessage(GetResource("Title_Error"), ex.Message) + "\");", true);
            }
        }
    }
}