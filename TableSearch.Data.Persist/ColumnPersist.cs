using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;

namespace TableSearch.Data.Persist
{
    public class ColumnPersist
    {
        public static void UpdateColumnDescription(int columnId, string newDescription, ISession session)
        {
            var column = session.Query<ColumnEntity>().First(x => x.Id == columnId);
            column.Description = newDescription;

            using (var transaction = session.BeginTransaction())
            {
                session.SaveOrUpdate(column);
                transaction.Commit();
            }

        }
    }
}