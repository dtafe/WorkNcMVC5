using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkNCInfoService.Utilities
{
    public class Constant
    {
        public const string WORKNC_CONNECTIONSTRING = "WorkNCConnectionString";
        public const string PORTAL_CONFIG = "PortalUrl";
      
        public const string WORKNC_WZ_IMAGE_NAME = "basePart.jpg";
        public const string WORKNC_WZ_DETAIL_IMAGE_NAME = "outil{0}.res.jpg"; //outil1.res.jpg

        public const string WORKNC_DEFAULT_SHEET = "Sheet1";

        #region Mail information
        public const string MAIL_SERVER = "mail.mailServer";
        public const string MAIL_USER_NAME = "mail.mailUserName";
        public const string MAIL_USER = "mail.mailUser";
        public const string MAIL_PWD = "mail.mailPwd";
        public const string MAIL_TIME_OUT = "mail.mailTimeOut";
        public const string MAIL_PORT = "mail.port";
        public const string MAIL_ENABLE_SSL = "mail.EnableSsl";

        public const string MAIL_RESETPW_SUBJECT = "mail.ResetPasswordSubject";
        public const string MAIL_RESETPW_BODY = "mail.ResetPasswordBody";

        public const string MAIL_REGISTER_SUBJECT = "mail.RegisterUserSubject";
        public const string MAIL_REGISTER_BODY = "mail.RegisterUserBody";
        #endregion

        #region MailDelivery
        public const string MAIL_DELIVERY_ADDRESS = "mailGridDelivery.mailAddress";
        public const string MAIL_DELIVERY_DISPLAY = "mailGridDelivery.displayName";
        public const string MAIL_DELIVERY_USER = "mailGridDelivery.mailUserName";
        public const string MAIL_DELIVERY_PASS = "mailGridDelivery.mailPwd";
        #endregion 

        public const string STR_RETURN_SERVICE_SUCCESS = "Success";

        public const string PORTAL = "Portal";
        public const string PATH_COMPANY = "/{0}_{1}"; 
        public const string PATH_FACTORY = "/{0}_{1}";


        public const string STORAGE_CONNECT_STRING = "StorageConnectionString";
        public const string STORAGE_CONTAINER_NAME = "BlobContainer";

        #region Permission
       
        public const string PERMISSION_MEMBER = "Member";
        public const string PERMISSION_CHIEF = "Chief"; 
        #endregion 
    }
}
