using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.ControllerShadowBase;

namespace TableSearch.Mvc.UI.Controllers
{
    public class TableController : Controller
    {
        #region Fields
        
        private ITableControllerShadow _tableControllerShadow; 

        #endregion

        #region Actions

        [HttpPost]
        public JsonResult RetrieveTableInformationByTableId(int tableId)
        {
            return TableControllerShadow.RetrieveTableInformationByTableId(tableId);
        }

        [HttpPost]
        public JsonResult UpdateTableDescription(int tableId, string description)
        {
            return TableControllerShadow.UpdateTableDescription(tableId, description);
        }

        #endregion

        #region Properties
        
        private ITableControllerShadow TableControllerShadow
        {
            get { return _tableControllerShadow ?? (_tableControllerShadow = new TableControllerShadow()); }
        } 

        #endregion
    }
}
