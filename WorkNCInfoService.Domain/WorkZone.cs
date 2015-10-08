using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.IO;
using System.Linq;
using WorkNCInfoService.Utilities;


namespace WorkNCInfoService.Domain
{
    [Serializable]
    [Table(Name =WorkZoneColumn.TABLE_NAME)]
    public class WorkZone : BaseDomain<WorkZone>, ICommonFunctions<WorkZone>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Private Member 

        private int _WorkZoneId;
        private string _Name;

        private int _CompanyId;
        private string _CompanyName; 

        private int _FactoryId;
        private string _FactoryName; 

        private int _MachineId;

        private string _WorkZonePath;
        private string _ModelDataProgramer;
        private string _NCDataProgramer;
        private System.Nullable<System.DateTime> _ProgramDate;
        private string _ModelName;
        private string _Parts;
        private string _PartName;
        private string _MachiningTimeTotal;
      
        private string _Comment;
        private int _Status;
        private string _ImageFile;
        public string Base64Data;

        #endregion 

        #region Property

        [ColumnAttribute(Name = WorkZoneColumn.WORKZONE_ID, IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int WorkZoneId
        {
            get
            {
                return this._WorkZoneId;
            }
            set
            {
                if ((this._WorkZoneId != value))
                {
                    this._WorkZoneId = value;
                }
            }
        }

        [ColumnAttribute(Name = MasterCompanyColumn.ID)]
        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        [ColumnAttribute(Name = WorkZoneColumn.COMPANY_NAME)]
        public string CompanyName
        {
            get { return this._CompanyName; }
            set { this._CompanyName = value; }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_FactoryId")]
        public int FactoryId
        {
            get
            {
                return this._FactoryId;
            }
            set
            {
                if ((this._FactoryId != value))
                {
                    this._FactoryId = value;
                }
            }
        }
        [ColumnAttribute(Name = WorkZoneColumn.FACTORY_NAME)]
        public string FactoryName
        {
            get
            {
                return this._FactoryName;
            }
            set
            {
                 this._FactoryName = value;
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MachineId")]
        public int MachineId
        {
            get
            {
                return this._MachineId;
            }
            set
            {
                if ((this._MachineId != value))
                {
                    this._MachineId = value;
                }
            }
        }

        [ColumnAttribute(Name=WorkZoneColumn.NAME)]
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                if ((this._Name != value))
                {
                    this._Name = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = WorkZoneColumn.WORK_ZONE_PATH)]
        public string WorkZonePath
        {
            get
            {
                return this._WorkZonePath;
            }
            set
            {
                if ((this._WorkZonePath != value))
                {
                    this._WorkZonePath = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Name = WorkZoneColumn.MODEL_PROGRAMER)]
        public string ModelDataProgramer
        {
            get
            {
                return this._ModelDataProgramer;
            }
            set
            {
                if ((this._ModelDataProgramer != value))
                {
                    this._ModelDataProgramer = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_NCDataProgramer")]
        public string NCDataProgramer
        {
            get
            {
                return this._NCDataProgramer;
            }
            set
            {
                if ((this._NCDataProgramer != value))
                {
                    this._NCDataProgramer = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ProgramDate")]
        public System.Nullable<System.DateTime> ProgramDate
        {
            get
            {
                return this._ProgramDate;
            }
            set
            {
                if ((this._ProgramDate != value))
                {
                    this._ProgramDate = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ModelName")]
        public string ModelName
        {
            get
            {
                return this._ModelName;
            }
            set
            {
                if ((this._ModelName != value))
                {
                    this._ModelName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Parts")]
        public string Parts
        {
            get
            {
                return this._Parts;
            }
            set
            {
                if ((this._Parts != value))
                {
                    this._Parts = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_PartName")]
        public string PartName
        {
            get
            {
                return this._PartName;
            }
            set
            {
                if ((this._PartName != value))
                {
                    this._PartName = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_MachiningTimeTotal")]
        public string MachiningTimeTotal
        {
            get
            {
                return this._MachiningTimeTotal;
            }
            set
            {
                if ((this._MachiningTimeTotal != value))
                {
                    this._MachiningTimeTotal = value;
                }
            }
        }

        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Comment")]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                if ((this._Comment != value))
                {
                    this._Comment = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_Status")]
        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                if ((this._Status != value))
                {
                    this._Status = value;
                }
            }
        }
        [global::System.Data.Linq.Mapping.ColumnAttribute(Storage = "_ImageFile")]
        public string ImageFile
        {
            get
            {
                return this._ImageFile;
            }
            set
            {
                if ((this._ImageFile != value))
                {
                    this._ImageFile = value;
                }
            }
        }
        [ColumnAttribute(Name = BaseColumn.CREATE_DATE)]
        public System.DateTime CreateDate
        {
            get { return this._CreateDate; }
            set { this._CreateDate = value; }
        }

        [ColumnAttribute(Name = BaseColumn.CREATE_ACCOUNT)]
        public string CreateAccount
        {
            get { return this._CreateAccount; }
            set { this._CreateAccount = value; }
        }

        [ColumnAttribute(Name = BaseColumn.MODIFIED_DATE)]
        public System.DateTime ModifiedDate
        {
            get { return this._ModifiedDate; }
            set { this._ModifiedDate = value; }
        }

        [ColumnAttribute(Name = BaseColumn.MODIFIED_ACCOUNT)]
        public string ModifiedAccount
        {
            get { return this._ModifiedAccount; }
            set { this._ModifiedAccount = value; }
        }
        #endregion 

        #region Method
        public WorkZone GetByPrimaryKey()
        {
            Table<WorkZone> table = GetTable();

            WorkZone item = table.Single(d => (d.WorkZoneId == this.WorkZoneId));
            if (item != null)
                item.Detach<WorkZone>();
            return item;
        }

        private static int InsertWorkZoneWithID(WorkZone WZ)
        {
            WZ.Insert();
            return WZ.WorkZoneId;
        }

       
        public static int TransactionUpLoad(WorkZone WZ, List<WorkZoneDetail> listWZD)
        {
            int idz = 0;
            using (DBContext db = new DBContext())
            {
                using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                {
                    try
                    {
                        idz = InsertWorkZoneWithID(WZ);
                        int nextDetailId = 1; 
                        foreach (WorkZoneDetail wzd in listWZD)
                        {
                            wzd.WorkZoneId = idz;
                            wzd.WorkZoneDetailId = nextDetailId++;
                            wzd.Insert();
                        }
                        return idz;
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        if (idz != 0)
                        {
                            WZ.Delete();
                        }
                        logger.Error("Error Transaction", e);
                        throw e;
                    }
                }
            }
        }
        public static void InsertUpdateWorkZone(string directoryApp , WorkZone info, List<WorkZoneDetail> listDetail)
        {
            logger.InfoFormat("Begin call InsertUpdateWorkZone , workZoneName = {0} , directoryApp = {1}" ,info.Name , directoryApp);
            WorkZone workZoneDb = WorkZone.GetWorkZoneFromMachine(info.MachineId, info.CompanyId, info.Name);
          
            info.CompanyName = Company.GetCompanyName(info.CompanyId);
            info.FactoryName = Factory.GetFactoryName(info.FactoryId);
           
            int idz = 0;
            using (DBContext db = new DBContext())
            {
                using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                {
                    try
                    {
                        int nextWorkZoneDetailId = 1;
                        if (workZoneDb == null)
                        {
                            idz = InsertWorkZoneWithID(info);
                            info.WorkZoneId = idz;
                            foreach (WorkZoneDetail wzd in listDetail)
                            {
                                wzd.WorkZoneId = info.WorkZoneId;
                                wzd.WorkZoneDetailId = nextWorkZoneDetailId; 
                                wzd.CreateAccount = wzd.ModifiedAccount = info.CreateAccount;
                                wzd.Insert();
                                nextWorkZoneDetailId ++;
                            }
                        }
                        else
                        {
                            info.WorkZoneId = workZoneDb.WorkZoneId;
                            info.Update();

                            // Insert , update , detail 
                            List<WorkZoneDetail> listDetailDB = WorkZoneDetail.GetWorkZoneDetailByWorkZoneID(workZoneDb.WorkZoneId);
                            foreach (WorkZoneDetail i in listDetailDB)
                                i.Delete();

                            foreach (WorkZoneDetail i in listDetail)
                            {
                                i.WorkZoneId = info.WorkZoneId;
                                i.WorkZoneDetailId = nextWorkZoneDetailId; 
                                i.CreateAccount = i.ModifiedAccount = info.CreateAccount;
                                i.Insert();
                                nextWorkZoneDetailId++;
                            }
                        }

                        //Upload file WorkZone 
                        if (!string.IsNullOrEmpty(info.Base64Data))
                        {
                            string pathToUpload = directoryApp + Common.GetFolderWorkZone(info.CompanyId, info.CompanyName, info.FactoryId, info.FactoryName, info.WorkZoneId, info.Name);
                            pathToUpload = pathToUpload.Replace("/", "\\");
                            if(!Directory.Exists(pathToUpload))
                            {
                                Directory.CreateDirectory(pathToUpload);
                            }
                            pathToUpload = Path.Combine(pathToUpload, info.ImageFile);
                            Common.UploadFileToPath(info.Base64Data, pathToUpload);
                        }
                        //upload Detail 
                        foreach (WorkZoneDetail i in listDetail)
                        {
                            if (!string.IsNullOrEmpty(i.Base64Data))
                            {
                                string pathToUpload = directoryApp + Common.GetFolderWorkZoneDetail(info.CompanyId, info.CompanyName, info.FactoryId, info.FactoryName, info.WorkZoneId, info.Name);
                                pathToUpload = pathToUpload.Replace("/", "\\");
                                if (!Directory.Exists(pathToUpload))
                                {
                                    Directory.CreateDirectory(pathToUpload);
                                }
                                pathToUpload = Path.Combine(pathToUpload,  i.ImageFile);
                                Common.UploadFileToPath(i.Base64Data, pathToUpload);
                            }
                        }

                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        if(idz != 0)
                        {
                            info.Delete();
                        }
                        logger.Error("Error InsertUpdateWorkZone", ex);
                        throw ex;
                    }
                }
            }
        }
        public static WorkZone GetWorkZone(int workZoneId)
        {
            return (from w in WorkZone.GetTable()
                    where w.WorkZoneId == workZoneId
                    select w).FirstOrDefault();
        }
        public static void DeleteWorkZone(int WorkZoneID)
        {
            using (DBContext db = new DBContext())
            {
                using (System.Data.Common.DbTransaction tran = db.UseTransaction())
                {
                    try
                    {
                        
                        WorkZone wz = new WorkZone();
                        wz.WorkZoneId = WorkZoneID;
                        wz.Delete();

                        List<WorkZoneDetail> listWZD = WorkZoneDetail.GetWorkZoneDetailByWorkZoneID(WorkZoneID);
                        foreach (WorkZoneDetail wzd in listWZD)
                        {
                            wzd.Delete();
                        }

                        List<DetailProblem> listProblem = DetailProblem.GetAllDetailProblem(WorkZoneID);
                        foreach (DetailProblem p in listProblem)
                        {
                            p.Delete();
                        }
                    }
                    catch (Exception e)
                    {
                        tran.Rollback();
                        logger.Error("Error DeleteWorkZone", e);
                        throw e;
                    }
                }
            }
        }
        public static WorkZone GetWorkZoneFromMachine(int machineId, int companyId , string name)
        {
            return (from w in WorkZone.GetTable()
                    where
                       w.MachineId == machineId 
                    && w.CompanyId == companyId 
                    && w.Name == name
                    select w).FirstOrDefault();
        }
        public static string GetWorkZoneName(int workZoneId)
        {
            return (from f in GetTable()
                    where f.WorkZoneId == workZoneId
                    select f).First().Name;
        }
        public static int GetCountWorkZoneName(int machineId, string workZoneName)
        {
            return (from item in GetTable()
                    where item.MachineId != machineId && item.Name == workZoneName
                    select item).Count();
        }
        #endregion 
    }
}
