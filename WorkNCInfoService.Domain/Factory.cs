using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.Domain
{
   [Table(Name = MasterFactoryColumn.TABLE_NAME)]
    public class Factory : BaseDomain<Factory>, ICommonFunctions<Factory>
    {
        private int _FactoryId;
        private int _CompanyId;
        private string _No;
        private string _Name;
        private bool _isDeleted;
        private string _ImageFile;

        #region Property
        [ColumnAttribute(Name = MasterFactoryColumn.ID, IsPrimaryKey = true, CanBeNull = false, IsDbGenerated=true, AutoSync = AutoSync.OnInsert)]
        public int FactoryId
        {
            get { return _FactoryId; }
            set { _FactoryId = value; }
        }
        [ColumnAttribute(Name = MasterCompanyColumn.ID)]
        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }

        [ColumnAttribute(Name = MasterFactoryColumn.NO)]
        public string No
        {
            get { return _No; }
            set { _No = value; }
        }
        [ColumnAttribute(Name = MasterFactoryColumn.NAME)]
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        [ColumnAttribute(Name = MasterFactoryColumn.IsDELETED , CanBeNull = false)]
        public bool isDeleted
        {
            get { return _isDeleted; }
            set { _isDeleted = value; }
        }
        [ColumnAttribute(Name = MasterFactoryColumn.IMGFILE, CanBeNull = true)]
        public string ImageFile
        {
            get { return _ImageFile; }
            set { _ImageFile = value; }
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

        #region IcommonFunction
        public Factory GetByPrimaryKey()
        {
            Table<Factory> table = GetTable();

            Factory item = table.Single(d => (d.FactoryId == this.FactoryId));
            if (item != null)
                item.Detach<Factory>();
            return item;
        }
        #endregion 

        #region Method
        public static List<Factory> GetFactorySearch(int companyId , string FactoryName)
        {
            return (from f in GetTable()
                    where f.Name.Contains(FactoryName) && f.CompanyId == companyId
                    select f).ToList();
        }
        public static List<Factory> GetAllFactory(int companyId)
        {
            return (from f in GetTable()
                    where f.CompanyId == companyId && f.isDeleted == false
                    select f).ToList();
        }
        public static int InsertWithIndentity(Factory fac)
        {
            fac.Insert();
            return fac.FactoryId;
        }
        public static string GetFactoryName(int factoryId)
        {
            return (from f in GetTable()
                    where f.FactoryId == factoryId
                    select f).First().Name;
        }
        #endregion 


    }
}
