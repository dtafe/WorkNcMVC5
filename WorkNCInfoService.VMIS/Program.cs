using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WorkNCInfoService.VMIS.Properties;
using WorkNCInfoService.VMIS.VeroMachingInfoWS;

namespace WorkNCInfoService.VMIS
{
    static class Program
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            log4net.Config.XmlConfigurator.Configure();
            logger.Info("Begin Run VMIS");

            VMISCommon.StartProcessBeforeRun();
            if (Settings.Default.LOGIN_USER_NAME.Trim() == "")
            {
                Application.Run(new Login());
            }
            else
            {
                Machine[] listMachine = VMISCommon.WS.GetListMachineFromUser(Settings.Default.LOGIN_USER_NAME.Trim() , Settings.Default.LOGIN_PASS_WORD);
                if (listMachine == null)
                {
                     Application.Run(new Login());
                }
                else
                {
                    frmUpload frmUpload = new frmUpload();
                    frmUpload.FillMachine(listMachine.ToList());
                    frmUpload.LoginUser = Settings.Default.LOGIN_USER_NAME.Trim();
                    Application.Run(frmUpload);
                }
            }
        }
    }
}
