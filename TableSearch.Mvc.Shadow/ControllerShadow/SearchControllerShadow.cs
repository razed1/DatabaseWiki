using System.Collections.Generic;
using System.Web.Mvc;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Mvc.Shadow.ControllerShadowBase;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;
using TableSearch.Mvc.Shadow.Utility;
using TableSearch.Shared.Functional;
using TableSearch.Shared.MethodResult;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Mvc.Shadow.ControllerShadow
{
    public class SearchControllerShadow : ISearchControllerShadow
    {
        #region Implentations
        
        public JsonResult SearchForItems(string searchString)
        {
            return SearchForItems(searchString, new SearchForItemsMethodGroup());
        } 

        #endregion

        #region Implementation Extentions

        public JsonResult SearchForItems(string searchString, SearchForItemsMethodGroup methodGroup)
        {
            var validationResult = methodGroup.SearchTextIsValid(searchString);

            var searchResult =
                When<MethodResult<IList<SearchForItemsResult>>>
                    .True(validationResult.Success)
                    .Then(() => 
                        new WithSession(methodGroup.SessionMethod)
                            .ReturnResult(session => 
                                new MethodResult<IList<SearchForItemsResult>>().SetValue(methodGroup.SearchQueryMethod(searchString, session))))
                    .Else(() => new MethodResult<IList<SearchForItemsResult>>(validationResult.Messages, new List<SearchForItemsResult>()));

            return new JsonResult { Data = new SimpleResult<IList<SearchForItemsResult>>(searchResult) };
        }  

        #endregion
    }
}