using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.Domain
{
    [Serializable]
    [Table(Name = WorkZoneDetailColumn.TABLE_NAME)]
    public class WorkZoneDetail : BaseDomain<WorkZoneDetail>, ICommonFunctions<WorkZoneDetail>
    {
        private static log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region Private Member 

        private int _WorkZoneId;
        private int _WorkZoneDetailId;

        private string _No;
        
        private string _PathType;
        private System.Nullable<double> _ToolLenth;
        private System.Nullable<double> _Tno;
        private System.Nullable<double> _StockAllowance;
        private System.Nullable<double> _Tolerance;

        //NC Data
        private string _NCFileName;
        private string _MachineTime;
        private System.Nullable<double> _MachineDistance;

        //Tool Infomation
        private string _ToolShape;
        private System.Nullable<double> _ToolDia;
        private System.Nullable<double> _ToolConerR;
        private string _HolderName;
        
        //Cut Condition
        private System.Nullable<double> _Spindle;
        private System.Nullable<double> _CuttingFeedRate;
        private System.Nullable<double> _ApproachFeedRate;
      
       
        private string _Comment;
        private int _Status;
        private string _ImageFile;
        public string Base64Data;

        #endregion

        #region Property

        [ColumnAttribute(Name = WorkZoneDetailColumn.WORK_ZONE_ID, CanBeNull = false, IsPrimaryKey = true)]
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
     
        [ColumnAttribute(Name = WorkZoneDetailColumn.WORK_ZONE_DETAIL_ID, IsPrimaryKey = true, CanBeNull = false)]
        public int WorkZoneDetailId
        {
            get
            {
                return this._WorkZoneDetailId;
            }
            set
            {
                if ((this._WorkZoneDetailId != value))
                {
                    this._WorkZoneDetailId = value;
                }
            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.NO)]
        public string No
        {
            get
            {
                return this._No;
            }
            set
            {
                if ((this._No != value))
                {
                    this._No = value;
                }
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.PATH_TYPE)]
        public string PathType
        {
            get
            {
                return this._PathType;
            }
            set
            {
                if ((this._PathType != value))
                {
                    this._PathType = value;
                }
            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.TOOL_LENTH, CanBeNull = true)]
        public System.Nullable<double> ToolLenth
        {
            get
            {
                return this._ToolLenth;
            }
            set
            {
                this._ToolLenth = value;
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.TNO, CanBeNull = true)]
        public System.Nullable<double> Tno
        {
            get
            {
                return this._Tno;
            }
            set
            {
                this._Tno = value;

            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.STOCK_ALLOWANCE, CanBeNull = true)]
        public System.Nullable<double> StockAllowance
        {
            get
            {
                return this._StockAllowance;
            }
            set
            {
                this._StockAllowance = value;
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.TOLERANCE, CanBeNull = true)]
        public System.Nullable<double> Tolerance
        {
            get
            {
                return this._Tolerance;
            }
            set
            {
                this._Tolerance = value;
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.NC_FILE_NAME)]
        public string NCFileName
        {
            get
            {
                return this._NCFileName;
            }
            set
            {
                    this._NCFileName = value;
            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.MACHINE_TIME)]
        public string MachineTime
        {
            get
            {
                return this._MachineTime;
            }
            set
            {
                this._MachineTime = value;

            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.MACHINE_DISTANCE, CanBeNull = true)]
        public System.Nullable<double> MachineDistance
        {
            get
            {
                return this._MachineDistance;
            }
            set
            {
                this._MachineDistance = value;
            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.TOOL_SHAPE)]
        public string ToolShape
        {
            get { return _ToolShape; }
            set { _ToolShape = value; }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.TOOL_DIA , CanBeNull=true)]
        public System.Nullable<double> ToolDia
        {
            get
            {
                return this._ToolDia;
            }
            set
            {
                this._ToolDia = value;
                
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.TOOL_CONERR , CanBeNull=true)]
        public System.Nullable<double> ToolConerR
        {
            get
            {
                return this._ToolConerR;
            }
            set
            {
                this._ToolConerR = value;
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.HOLDER_NAME)]
        public string HolderName
        {
            get { return _HolderName; }
            set { _HolderName = value; }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.SPINDLE, CanBeNull = true)]
        public System.Nullable<double> Spindle
        {
            get
            {
                return this._Spindle;
            }
            set
            {
                this._Spindle = value;
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.CUTTING_FEED_RATE, CanBeNull = true)]
        public System.Nullable<double> CuttingFeedRate
        {
            get
            {
                return this._CuttingFeedRate;
            }
            set
            {
                this._CuttingFeedRate = value;
            }
        }

        [ColumnAttribute(Name = WorkZoneDetailColumn.APPROACH_FEED_RATE, CanBeNull = true)]
        public System.Nullable<double> ApproachFeedRate
        {
            get
            {
                return this._ApproachFeedRate;
            }
            set
            {
                this._ApproachFeedRate = value;
            }
        }


        [ColumnAttribute(Name = WorkZoneDetailColumn.COMMENT)]
        public string Comment
        {
            get
            {
                return this._Comment;
            }
            set
            {
                this._Comment = value;     
            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.STATUS)]
        public int Status
        {
            get
            {
                return this._Status;
            }
            set
            {
                this._Status = value;
            }
        }
        [ColumnAttribute(Name = WorkZoneDetailColumn.IMAGE_FILE)]
        public string ImageFile
        {
            get
            {
                return this._ImageFile;
            }
            set
            {
                 this._ImageFile = value;
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
        public WorkZoneDetail GetByPrimaryKey()
        {
            Table<WorkZoneDetail> table = GetTable();

            WorkZoneDetail item = table.Single(d => (d.WorkZoneId == this.WorkZoneId && d.WorkZoneDetailId == this.WorkZoneDetailId));
            if (item != null)
                item.Detach<WorkZoneDetail>();
            return item;
        }

        public static List<WorkZoneDetail> GetWorkZoneDetailByWorkZoneID(int WorkZoneID)
        {
            return (from wd in WorkZoneDetail.GetTable()
                    where wd.WorkZoneId == WorkZoneID
                    select wd).ToList();
        }
        public static WorkZoneDetail GetWorkZoneDetail(int workZoneId , int detailId)
        {
            return (from wd in WorkZoneDetail.GetTable()
                    where workZoneId == wd.WorkZoneId && detailId == wd.WorkZoneDetailId
                    select wd).FirstOrDefault();
        }
        public static int GetStatusForWorkZone(int workZoneId)
        {
            int status = 0;
            List<WorkZoneDetail> listDetail = GetWorkZoneDetailByWorkZoneID(workZoneId);
            if (listDetail.Count > 0)
            {
                if (listDetail.Where(p => p.Status == 1).Count() > 0)
                {
                    status = 1; 
                }
                else if (listDetail.Where(p => p.Status == 0).Count() > 0)
                {
                    status = 0;
                }
                else
                {
                    status = 2; 
                }
            }
            return status;
        }
        public static double GetNextWorkZoneDetailId(int workZoneId)
        {
            var list = GetTable().Where(p => p.WorkZoneId == workZoneId);
            if (list.Count() == 0)
                return 1;
            else
                return list.Max(p => p.WorkZoneDetailId) + 1;
        }


        public static List<WorkZoneDetail> GetListWorkZoneDetail(int workZoneId)
        {
            List<WorkZoneDetail> listDetail = new List<WorkZoneDetail>();

            WorkZone objWorkZone = WorkZone.GetWorkZone(workZoneId);
            if (objWorkZone != null)
            {

                listDetail =
                     (from d in WorkZoneDetail.GetTable()
                      where
                        (
                           d.WorkZoneId == workZoneId
                        )
                      select d
                      ).ToList();

                foreach (WorkZoneDetail d in listDetail)
                {
                    if (string.IsNullOrEmpty(d.ImageFile))
                        d.ImageFile = "";  //no image
                    else
                    {
                        string pathWorkZoneDetail = Common.GetFolderWorkZoneDetail(objWorkZone.CompanyId, objWorkZone.CompanyName, objWorkZone.FactoryId, objWorkZone.FactoryName, objWorkZone.WorkZoneId, objWorkZone.Name);
                        pathWorkZoneDetail = pathWorkZoneDetail.Replace("Portal", "");
                        d.ImageFile = string.Format(@"{0}{1}/{2}", Common.AppSettingKey(Constant.PORTAL_CONFIG), pathWorkZoneDetail, d.ImageFile).Replace(" ", "%20");
                    }
                }
            }
            return listDetail;
        }

        public static void UpdateDetailStatus(int workZoneId, int workZoneDetailId, int updateStatus, string userName)
        {
            WorkZoneDetail item = WorkZoneDetail.GetWorkZoneDetail(workZoneId, workZoneDetailId);

            item.Status = updateStatus;
            item.ModifiedAccount = userName;
            item.Update();
            logger.Debug("End UpdateDetailStatus.Update successful!");
        }

        #endregion 

    }
}
