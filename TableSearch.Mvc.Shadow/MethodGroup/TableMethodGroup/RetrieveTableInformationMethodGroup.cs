using System;
using System.Collections.Generic;
using NHibernate;
using TableSearch.Data.Query;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.MethodGroup.TableMethodGroup
{
    public class RetrieveTableInformationMethodGroup : MethodGroupBase
    {
        #region Constructors

        public RetrieveTableInformationMethodGroup()
        {
            SearchForTable = TableQuery.RetrieveTableInformationByTableId;
            QueryForColumns = ColumnQuery.RetrievingColumnsByTableId;
        }

        public RetrieveTableInformationMethodGroup(Func<int, ISession, TableInformationResult> searchForTable, Func<int, ISession, IEnumerable<ColumnItemResult>> queryForColumns, Func<ISession> sessionMethod)
            : base(sessionMethod)
        {
            SearchForTable = searchForTable;
            QueryForColumns = queryForColumns;
        } 

        #endregion

        #region Properties

        public Func<int, ISession, IEnumerable<ColumnItemResult>> QueryForColumns { get; private set; }
        public Func<int, ISession, TableInformationResult> SearchForTable { get; private set; }

        #endregion
    }
}