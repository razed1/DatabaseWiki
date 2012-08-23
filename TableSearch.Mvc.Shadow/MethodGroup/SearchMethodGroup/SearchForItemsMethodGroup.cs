using System;
using System.Collections.Generic;
using NHibernate;
using TableSearch.Data.Query;
using TableSearch.Service.Validation.SearchValidator;
using TableSearch.Shared.MethodResult;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup
{
    public class SearchForItemsMethodGroup : MethodGroupBase
    {
        #region Constructors
            
        public SearchForItemsMethodGroup()
        {
            SearchTextIsValid = SearchValidator.ValidateThatSearchTextIsLongEnough;
            SearchQueryMethod = TableQuery.SearchForTablesByName;
        }

        public SearchForItemsMethodGroup(Func<string, MethodResult<bool>> searchTextIsValid, Func<string, ISession, IList<SearchForItemsResult>> searchQueryMethod, Func<ISession> sessionMethod) : base(sessionMethod)
        {
            SearchTextIsValid = searchTextIsValid;
            SearchQueryMethod = searchQueryMethod;
        }

        #endregion
            
        #region Properties

        public Func<string, MethodResult<bool>> SearchTextIsValid { get; private set; }
        public Func<string, ISession, IList<SearchForItemsResult>> SearchQueryMethod { get; private set; }

        #endregion
    }
}