using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using WorkNCInfoService.Domain;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.WebForm.ResponeData
{
    [Serializable]
    [DataContract]
    public class FactoryStatus
    {
        [DataMember]
        public int FactoryId;

        [DataMember]
        public string FactoryName;

        [DataMember]
        public string ImageFile;

        [DataMember]
        public int InProcess;

        [DataMember]
        public int HaveProblem;

        [DataMember]
        public int Finished;

        public static List<FactoryStatus> GetListFactoryStatusFilter(int companyId, string factoryName, string fromDate, string toDate)
        {
            DateTime dtFrom = new DateTime(2000, 1, 1);
            DateTime dtTo = new DateTime(2200, 1, 1);

            if (!string.IsNullOrWhiteSpace(fromDate))
                dtFrom = Convert.ToDateTime(fromDate);
            if (!string.IsNullOrWhiteSpace(toDate))
                dtTo = Convert.ToDateTime(toDate);
            if (string.IsNullOrWhiteSpace(factoryName))
                factoryName = "";

            var context = new DBContext();

            var query =
                (from w in context.GetTable<WorkZone>()
                 join
                    f in context.GetTable<Factory>() on new { w.FactoryId } equals new { f.FactoryId }
                 where
                   (
                           f.CompanyId == companyId
                      && w.CompanyId == companyId
                      && f.Name.ToLower().Contains(factoryName.ToLower())
                      && (w.ProgramDate != null && dtFrom.Date <= w.ProgramDate.Value.Date && w.ProgramDate.Value.Date <= dtTo.Date)
                      && f.isDeleted == false
                    )
                 select new
                 {
                     ImageFile = f.ImageFile,
                     Status = WorkZoneDetail.GetStatusForWorkZone(w.WorkZoneId),
                     FactoryId = w.FactoryId,
                     FactoryName = f.Name
                 }
                 ).GroupBy(p => new { p.FactoryId, p.FactoryName }).ToList();

            List<FactoryStatus> list = new List<FactoryStatus>();
            string companyPath = string.Format(Constant.PATH_COMPANY, companyId, Company.GetCompanyName(companyId));
            foreach (var i in query)
            {
                FactoryStatus s = new FactoryStatus();
                s.FactoryId = i.First().FactoryId;
                s.FactoryName = i.First().FactoryName;
                string factoryPath = string.Format(Constant.PATH_FACTORY, s.FactoryId, s.FactoryName);
                s.ImageFile = string.IsNullOrEmpty(i.First().ImageFile) ? "" : string.Format(@"{0}{1}{2}/{3}", Common.AppSettingKey(Constant.PORTAL_CONFIG), companyPath, factoryPath, i.First().ImageFile.Replace(" ", "%20"));//Ima
               
                s.InProcess = i.Where(p => p.Status == 0).Count();
                s.HaveProblem = i.Where(p => p.Status == 1).Count();
                s.Finished = i.Where(p => p.Status == 2).Count();
              
                list.Add(s);
            }
            return list;
        }
    }
}