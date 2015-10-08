using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace WorkNCInfoService.Domain
{
    [Table(Name = MasterMachineColumn.TABLE_NAME)]
    public class Machine : BaseDomain<Machine>, ICommonFunctions<Machine>
    {
        private int _MachineId;
        private int _CompanyId;
        private int _FactoryId;
        private string _No;
        private string _Name;
        private bool _isDeleted;
        
        #region Property
        [ColumnAttribute(Name = MasterMachineColumn.ID, IsPrimaryKey = true, CanBeNull = false, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int MachineId
        {
            get { return _MachineId; }
            set { _MachineId = value; }
        }

        [ColumnAttribute(Name = MasterCompanyColumn.ID)]
        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }

        [ColumnAttribute(Name = MasterMachineColumn.FACTORYID)]
        public int FactoryId
        {
            get { return _FactoryId; }
            set { _FactoryId = value; }
        }

        [ColumnAttribute(Name = MasterMachineColumn.NO)]
        public string No
        {
            get { return _No; }
            set { _No = value; }
        }
        [ColumnAttribute(Name = MasterMachineColumn.NAME)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [ColumnAttribute(Name = MasterMachineColumn.IsDELETED, CanBeNull = false)]
        public bool isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
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
        public Machine GetByPrimaryKey()
        {
            Table<Machine> table = GetTable();

            Machine item = table.Single(d => (d.MachineId == this.MachineId));
            if (item != null)
                item.Detach<Machine>();
            return item;
        }
        public static List<Machine> GetMachineSearch(int companyID , string MachinName, string FactoryID)
        {
            List<Machine> list = null;
            if (FactoryID == string.Empty)
            {
                list = (from m in GetTable() 
                        where ( m.Name.Contains(MachinName) && m.CompanyId == companyID) 
                        select m).ToList();
            }
            else
            {
                list = (from m in GetTable()
                        where
                            m.Name.Contains(MachinName) && m.FactoryId.ToString()==FactoryID && m.CompanyId == companyID
                        select m).ToList();
            }

            return list;
        }
        public static List<Machine> GetListMachine(int companyID, string FactoryID)
        {
            return (from m in GetTable()
                     where (m.FactoryId.ToString() == FactoryID && m.CompanyId == companyID && m.isDeleted == false)
                        select m).ToList();
        }
        public static List<Machine> GetListMachineFromUser(string userName)
        {
            Nullable<int> companyId = UserPermission.GetCompanyId(userName, false);
            if (companyId == null)
                return null; 
            else 
                return 
                    (from m in GetTable()
                      where (m.CompanyId == companyId.Value && m.isDeleted == false)
                      select m).ToList();
        } 
        public static Machine GetMachine(int machineId)
        {
            return (from m in GetTable()
                    where (m.MachineId == machineId)
                    select m).FirstOrDefault();
        }
        #endregion 
    }
}
