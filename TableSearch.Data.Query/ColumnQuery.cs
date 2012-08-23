using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Data.Query
{
    public class ColumnQuery
    {
        public static IEnumerable<ColumnItemResult> RetrievingColumnsByTableId(int tableId, ISession session)
        {
            return session.Query<ColumnEntity>()
                .Where(x => x.ParentTable.Id == tableId)
                .Select(x => new ColumnItemResult { Id = x.Id, ColumnName = x.Name })
                .OrderBy(x => x.ColumnName)
                .ToList();
        }

        public static ColumnInformationResult RetrieveColumnInformationByColumnId(int columnId, ISession session)
        {
            return
                session
                    .Query<ColumnEntity>()
                    .Where(x => x.Id == columnId)
                    .Select(x => new ColumnInformationResult { ColumnId = x.Id, ColumnName = x.Name, DataType = x.DataType, Description = x.Description })
                    .First();
        }

        public static bool ColumnExists(int columnId, ISession session)
        {
            return session.Query<ColumnEntity>().Any(x => x.Id == columnId);
        }
    }
}