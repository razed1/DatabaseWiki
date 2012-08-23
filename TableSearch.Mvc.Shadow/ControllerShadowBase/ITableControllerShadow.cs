using System;
using System.Collections.Generic;
using System.Web.Mvc;
using TableSearch.Mvc.Shadow.MethodGroup.TableMethodGroup;

namespace TableSearch.Mvc.Shadow.ControllerShadowBase
{
    public interface ITableControllerShadow
    {
        JsonResult RetrieveTableInformationByTableId(int tableId);
        JsonResult UpdateTableDescription(int id, string description);
    }
}