using System.Web.Mvc;
using TableSearch.Mvc.Shadow.MethodGroup.SearchMethodGroup;

namespace TableSearch.Mvc.Shadow.ControllerShadowBase
{
    public interface ISearchControllerShadow
    {
        JsonResult SearchForItems(string searchString);
    }
}