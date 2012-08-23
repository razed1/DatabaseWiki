using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.ControllerShadowBase;

namespace TableSearch.Mvc.UI.Controllers
{
    public class SearchController : Controller
    {
        #region Fields

        private ISearchControllerShadow _searchControllerShadow;

        #endregion

        #region Actions
        
        [HttpPost]
        public JsonResult SearchForTables(string partialName)
        {
            return SearchControllerShadow.SearchForItems(partialName);
        } 

        #endregion

        #region Properties
        
        private ISearchControllerShadow SearchControllerShadow
        {
            get { return _searchControllerShadow ?? (_searchControllerShadow = new SearchControllerShadow()); }
        } 

        #endregion
    }
}
