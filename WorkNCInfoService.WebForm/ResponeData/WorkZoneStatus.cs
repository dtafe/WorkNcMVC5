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
    public class WorkZoneStatus
    {
        [DataMember]
        public int WorkZoneId;

        [DataMember]
        public int CompanyId;

        [DataMember]
        public int StatusId; // 0  1  2

        [DataMember]
        public string Status = "";

        [DataMember]
        public string ImageFile;

        [DataMember]
        public string WorkZoneName;

        [DataMember]
        public string Upload = "";

        [DataMember]
        public string Machine = "";

        [DataMember]
        public string Date = "" ;

        [DataMember]
        public string Operator= "";


        public static List<WorkZoneStatus> GetListWorkZoneStatusFromFactory(int FactoryId)
        {
            List<WorkZoneStatus> list = new List<WorkZoneStatus>();
                var context = new DBContext();
               list =
                    (from w in context.GetTable<WorkZone>()
                     join m in context.GetTable<Machine>() on new { w.MachineId  } equals new { m.MachineId }
                     where
                       (
                           w.FactoryId == FactoryId 
                           && m.isDeleted == false
                       )
                     select new WorkZoneStatus()
                     {
                         WorkZoneId = w.WorkZoneId,
                         WorkZoneName = w.Name ,
                         CompanyId = w.CompanyId,
                         ImageFile = w.ImageFile,
                         Upload = string.Format("{0:yyyy-MM-dd}", w.ModifiedDate),
                         Machine = m.Name,
                         Operator = w.NCDataProgramer,
                         StatusId = WorkZoneDetail.GetStatusForWorkZone(w.WorkZoneId),   //w.Status,
                         Date = !w.ProgramDate.HasValue ? "" : w.ProgramDate.Value.ToString(),
                     }
                     ).ToList();

               foreach (WorkZoneStatus i in list)
               {
                   i.Status = Common.GetResourceString(string.Format("STATUS_{0}", i.StatusId));
           
                   if (i.Date != "")
                       i.Date = DateTime.Parse(i.Date).ToString("yyyy-MM-dd");
                   if (string.IsNullOrEmpty(i.ImageFile))
                       i.ImageFile = "";  //no image
                   else
                   {
                       string pathWorkZone = Common.GetFolderWorkZone(i.CompanyId, Company.GetCompanyName(i.CompanyId), FactoryId, Factory.GetFactoryName(FactoryId), i.WorkZoneId, i.WorkZoneName);
                       pathWorkZone = pathWorkZone.Replace("Portal", "");
                       i.ImageFile = string.Format(@"{0}{1}/{2}", Common.AppSettingKey(Constant.PORTAL_CONFIG), pathWorkZone, i.ImageFile).Replace(" ", "%20");
                   }
               }
            return list;
        }

    }
}