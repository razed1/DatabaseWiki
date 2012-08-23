using System.Web.Mvc;
using TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup;

namespace TableSearch.Mvc.Shadow.ControllerShadowBase
{
    public interface IColumnControllerShadow
    {
        JsonResult RetrieveColumnInformationByColumnId(int columnId);
        JsonResult UpdateColumnDescription(int columnId, string description);
    }
}