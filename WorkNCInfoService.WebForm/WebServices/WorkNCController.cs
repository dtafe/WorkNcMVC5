using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;
using WorkNCInfoService.WebForm.ResponeData;

namespace WorkNCInfoService.WebForm.WebServices
{
    public class WorkNCController : ApiController
    {
        readonly static ILog logger = LogManager.GetLogger(typeof(WorkNCController));
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpGet]
        public string ValidateUserName(string userName, string password)
        {
            try
            {
                if (Membership.ValidateUser(userName, password))
                {
                    //kiem tra tiep userName co quyen tren app 
                    if (!UserPermission.HasAppPermision(userName))
                    {
                        return "-2"; // Your account can not login on IOS. Please contact Admin.";
                    }
                    else
                    {
                        int ?companyID = UserPermission.GetCompanyId(userName, true);
                        if (companyID.HasValue)
                        {
                            return companyID.Value.ToString();
                        }
                        else
                        {
                            return "-2"; 
                        }                        
                    }
                }
                else
                {
                    return "-1"; // "Login Failed. Please remember that passwords are case sensitive";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
       
        [HttpGet]
        [AllowAnonymous]
        public List<FactoryStatus> GetListFactoryStatus(int companyId, string factoryName, string dateFrom, string dateTo)
        {
            return FactoryStatus.GetListFactoryStatusFilter(companyId , factoryName, dateFrom, dateTo);
        }

        // GET api/<controller>/1
        [HttpGet]
        [AllowAnonymous]
        public List<WorkZoneStatus> GetListWorkZoneStatus(int factoryId)
        {
            return WorkZoneStatus.GetListWorkZoneStatusFromFactory(factoryId);
        }

        // GET api/<controller>/1
        [HttpGet]
        [AllowAnonymous]
        public List<WorkZoneDetail> GetListWorkZoneDetail(int workZoneId)
        {
            return WorkZoneDetail.GetListWorkZoneDetail(workZoneId);
        }

        // GET api/<controller>/1
        [HttpGet, HttpPost]
        [AllowAnonymous]
        public void UpdateDetailStatus(WorkZoneDetail workZoneDetail)// int WorkZoneId, int WorkZoneDetailId, int Status, string CreateAccount) //
        {            
            logger.DebugFormat("updateDetailStatus Begin, workZoneId= {0}, workZoneDetailId = {1}, updateStatus ={2}, userName = {3}",workZoneDetail.WorkZoneId, workZoneDetail.WorkZoneDetailId, workZoneDetail.Status, workZoneDetail.CreateAccount );
            WorkZoneDetail.UpdateDetailStatus(workZoneDetail.WorkZoneId, workZoneDetail.WorkZoneDetailId, workZoneDetail.Status, workZoneDetail.CreateAccount);

            if (workZoneDetail.Status == 2)
            {
                logger.Debug("Case Finish workZone - remove detail problem");
                try
                {
                    WorkZone wk = WorkZone.GetWorkZone(workZoneDetail.WorkZoneId);

                    string pathFolderProblem = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, Common.GetFolderWorkZoneProblem(wk.CompanyId, wk.CompanyName, wk.FactoryId, wk.FactoryName, wk.WorkZoneId, wk.Name));
                  
                    List<DetailProblem> listProblem = DetailProblem.GetAllDetailProblem(workZoneDetail.WorkZoneId, workZoneDetail.WorkZoneDetailId); 
                    foreach(DetailProblem i in listProblem)
                    {
                        i.Delete();

                        string pathFile = Path.Combine(pathFolderProblem, i.ImageFile);
                        if (File.Exists(pathFile))
                            File.Delete(pathFile);

                      
                    }
                }
                catch (Exception ex)
                {
                    logger.Warn("Ignore case delete problem" ,ex);
                }
            }
        }

         // GET api/<controller>/1
        [HttpGet, HttpPost]
        [AllowAnonymous]
        public string UploadFileTest()
        {
         
            DetailProblem problem = new DetailProblem();
            problem.WorkZoneId = 50;
            problem.WorkZoneDetailId = 1;
            problem.ImageFile = "a.jpg";
            problem.Base64Data = "afdsfdfs";
            problem.FileId = 1;
            problem.CreateAccount = "ddv1";
            UploadFile(problem);
            return "";
        }

        // GET api/<controller>/1
        [HttpGet, HttpPost]
        [AllowAnonymous]
        public void UploadFile(DetailProblem problem)
        {            
            logger.DebugFormat("Begin UploadFile , WorkZoneId = {0},  workZOneDetailId id = {1}", problem.WorkZoneId, problem.WorkZoneDetailId);
            WorkZone wk = WorkZone.GetWorkZone(problem.WorkZoneId);
            string pathFolderProblem = Path.Combine(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath, Common.GetFolderWorkZoneProblem(wk.CompanyId, wk.CompanyName, wk.FactoryId, wk.FactoryName, wk.WorkZoneId, wk.Name));
            logger.Debug("PhysicalPath pathFolder =" + pathFolderProblem);
            
            string uploadFilePath = "";
            try
            {
                using (DBContext db = new DBContext())
                {
                    using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                    {
                        try
                        {
                            if (problem.FileId == 0) // case insert New
                            {
                                logger.Debug(" Case Insert new problem");

                                WorkZoneDetail d = WorkZoneDetail.GetWorkZoneDetail(problem.WorkZoneId, problem.WorkZoneDetailId);
                                d.Status = 1; // Has Problem
                                d.ModifiedAccount = problem.CreateAccount;
                                d.Update();


                                problem.FileId = DetailProblem.GetNextFileID(d.WorkZoneId, d.WorkZoneDetailId);
                                if(problem.Base64Data!= null)
                                {
                                    problem.ImageFile = string.Format("{0}_{1}.png", d.WorkZoneDetailId, problem.FileId);
                                }
                                problem.ModifiedAccount = problem.CreateAccount;
                                problem.Insert();

                                logger.Debug(" Update problem and WorkZone Detail");

                                if (!Directory.Exists(pathFolderProblem))
                                    Directory.CreateDirectory(pathFolderProblem);

                                if (!string.IsNullOrEmpty(problem.Base64Data))
                                {
                                    uploadFilePath = Path.Combine(pathFolderProblem, problem.ImageFile);
                                    logger.Debug("Case Create file problem = " + uploadFilePath);
                                    Byte[] data = Convert.FromBase64String(problem.Base64Data);
                                    File.WriteAllBytes(uploadFilePath, data);
                                    problem.Base64Data = null;
                                } 
                            }
                            else if (problem.FileId != 0) // Update or Delete
                            {
                                DetailProblem p = DetailProblem.GetDetailProblem(problem.WorkZoneId, problem.WorkZoneDetailId, problem.FileId);
                                if(string.IsNullOrEmpty(problem.ImageFile))
                                {
                                    //delete 
                                    logger.Debug("Case Delete detail Problem");
                                    p.Delete();

                                    uploadFilePath = Path.Combine(pathFolderProblem, p.ImageFile);
                                    logger.Debug("Case Delete file uploadFilePath = " + uploadFilePath);
                                    if (File.Exists(uploadFilePath))
                                        File.Delete(uploadFilePath);
                                }
                                else
                                {
                                    logger.Debug("Case update detail Problem");
                                    p.Comment = problem.Comment;
                                    p.ModifiedAccount = problem.CreateAccount;
                                    p.Update();
                                }
                            }
                            tran.Commit();
                        }
                        catch (Exception ex)
                        {
                            tran.Rollback();
                            logger.Error("Error UploadFile", ex);
                            throw ex;
                        }
                    }
                }
                logger.Debug("End UploadFile");
            }
            catch (Exception ex)
            {
                logger.Error("Error UploadFile ", ex);
                throw ex;
            }
        }

        // GET api/<controller>/1
        [HttpGet]
        [AllowAnonymous]
        public List<DetailProblem> GetAllFileProblem(int workZoneId, int workZoneDetailId)
        {
            //string workZoneName = WorkZone.GetWorkZoneName(workZoneId);
            List<DetailProblem> listFile = DetailProblem.GetAllDetailProblem(workZoneId, workZoneDetailId);

            WorkZone wz = WorkZone.GetWorkZone(workZoneId);
            foreach (DetailProblem i in listFile)
            {
                string pathProblem = Common.GetFolderWorkZoneProblem(wz.CompanyId, wz.CompanyName, wz.FactoryId, wz.FactoryName, wz.WorkZoneId, wz.Name);
                pathProblem = pathProblem.Replace("Portal", "");
                i.ImageFile = string.Format(@"{0}{1}/{2}", Common.AppSettingKey(Constant.PORTAL_CONFIG), pathProblem, i.ImageFile).Replace(" ", "%20");
            }
            return listFile;
        }

        // GET api/<controller>/1
        [HttpGet]
        [AllowAnonymous]
        public int GetCountFileProblem(int workZoneId,int workZoneDetailId)
        {
            return DetailProblem.GetCountListAddPicture(workZoneId, workZoneDetailId);
        }
    }
}