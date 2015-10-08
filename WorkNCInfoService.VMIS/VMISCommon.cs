using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using WorkNCInfoService.Utilities;
using WorkNCInfoService.VMIS.Properties;

namespace WorkNCInfoService.VMIS
{
    public class VMISCommon
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static VeroMachingInfoWS.VeroMachingInfoWS WS
        {
            get
            {
                VeroMachingInfoWS.VeroMachingInfoWS ws = new VeroMachingInfoWS.VeroMachingInfoWS();
                ws.UseDefaultCredentials = true;
                //ws.Credentials = new System.Net.NetworkCredential(@"administrator", "Password!"); 

                ws.Url = Settings.Default.WorkNCInfoService_VMIS_VeroMachingInfoWS_VeroMachingInfoWS;
               
                ws.Timeout = int.Parse(Settings.Default.CONFIG_TIME_OUT);
                return ws;
            }
        }
        public static void StartProcessBeforeRun()
        {
         
            string pathProcess = Settings.Default.PROCESS_PATH;
            if (pathProcess != "")
            {
                string enviromentKey = System.Text.RegularExpressions.Regex.Match(Settings.Default.PROCESS_PATH, @"\%(\w+)\%").Groups[1].Value;
                if (!string.IsNullOrEmpty(enviromentKey))
                {
                    string pathEnviroment = Common.GetEnviromentConfigPath(enviromentKey);
                    pathProcess = pathProcess.Replace("%", "").Replace(enviromentKey, pathEnviroment);
                }

                if (File.Exists(pathProcess))
                {
                    logger.Debug("StartProcessBeforeRun pathProcess =" + pathProcess);
                    Process cmd = new Process();
                    cmd.StartInfo.UseShellExecute = false;
                    cmd.StartInfo.FileName = pathProcess;
                    cmd.StartInfo.Arguments = Settings.Default.PROCESS_ARGUMENT;
                    cmd.StartInfo.CreateNoWindow = true;
                    cmd.StartInfo.RedirectStandardInput = true;
                    cmd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    cmd.Start();
                }
            }
        }

     
    }
}
