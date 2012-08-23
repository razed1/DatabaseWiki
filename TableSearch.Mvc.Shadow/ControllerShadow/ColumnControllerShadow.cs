using System.Web.Mvc;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Mvc.Shadow.ControllerShadowBase;
using TableSearch.Mvc.Shadow.MethodGroup.ColumnMethodGroup;
using TableSearch.Shared.Functional;

namespace TableSearch.Mvc.Shadow.ControllerShadow
{
    public class ColumnControllerShadow : IColumnControllerShadow
    {
        #region Implementations
        
        public JsonResult RetrieveColumnInformationByColumnId(int columnId)
        {
            return RetrieveColumnInformationByColumnId(columnId, new RetrieveColumnInformationByColumnIdMethodGroup());
        }

        public JsonResult UpdateColumnDescription(int columnId, string description)
        {
            return UpdateColumnDescription(columnId, description, new UpdateColumnDescriptionMethodGroup());
        }

        #endregion

        #region Implementation Extensions

        public JsonResult RetrieveColumnInformationByColumnId(int columnId, RetrieveColumnInformationByColumnIdMethodGroup methodGroup)
        {
            var columnInformation =
                new WithSession(methodGroup.SessionMethod)
                    .ReturnResult(session =>
                            {
                                var result = methodGroup.RetrieveColumnInformationById(columnId, session);
                                GuardClause.IfNullThrowArgumentException(result, "ColumnControllerShadow.");

                                return result;
                            });
            
            return new JsonResult { Data = columnInformation };
        }

        public JsonResult UpdateColumnDescription(int columnId, string description, UpdateColumnDescriptionMethodGroup methodGroup)
        {
            new WithSession(methodGroup.SessionMethod)
                .Do(session =>
                    {
                        GuardClause.IfFalseThenThrowArgumentException(() => methodGroup.ColumnExists(columnId, session), "ColumnControllerShadow.UpdateColumnDescription: column doesn't exist.");
                        methodGroup.UpdateMethod(columnId, description, session);            
                    });
           
            return new JsonResult { Data = description };
        }

        #endregion
    }
}