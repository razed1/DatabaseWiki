using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Shared.WorkflowEntities.Result;

namespace TableSearch.Data.Query
{
    public class TableQuery
    {
        public static IList<SearchForItemsResult> SearchForTablesByName(string name, ISession session)
        {
            return
                session.Query<TableEntity>()
                    .Where(x => x.Name.Contains(name))
                    .OrderBy(x => x.Name)
                    .Select(x => new SearchForItemsResult(x.Id, x.DatabaseName, x.SchemaName, x.Name))
                    .ToList();

        }

        public static TableInformationResult RetrieveTableInformationByTableId(int tableId, ISession session)
        {
            return 
                session.Query<TableEntity>()
                    .Where(x => x.Id == tableId)
                    .Select(x => new TableInformationResult { Id = x.Id, DatabaseName = x.DatabaseName, Description = x.Description, SchemaName = x.SchemaName, TableName = x.Name })
                    .First();
        }

        public static bool TableExists(int tableId, ISession session)
        {
            return session.Query<TableEntity>().Any(x => x.Id == tableId);
        }
    }
}
