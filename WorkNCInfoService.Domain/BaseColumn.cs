using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkNCInfoService.Domain
{
    public class BaseColumn
    {
        public BaseColumn() { }

        #region Common column of all table

        public const string CREATE_ACCOUNT = "CreateAccount";
        public const string CREATE_DATE = "CreateDate";
        public const string MODIFIED_ACCOUNT = "ModifiedAccount";
        public const string MODIFIED_DATE = "ModifiedDate";

        #endregion
    }


    #region "Master Table"
    public class MasterCompanyColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_Company";

        public const string ID = "CompanyId";
        public const string COMPANY_NAME = "CompanyName";
        public const string ADDRESS1 = "Address1";
        public const string ADDRESS2 = "Address2";
        public const string TEL = "TEL";
        public const string FAX = "FAX";
    }
    public class MasterFactoryColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_Factory";

        public const string ID = "FactoryId";
        public const string NO = "No";
        public const string NAME = "Name";
        public const string IsDELETED  = "isDeleted";
        public const string IMGFILE = "ImageFile";
    }
    public class MasterMachineColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_Machine";

        public const string ID = "MachineId";
        public const string FACTORYID = "FactoryId";
        public const string NO = "No";
        public const string NAME = "Name";
        public const string IsDELETED = "isDeleted";
    }
    public class WorkZoneColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_WorkZone";

        public const string WORKZONE_ID = "WorkZoneId";
        public const string NAME = "Name";

        public const string FACTORY_NAME = "FactoryName";
        public const string COMPANY_NAME = "CompanyName";

        public const string WORK_ZONE_PATH = "WorkZonePath";
        public const string MODEL_PROGRAMER  = "ModelDataProgramer"; 
        public const string PARTS = "Parts";
    }
    public class WorkZoneDetailColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_WorkZoneDetail";

        public const string WORK_ZONE_ID = "WorkZoneId";
        public const string WORK_ZONE_DETAIL_ID = "WorkZoneDetailId";

        public const string NO = "No";
        public const string PATH_TYPE = "PathType";
        public const string TNO = "Tno";
        public const string TOOL_LENTH = "ToolLenth";
        public const string STOCK_ALLOWANCE = "StockAllowance";
        public const string TOLERANCE = "Tolerance";


        public const string NC_FILE_NAME = "NCFileName";
        public const string MACHINE_TIME = "MachineTime";
        public const string MACHINE_DISTANCE = "MachineDistance";

        public const string TOOL_SHAPE = "ToolShape";
        public const string TOOL_DIA = "ToolDia";
        public const string TOOL_CONERR = "ToolConerR";
        public const string HOLDER_NAME = "HolderName";
    
      
        public const string SPINDLE = "Spindle";
        public const string CUTTING_FEED_RATE = "CuttingFeedRate";
        public const string APPROACH_FEED_RATE = "ApproachFeedRate";

        public const string COMMENT = "Comment";

        public const string STATUS = "Status";
        public const string IMAGE_FILE = "ImageFile";
    }
    public class UserPermissionColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_UserPermission";

        public const string USERNAME = "Username";
        public const string COMPANY_ID = "CompanyId";
        public const string WEB_PERMISISON = "WebPermission";
        public const string APP_PERMISSION = "AppPermission";
        public const string USERTYPE = "Usertype";
    }

    public class DetailProblemColumn : BaseColumn
    {
        public const string TABLE_NAME = "WorkNC_WorkZoneDetailProblem";

        public const string FILEID = "FileID";
        public const string ID = "Id";
        public const string IMAGEFILE = "ImageFile";
        public const string COMMENT = "Comment";
    }
    #endregion    

}
