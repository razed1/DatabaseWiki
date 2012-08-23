using System;
using NHibernate;
using TableSearch.Data.Persist;
using TableSearch.Data.Query;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;

namespace TableSearch.Mvc.Shadow.MethodGroup.TableMethodGroup
{
    public class UpdateTableDescriptionMethodGroup : MethodGroupBase
    {

        #region Constructors
        
        public UpdateTableDescriptionMethodGroup()
        {
            UpdateMethod = TablePersist.UpdateTableDescription;
            TableExists = TableQuery.TableExists;
        }

        public UpdateTableDescriptionMethodGroup(Func<int, ISession, bool> tableExists, Action<int, string, ISession> updateMethod, Func<ISession> sessionMethod)
            : base(sessionMethod)
        {
            TableExists = tableExists;
            UpdateMethod = updateMethod;
        }

        #endregion
        
        #region Properties

        public Func<int, ISession, bool> TableExists { get; private set; }
        public Action<int, string, ISession> UpdateMethod { get; private set; } 

        #endregion
    }
}