using System;
using NHibernate;
using TableSearch.Data.Persist;
using TableSearch.Data.Query;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;

namespace TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup
{
    public class UpdateColumnDescriptionMethodGroup : MethodGroupBase
    {
        #region Fields
        
        public Func<int, ISession, bool> ColumnExists { get; set; }
        public Action<int, string, ISession> UpdateMethod { get; set; } 

        #endregion

        #region Constructors

        public UpdateColumnDescriptionMethodGroup()
        {
            ColumnExists = ColumnQuery.ColumnExists;
            UpdateMethod = ColumnPersist.UpdateColumnDescription;
        }

        public UpdateColumnDescriptionMethodGroup(Func<int, ISession, bool> columnExists, Action<int, string, ISession> updateMethod, Func<ISession> sessionMethod ) : base(sessionMethod)
        {
            ColumnExists = columnExists;
            UpdateMethod = updateMethod;
        } 

        #endregion
    }
}