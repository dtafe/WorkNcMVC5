using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading.Tasks;
using WorkNCInfoService.VMIS.Properties;
using WorkNCInfoService.VMIS.VeroMachingInfoWS;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.VMIS
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtUserName.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show(Common.GetResourceString("MSG_LOGIN_EMPTY"));
                    return;
                }
                Machine[] listMachine = VMISCommon.WS.GetListMachineFromUser(txtUserName.Text.Trim(), txtPassword.Text.Trim());
                if (listMachine == null)
                {
                    MessageBox.Show(Common.GetResourceString("LoginFail"));
                }
                else
                {
                    frmUpload frmUpload = new frmUpload();
                    frmUpload.FillMachine(listMachine.ToList());
                    frmUpload.LoginUser = txtUserName.Text.Trim();
                    frmUpload.ShowDialog();
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
