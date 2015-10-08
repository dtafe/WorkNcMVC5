using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using WorkNCInfoService.Domain;

namespace WorkNCInfoService.WebForm.WebServices
{
    /// <summary>
    /// Summary description for VeroMachingInfoWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VeroMachingInfoWS : System.Web.Services.WebService
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [WebMethod]
        public List<Machine> GetListMachineFromUser(string userName, string password)
        {
            try
            {
                if (Membership.ValidateUser(userName, password))
                {
                    if (UserPermission.GetWebPermission(userName) != "")
                    {
                        List<Machine> list = Machine.GetListMachineFromUser(userName);
                        if (list == null)
                            throw new Exception("This user dosen't have permission to access !");
                        else
                            return list;
                    }
                    else
                        throw new Exception("This user dosen't have permission to access !");
                }
                else
                    return null;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public void UploadWorkZone(WorkZone workZoneInfo, List<WorkZoneDetail> listWorkZoneDetail)
        {
            logger.Debug("Begin Upload work ZOne");
            try
            {
                int? companyId = UserPermission.GetCompanyId(workZoneInfo.CreateAccount, false);
                if (companyId != null)
                {
                    workZoneInfo.CompanyId = companyId.Value;
                    Machine objMachine = Machine.GetMachine(workZoneInfo.MachineId);

                    workZoneInfo.FactoryId = objMachine.FactoryId;

                    WorkZone.InsertUpdateWorkZone(Server.MapPath("~/"), workZoneInfo, listWorkZoneDetail);
                    logger.Debug("End upload work ZOne");
                }
            }
            catch (Exception ex)
            {
                logger.Error("Error UploadWorkZone ", ex);
                throw ex;
            }
        }
    }
}
