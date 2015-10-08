using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkNCInfoService.Domain
{
    public class WorkZoneListInfo:WorkZone
    {
        public string FactoryName { get; set; }
        public string MachineName { get; set; }

        public static List<WorkZoneListInfo> GetWorkZoneListSearch(int companyId, string WorkZoneName, string DateMin, string DateMax, string FactoryName,string MachineName)
        {
            if(DateMin==string.Empty)
                DateMin ="1/1/1973";
            if(DateMax ==string.Empty)
                DateMax ="12/31/2999";
            var context = new DBContext();
            return (from w in context.GetTable<WorkZone>()
                         from f in context.GetTable<Factory>()
                         from m in context.GetTable<Machine>()
                         where
                         w.CompanyId == companyId 
                         && w.FactoryId == f.FactoryId
                         && w.MachineId == m.MachineId
                         && w.Name.Contains(WorkZoneName)
                         && f.Name.Contains(FactoryName)
                         && m.Name.Contains(MachineName)
                         && w.ProgramDate >= Convert.ToDateTime(DateMin)
                         && w.ProgramDate <= Convert.ToDateTime(DateMax)
                         select new
                         {
                             WorkZoneId = w.WorkZoneId,
                             Name = w.Name,
                             WorkZonePath = w.WorkZonePath,
                             ModelDataProgramer = w.ModelDataProgramer,
                             NCDataProgramer = w.NCDataProgramer,
                             ProgramDate = w.ProgramDate,
                             ModelName = w.ModelName,
                             Parts = w.Parts,
                             PartName = w.PartName,
                             MachiningTimeTotal = w.MachiningTimeTotal,
                             FactoryId = w.FactoryId,
                             FactoryName = f.Name,
                             MachineId = w.MachineId,
                             MachineName = m.Name,
                             Comment = w.Comment,
                             Status = w.Status,
                             ImageFile = w.ImageFile
                         }).AsEnumerable().Select(w => new WorkZoneListInfo
                        {
                            WorkZoneId = w.WorkZoneId,
                            Name = w.Name,
                            WorkZonePath = w.WorkZonePath,
                            ModelDataProgramer = w.ModelDataProgramer,
                            NCDataProgramer = w.NCDataProgramer,
                            ProgramDate = w.ProgramDate,
                            ModelName = w.ModelName,
                            Parts = w.Parts,
                            PartName = w.PartName,
                            MachiningTimeTotal = w.MachiningTimeTotal,
                            FactoryId = w.FactoryId,
                            FactoryName = w.FactoryName,
                            MachineId = w.MachineId,
                            MachineName = w.MachineName,
                            Status = w.Status,
                            Comment = w.Comment,
                            ImageFile = w.ImageFile
                        }).ToList();
        }
    }
}
