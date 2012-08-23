using System;
using NHibernate;
using TableSearch.Data.Query;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup
{
    public class RetrieveColumnInformationByColumnIdMethodGroup : MethodGroupBase
    {
        #region Constructors

        public RetrieveColumnInformationByColumnIdMethodGroup()
        {
            RetrieveColumnInformationById = ColumnQuery.RetrieveColumnInformationByColumnId;
        }

        public RetrieveColumnInformationByColumnIdMethodGroup(Func<int, ISession, ColumnInformationResult> retrieveColumnInformationById, Func<ISession> sessionMethod) : base(sessionMethod)
        {
            RetrieveColumnInformationById = retrieveColumnInformationById;
        } 

        #endregion

        #region Properties
        
        public Func<int, ISession, ColumnInformationResult> RetrieveColumnInformationById { get; private set; } 

        #endregion
    }
}