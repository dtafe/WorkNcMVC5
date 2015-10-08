using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
namespace WorkNCInfoService.Domain
{
    [Serializable]
    public class BaseDomain<T> where T : class,  ICommonFunctions<T>
    {
        protected DateTime _CreateDate;
        protected string _CreateAccount;
        protected DateTime _ModifiedDate;
        protected string _ModifiedAccount;

        #region Class Method

        public T Clone()
        {
            return (T)this.MemberwiseClone();
        }
        public static Table<T> GetTable()
        {
            DBContext db = new DBContext();
            return db.GetTable<T>();
        }

        public static Table<T> GetTable(DBContext db)
        {
            return db.GetTable<T>();
        }

        public static List<T> GetAll()
        {
            var list = from i in GetTable() select i;
            return list.ToList();
        }


        #endregion

        #region New Insert, Delete, Update
        public void Insert()
        {
            DBContext db = new DBContext();
            db.Insert<T>(this as T);
        }

        public void Delete()
        {
            DBContext db = new DBContext();
            db.Delete<T>(this as T);
        }

        public void Update()
        {
            DBContext db = new DBContext();
            db.Update<T>(this as T);
        }

        public void SetDefaultValueWhenInsert(bool bInsert)
        {
            this._ModifiedDate = DateTime.Now;
            if (bInsert) // Case Insert
            {
                this._CreateDate = DateTime.Now;
                this._ModifiedAccount = this._CreateAccount;
            }
        }

        #endregion

    }
    public interface ICommonFunctions<T>
    {
        T GetByPrimaryKey();
        void SetDefaultValueWhenInsert(bool bInsert);
    }
}
