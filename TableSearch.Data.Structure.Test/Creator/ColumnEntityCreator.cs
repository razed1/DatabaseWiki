using NHibernate;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Utility;
using TableSearch.Shared.Test;

namespace TableSearch.Data.Structure.Test.Creator
{
    public class ColumnEntityCreator
    {
        public ColumnEntity Create(ISession session, CleanUp cleanUp, TableEntity table = null, string name = null, string datatype = null, string description = null)
        {
            table = table ?? (new TableEntityCreator()).Create(session, cleanUp);

            name = name ?? RandomTool.RandomString(20);
            datatype = datatype ?? RandomTool.RandomString(20);
            description = description ?? RandomTool.RandomString(20);

            var entityToSave = new ColumnEntity
                {
                    DataType = datatype,
                    Description = description,
                    Name = name,
                    ParentTable = table
                };

            using (var transaction = session.BeginTransaction())
            {
                session.Save(entityToSave);
                transaction.Commit();
                cleanUp.AddForDeletion(entityToSave);
            }

            return entityToSave;
        } 
    }
}