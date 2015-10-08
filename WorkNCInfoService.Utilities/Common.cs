using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net;
using System.Configuration;
namespace WorkNCInfoService.Utilities
{
    public class Common
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private static log4net.ILog logWarnBizMaker = null;
        public static string LoggerBizMakerName = "BizMaker";
        private static log4net.ILog logWarnBizMaker = log4net.LogManager.GetLogger(LoggerBizMakerName);

        private static bool _initLog4net = false;
        public static string configError = "NO ERROR";
        public static string DatabaseName;
        public static string ConnectionString = string.Empty;
        public static Configuration config = null;
        private static string _prefixMessage = "PrefixMessage";
        private static string _subfixMessage = "SubfixMessage";

        #region Config
        private static void ReconfigLogAppender(string _configFile, string refixLogName)
        {
            FileInfo fInfo = new FileInfo(_configFile);
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fInfo);
            string appenderNames = "FileAppender";
            log4net.Appender.IAppender[] appenders = logger.Logger.Repository.GetAppenders();
            foreach (log4net.Appender.IAppender appender in appenders)
            {
                if (appenderNames.Equals(appender.Name))
                {
                    log4net.Appender.RollingFileAppender fileAppender = appender as log4net.Appender.RollingFileAppender;
                    fileAppender.File = Path.Combine(Path.GetDirectoryName(fileAppender.File), refixLogName + Path.GetExtension(fileAppender.File));
                    fileAppender.ActivateOptions();
                    break;
                }
            }
        }
        public static string GetResourceString(string key)
        {
            try
            {
                return ResourceUtil.Instance.GetString(key);
            }
            catch (Exception ex)
            {
                logger.Debug("GetResourceString key = " + key);
                logger.Error("Error GetResourceString ", ex);
                return "";
            }
        }
        public static string AppSettingKey(string key)
        {
            try
            {
                return ConfigurationManager.AppSettings[key];
            }
            catch (Exception ex)
            {
                logger.Error("Error Get AppSettingKey ", ex);
                throw ex;
            }
        }
        public static log4net.ILog InitLog4Net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                logger.Debug("Init log4net completed");

            }
            catch (Exception ex)
            {
                logger.Error(ex);
                configError += ex.StackTrace;
            }
            return logger;
        }

        public static string SetException(string message)
        {
            return _prefixMessage + message + _subfixMessage;
        }

        public static string GetException(string message)
        {
            int indexPre = message.LastIndexOf(_prefixMessage);
            int indexSub = message.IndexOf(_subfixMessage);
            if (indexPre >= 0 && indexSub >= 0 && indexSub > indexPre)
                return message.Substring(indexPre + _prefixMessage.Length, indexSub - indexPre - _prefixMessage.Length);
            else
                return message;
        }
        public static string GetJSMessage(string title, string message)
        {
            return title + "\\n" + ChangeBreakLine((message.Replace('\r', ' ')).Replace("\n", "\\n"));
        }
        public static string ChangeBreakLine(string msg)
        {
            return msg.Replace('\r', ' ').Replace("\n", "\\n").Replace("\\\"", "\"").Replace("\"", "\\\"");
        }
        public static string GenerateFileNameBySystem(params object[] arg)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(arg[0] + "_");

                for (int i = arg.Length - 1; i >= 1; i--)
                {
                    if (arg[i] == null || arg[i].ToString() == "")
                        continue;
                    sb.Append(arg[i] + "_");
                    break;
                }
                sb.Append(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString());
                return sb.ToString();
            }
            catch (Exception ex)
            {
                logger.Error("ERROR GenerateFileNameBySystem !", ex);
                return Path.GetRandomFileName();
            }
        }
        #endregion

        #region Common
        public static Nullable<int> GetNullableInt(string obj)
        {
            if (obj == null || obj.ToString() == "") return null;
            return Convert.ToInt32(obj);
        }
        public static Nullable<double> GetNullableDouble(string obj)
        {
            if (obj == null || obj.ToString() == "") return null;
            return Convert.ToDouble(obj);
        }
        public static Nullable<bool> GetNullableBool(string obj)
        {
            if (obj == null || obj.ToString() == "") return null;
            return Convert.ToBoolean(obj);
        }
        public static Nullable<DateTime> GetNullableDateTime(string obj)
        {
            if (obj == null || obj.ToString() == "") return null;
            return Convert.ToDateTime(obj);
        }
        public static string GetNullableString(object obj)
        {
            if (obj == null) return "";
            return obj.ToString();
        }
        public static DateTime GetMinableDateTime(string obj)
        {
            if (obj == null || obj.ToString() == "") return DateTime.MinValue;
            return Convert.ToDateTime(obj);
        }
        public static string GetRowString(string textString)
        {
            return textString.Replace("&nbsp;", "");
        }
        public static string GetEnviromentConfigPath(string enviromentKey)
        {
            return System.Environment.GetEnvironmentVariable(enviromentKey, EnvironmentVariableTarget.Machine);

        }
        public static bool isAdminAccount(string loginName)
        {
            try
            {
                string[] arrAdmin = Common.AppSettingKey("AdminAcount").Split(';');
                if (arrAdmin.FirstOrDefault(p => p.Equals(loginName, StringComparison.OrdinalIgnoreCase)) != null)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion 

        #region Struct Folder WorkNC
        public static void UploadFileToPath(string base64Data, string pathUpload)
        {
             Byte[] data = Convert.FromBase64String(base64Data);
             File.WriteAllBytes(pathUpload, data);
            
        }
        public static string GetFolderFactory(int companyId, string companyName, int factoryId, string factoryName)
        {
            return string.Format("{0}/{1}_{2}/{3}_{4}", Constant.PORTAL, companyId, companyName, factoryId, factoryName);           
        }
        public static string GetFolderWorkZone(int companyId, string companyName, int factoryId, string factoryName, int workZoneId, string workZoneName)
        {
            return string.Format("{0}/{1}_{2}/{3}_{4}/{5}_{6}", Constant.PORTAL, companyId, companyName, factoryId, factoryName,workZoneId , workZoneName); 
        }
        public static string GetFolderWorkZoneDetail(int companyId, string companyName, int factoryId, string factoryName, int workZoneId, string workZoneName)
        {
            return string.Format("{0}/{1}_{2}/{3}_{4}/{5}_{6}/Detail", Constant.PORTAL, companyId, companyName, factoryId, factoryName, workZoneId, workZoneName); 
        }
        public static string GetFolderWorkZoneProblem(int companyId, string companyName, int factoryId, string factoryName, int workZoneId, string workZoneName)
        {
            return string.Format("{0}/{1}_{2}/{3}_{4}/{5}_{6}/Problem", Constant.PORTAL, companyId, companyName, factoryId, factoryName, workZoneId, workZoneName); 
        }
        #endregion
    }
}
