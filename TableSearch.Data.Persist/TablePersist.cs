using System.Linq;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Util;
using TableSearch.Data.Structure.Entity;

namespace TableSearch.Data.Persist
{
    public class TablePersist
    {
        public static void UpdateTableDescription(int tableId, string newDescription, ISession session)
        {
            var table = session.Query<TableEntity>().First(x => x.Id == tableId);
            table.Description = newDescription;

            using(var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(table);
                transaction.Commit();
            }
        }
    }
}