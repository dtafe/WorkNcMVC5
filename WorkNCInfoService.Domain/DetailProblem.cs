using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace WorkNCInfoService.Domain
{
    [Table(Name = DetailProblemColumn.TABLE_NAME)]
    public class DetailProblem : BaseDomain<DetailProblem>, ICommonFunctions<DetailProblem>
    {
        private int _WorkZoneId;
        private int _WorkZoneDetailId;
        private int _FileId;

        private string _ImageFile;
        private string _Comment;

        public string Base64Data; 

        #region Propertise
        [ColumnAttribute(Name = WorkZoneDetailColumn.WORK_ZONE_ID, CanBeNull = false, IsPrimaryKey = true)]
        public int WorkZoneId
        {
            get
            {
                return this._WorkZoneId;
            }
            set
            {
                    this._WorkZoneId = value;
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
                    this._WorkZoneDetailId = value;
            }
        }
        [ColumnAttribute(Name = DetailProblemColumn.FILEID, IsPrimaryKey = true, CanBeNull = false)]
        public int FileId
        {
            get { return _FileId; }
            set { _FileId = value; }
        }

        [ColumnAttribute(Name = DetailProblemColumn.IMAGEFILE)]
        public string ImageFile
        {
            get { return _ImageFile; }
            set { _ImageFile = value; }
        }

        [ColumnAttribute(Name = DetailProblemColumn.COMMENT)]
        public string Comment
        {
            get { return _Comment; }
            set { _Comment = value; }
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

        public DetailProblem GetByPrimaryKey()
        {
            Table<DetailProblem> table = GetTable();

            DetailProblem item = table.Single(d => (d.FileId == this.FileId && d.WorkZoneId == this.WorkZoneId && d.WorkZoneDetailId == this.WorkZoneDetailId) );
            if (item != null)
                item.Detach<DetailProblem>();
            return item;
        }

        public static DetailProblem GetDetailProblem(int workZoneId, int detailId , int fileId)
        {
            return (from m in GetTable()
                    where m.WorkZoneId == workZoneId && m.WorkZoneDetailId == detailId && m.FileId == fileId
                    select m).FirstOrDefault();
        }
        public static List<DetailProblem> GetAllDetailProblem(int workZoneId)
        {
            return (from m in GetTable()
                    where m.WorkZoneId == workZoneId
                    select m).ToList();
        }

        public static List<DetailProblem> GetAllDetailProblem(int workZoneId, int wokzoneDetailId)
        {
            return (from m in GetTable()
                    where m.WorkZoneId == workZoneId && m.WorkZoneDetailId == wokzoneDetailId 
                    select m).ToList();
        }

        public static int GetCountListAddPicture(int workZoneId, int wokzoneDetailId)
        {
            return (from m in GetTable()
                    where m.WorkZoneId == workZoneId && m.WorkZoneDetailId == wokzoneDetailId 
                    select m).ToList().Count;
        }
        public static int GetNextFileID(int workZoneId , int workZoneDetailId)
        {
            var list = GetTable().Where(p => p.WorkZoneId == workZoneId && p.WorkZoneDetailId == workZoneDetailId);
            if (list.Count() == 0)
                return 1;
            else
                return list.Max(p => p.FileId) + 1;
        }
    }
}
