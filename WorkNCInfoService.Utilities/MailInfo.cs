using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
//using SendGrid;

namespace WorkNCInfoService.Utilities
{
    public class MailInfo
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void SendMail(string mailAddressTo, string subject, string bodyFilePath, List<ParamMailContent>  listReplace)
        {
            try
            {
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = Common.AppSettingKey(Constant.MAIL_SERVER);
                smtpClient.Port = Convert.ToInt32(Common.AppSettingKey(Constant.MAIL_PORT));
                smtpClient.Credentials = new System.Net.NetworkCredential(Common.AppSettingKey(Constant.MAIL_USER), Common.AppSettingKey(Constant.MAIL_PWD));
                smtpClient.EnableSsl = Common.AppSettingKey(Constant.MAIL_ENABLE_SSL).ToLower() == "false" ? false : true;

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(Common.AppSettingKey(Constant.MAIL_USER));
                mail.To.Add(new MailAddress(mailAddressTo));
                mail.Subject = subject;
                mail.IsBodyHtml = true;


                mail.Body = GetBodyContent(bodyFilePath,listReplace);

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                logger.Error("Error SendMail ", ex);
                throw ex;
            }
        }
        /*
        public static void SendGridEmailDelivery(string mailAddressTo, string subject, string bodyFilePath, List<ParamMailContent> listReplace)
        {
            try
            {
                // Create the email object first, then add the properties.
                SendGridMessage myMessage = new SendGridMessage();
                myMessage.AddTo(mailAddressTo);
                myMessage.From = new MailAddress(Common.AppSettingKey(Constant.MAIL_DELIVERY_ADDRESS), Common.AppSettingKey(Constant.MAIL_DELIVERY_DISPLAY));
                myMessage.Subject = subject;
                myMessage.Text = GetBodyContent(bodyFilePath, listReplace);

                // Create credentials, specifying your user name and password.
                var credentials = new NetworkCredential(Common.AppSettingKey(Constant.MAIL_DELIVERY_USER), Common.AppSettingKey(Constant.MAIL_DELIVERY_PASS));

                // Create an Web transport for sending email.
                var transportWeb = new Web(credentials);

                // Send the email.
                // You can also use the **DeliverAsync** method, which returns an awaitable task.
                transportWeb.DeliverAsync(myMessage);
            }
            catch (Exception ex)
            {
                logger.Error("Error SendGridEmailDelivery ", ex);
                throw ex;
            }
        }
         * */
        private static string GetBodyContent(string bodyFilePath, List<ParamMailContent> listReplace)
        {
            string strBodyContent = "";

            if (File.Exists(bodyFilePath))
            {
                using (StreamReader fileRead = File.OpenText(bodyFilePath))
                {
                    while (!fileRead.EndOfStream)
                    {
                        strBodyContent += fileRead.ReadLine();
                    }
                }
            }
            else
                strBodyContent = bodyFilePath;

            if (listReplace != null)
            {
                foreach (ParamMailContent param in listReplace)
                {
                    strBodyContent = strBodyContent.Replace(param.Key, param.Value);
                }
            }
            return strBodyContent;
        }
    }
    public class ParamMailContent
    {
        public string Key = "";
        public string Value = "";

        public ParamMailContent(string keyV, string valueV)
        {
            Key = keyV;
            Value = valueV;
        }
        public ParamMailContent() { }

        public static List<ParamMailContent> GetListParamailContext(string accountId , string accountpass , string email)
        {
            List<ParamMailContent> list = new List<ParamMailContent>();
            list.Add(new ParamMailContent("{ACCOUNT_ID}", accountId));
            list.Add(new ParamMailContent("{ACCOUNT_EMAIL}", email));
            list.Add(new ParamMailContent("{ACCOUNT_PASS}", accountpass));
            return list;

        }
    }

}
