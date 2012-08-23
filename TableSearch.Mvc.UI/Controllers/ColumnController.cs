using System.Web.Mvc;
using TableSearch.Mvc.Shadow.ControllerShadow;
using TableSearch.Mvc.Shadow.ControllerShadowBase;

namespace TableSearch.Mvc.UI.Controllers
{
    public class ColumnController : Controller
    {
        #region Fields
        
        private IColumnControllerShadow _columnControllerShadow; 

        #endregion

        #region Actions
        
        [HttpPost]
        public JsonResult RetrieveColumnInformationByColumnId(int columnId)
        {
            return ColumnControllerShadow.RetrieveColumnInformationByColumnId(columnId);
        }

        [HttpPost]
        public JsonResult UpdateColumnDescription(int columnId, string description)
        {
            return ColumnControllerShadow.UpdateColumnDescription(columnId, description);
        }

        #endregion

        #region Properties
        
        private IColumnControllerShadow ColumnControllerShadow
        {
            get { return _columnControllerShadow ?? (_columnControllerShadow = new ColumnControllerShadow()); }
        } 

        #endregion

    }
}
