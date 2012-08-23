using NHibernate;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Utility;
using TableSearch.Shared.Test;

namespace TableSearch.Data.Structure.Test.Creator
{
    public class TableEntityCreator
    {
        public TableEntity Create(ISession session, CleanUp cleanUp, string name = null, string database = null, string description = null, string schema = null)
        {
            name = name ?? RandomTool.RandomString(20);
            database = database ?? RandomTool.RandomString(20);
            description = description ?? RandomTool.RandomString(20);
            schema = schema ?? RandomTool.RandomString(20);

            var tableToSave = new TableEntity
                {
                    DatabaseName = database,
                    Description = description,
                    Name = name,
                    SchemaName = schema
                };

            using (var transaction = session.BeginTransaction())
            {
                session.Save(tableToSave);
                transaction.Commit();
                cleanUp.AddForDeletion(tableToSave);
            }

            return tableToSave;
        }
    }
}