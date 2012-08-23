using System.Web.Mvc;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Mvc.Shadow.ControllerShadowBase;
using TableSearch.Mvc.Shadow.MethodGroup.TableMethodGroup;
using TableSearch.Shared.Functional;

namespace TableSearch.Mvc.Shadow.ControllerShadow
{
    public class TableControllerShadow : ITableControllerShadow
    {
        #region Implementations

        public JsonResult UpdateTableDescription(int id, string description)
        {
            return UpdateTableDescription(id, description, new UpdateTableDescriptionMethodGroup());
        }
        
        public JsonResult RetrieveTableInformationByTableId(int tableId)
        {
            return RetrieveTableInformationByTableId(tableId, new RetrieveTableInformationMethodGroup());
        } 

        #endregion

        #region Implementation Extensions

        public JsonResult RetrieveTableInformationByTableId(int tableId, RetrieveTableInformationMethodGroup methodGroup)
        {
            var tableResult =
                new WithSession(methodGroup.SessionMethod)
                    .ReturnResult(session =>
                         {
                            var innerResult = methodGroup.SearchForTable(tableId, session);
                            GuardClause.IfNullThrowArgumentException(innerResult,"TableControllerShadow.RetrieveTableInformation: Table doesn't exist.");
                            innerResult.ColumnList = methodGroup.QueryForColumns(innerResult.Id, session);

                            return innerResult;
                        });

            return new JsonResult { Data = tableResult };
        }

        public JsonResult UpdateTableDescription(int id, string description, UpdateTableDescriptionMethodGroup methodGroup)
        {
            new WithSession(methodGroup.SessionMethod)
                .Do(session =>
                    {
                        GuardClause.IfFalseThenThrowArgumentException(() => methodGroup.TableExists(id, session), "TableControllerShadow.UpdateTableDescription: Table doesn't exist.");
                        methodGroup.UpdateMethod(id, description, session);
                    });

            return new JsonResult {Data = description};
        }

        #endregion

       
    }
}