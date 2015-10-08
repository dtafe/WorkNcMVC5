using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Data.Linq;
using WorkNCInfoService.Utilities;

namespace WorkNCInfoService.Domain
{
    [Table(Name = UserPermissionColumn.TABLE_NAME)]
    public class UserPermission : BaseDomain<UserPermission>, ICommonFunctions<UserPermission>
    {
        private string _Username;
        private int companyId;
        private string webPermission;
        private bool appPermission;

        #region propertise
        [ColumnAttribute(Name = UserPermissionColumn.USERNAME, IsPrimaryKey = true, CanBeNull = false)]
        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        [ColumnAttribute(Name = UserPermissionColumn.COMPANY_ID)]
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
        [ColumnAttribute(Name = UserPermissionColumn.WEB_PERMISISON)]
        public string WebPermission
        {
            get { return webPermission; }
            set { webPermission = value; }
        }
        [ColumnAttribute(Name = UserPermissionColumn.APP_PERMISSION)]
        public bool AppPermission
        {
            get { return appPermission; }
            set { appPermission = value; }
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
        public UserPermission GetByPrimaryKey()
        {
            Table<UserPermission> table = GetTable();

            UserPermission item = table.Single(d => (d.Username == this.Username));
            if (item != null)
                item.Detach<UserPermission>();
            return item;
        }
        public static UserPermission GetUserPermission(string username)
        {
            return (from f in GetTable()
                    where f.Username == username
                    select f).FirstOrDefault();
        }
        public static bool existUser(string username)
        {
            var user = (from f in GetTable()
                        where f.Username == username
                        select f).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public static List<UserPermission> GetListUserPermision(int companyId)
        {
            return (from f in GetTable()
                    where f.CompanyId == companyId
                    select f).ToList();
        }
        public static bool IsChiefPermission(string username)
        {
            var user = (from f in GetTable()
                        where f.Username == username && f.WebPermission == "Chief"
                        select f).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public static bool IsMemberPermission(string username)
        {
            var user = (from f in GetTable()
                        where f.Username == username && f.WebPermission == "Member"
                        select f).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }
        public static string GetWebPermission(string username)
        {
            var user = (from f in GetTable()
                        where f.Username == username && (f.WebPermission == "Chief" || f.WebPermission == "Member")
                        select f).FirstOrDefault();
            if (user != null)
            {
                return user.WebPermission;
            }
            return "";
        }
        public static bool HasAppPermision(string username)
        {
            var user = (from f in GetTable()
                        where f.Username == username && f.AppPermission == true
                        select f).FirstOrDefault();
            if (user != null)
                return true;
            return false;
        }
        public static int? GetCompanyId(string userName, bool fromApp)
        {
            var user = (from f in GetTable()
                        where
                              f.Username == userName
                            && (fromApp == false || fromApp == f.AppPermission)
                        select f).FirstOrDefault();
            if (user != null)
                return user.CompanyId;
            return null;
        }
        public static List<Company> GetListCompanyFromUser(string username)
        {
            if (Common.isAdminAccount(username))
                return Company.GetAllCompany();
            else
            {
                List<Company> list = new List<Company>();
                var user = (from f in GetTable()
                            where f.Username == username
                            select f).FirstOrDefault();

                if (user != null)
                {
                    Company comp = new Company();
                    comp.CompanyId = user.companyId;
                    comp = comp.GetByPrimaryKey();

                    if(comp.isDeleted == false)
                       list.Add(comp);
                }
                return list;
            }
        }
        #endregion
    }
}
