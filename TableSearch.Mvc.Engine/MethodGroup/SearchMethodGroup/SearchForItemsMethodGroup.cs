using System;
using System.Collections.Generic;
using BH.Framework.Utility.Result;
using TableSearch.Data.Query;
using TableSearch.Service.Validation.SearchValidator;
using TableSearch.Shared.Model.SearchResult;

namespace TableSearch.Mvc.Engine.MethodGroup.SearchMethodGroup
{
    public class SearchForItemsMethodGroup
    {
        #region Fields
            
        private readonly Func<string, MethodResult<bool>> _searchTextIsValid;
        private readonly Func<string, IList<SearchForItemsResult>> _searchQueryMethod;

        #endregion
            
        #region Constructors
            
        public SearchForItemsMethodGroup()
        {
            _searchTextIsValid = SearchValidator.ValidateThatSearchTextIsLongEnough;
            _searchQueryMethod = TableQuery.SearchForTablesByName;
        }

        public SearchForItemsMethodGroup(Func<string, MethodResult<bool>> searchTextIsValid, Func<string, IList<SearchForItemsResult>> searchQueryMethod)
        {
            _searchTextIsValid = searchTextIsValid;
            _searchQueryMethod = searchQueryMethod;
        }

        #endregion
            
        #region Properties
		
        public Func<string, IList<SearchForItemsResult>> SearchQueryMethod
        {
            get { return _searchQueryMethod; }
        }

        public Func<string, MethodResult<bool>> SearchTextIsValid
        {
            get { return _searchTextIsValid; }
        } 

        #endregion
    }
}