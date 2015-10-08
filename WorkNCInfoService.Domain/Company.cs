using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace WorkNCInfoService.Domain
{
    [Table(Name = MasterCompanyColumn.TABLE_NAME)]
    public class Company : BaseDomain<Company>, ICommonFunctions<Company>
    {
        private int _CompanyId;
        private string _CompanyName;
        private string _Address1;
        private string _Address2;
        private string _Tel;
        private string _Fax;
        private bool _isDeleted;

        #region Property 
        [ColumnAttribute(Name = MasterCompanyColumn.ID , IsPrimaryKey=true)]
        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }
        [ColumnAttribute(Name = MasterCompanyColumn.COMPANY_NAME)]
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }
        [ColumnAttribute(Name = MasterCompanyColumn.ADDRESS1)]
        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        [ColumnAttribute(Name = MasterCompanyColumn.ADDRESS2)]
        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }

        [ColumnAttribute(Name = MasterCompanyColumn.TEL)]
        public string TEL
        {
            get { return _Tel; }
            set { _Tel = value; }
        }

        [ColumnAttribute(Name = MasterCompanyColumn.FAX)]
        public string FAX
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        [ColumnAttribute(Name = MasterFactoryColumn.IsDELETED, CanBeNull = false)]
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
        public Company GetByPrimaryKey()
        {
            Table<Company> table = GetTable();

            Company item = table.Single(d => (d.CompanyId == this.CompanyId));
            if (item != null)
                item.Detach<Company>();
            return item;
        }
        public static List<Company> GetAllCompany()
        {
            return (from f in GetTable()
                    where f.isDeleted == false
                    select f).ToList();
        }
        public static List<Company> GetCompanySearch(string companyName)
        {
            return (from f in GetTable()
                    where f.CompanyName.Contains(companyName)
                    select f).ToList();
        }
        public static string GetCompanyName(int companyID)
        {
            return (from f in GetTable()
                    where f.CompanyId == companyID
                    select f).First().CompanyName;
        }
        public static double GetNextCompanyId()
        {
            var list = GetAll();
            if (list.Count() == 0)
                return 1;
            else
                return list.Max(p => p.CompanyId) + 1;
        }
        #endregion 
    }
}
