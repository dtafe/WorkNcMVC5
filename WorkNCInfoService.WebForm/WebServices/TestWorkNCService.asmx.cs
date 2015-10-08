using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using WorkNCInfoService.WebForm.ResponeData;
using WorkNCInfoService.Domain;
using System.Web.Security;

namespace WorkNCInfoService.WebForm.WebServices
{
    /// <summary>
    /// Summary description for TestWorkNCService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class TestWorkNCService : System.Web.Services.WebService
    {
         private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

         #region Test 
        [WebMethod]
        public List<FactoryStatus> TestGetListFactoryStatus(string factoryName ,string dateFrom, string dateTo)
        {
            return FactoryStatus.GetListFactoryStatusFilter(1, factoryName, dateFrom, dateTo);
        }
        [WebMethod]
        public List<WorkZoneStatus> TestGetListWorkZoneStatus(int factoryId)
        {
            return WorkZoneStatus.GetListWorkZoneStatusFromFactory(factoryId);
        }
        [WebMethod]
        public List<WorkZoneDetail> TestGetListWorkZoneDetail(int workZoneId)
        {
            return WorkZoneDetail.GetListWorkZoneDetail(workZoneId);
        }
        [WebMethod]
        public void TestUpload(string newPath)
        {
            WorkNCController control = new WorkNCController();
            DetailProblem p = new DetailProblem();
            p.Comment = "sdf";
            p.WorkZoneId = 62;
            p.WorkZoneDetailId = 1;
            p.FileId = 1;

            p.ImageFile = @""; 
            if(newPath=="")
                newPath = @"D:\t1.jpg";
            byte[] data = File.ReadAllBytes(newPath); 
            p.Base64Data = Convert.ToBase64String(data);
            p.CreateAccount = "WS";
            control.UploadFile(p);
        }
        #endregion

       
    }
}
