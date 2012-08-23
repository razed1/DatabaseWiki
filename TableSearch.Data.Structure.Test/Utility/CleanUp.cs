using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;

namespace TableSearch.Data.Structure.Test.Utility
{
    public class CleanUp
    {
        #region Fields

        private readonly IList<int> _columnIdsToDelete;
        private readonly IList<int> _tableIdsToDelete;

        #endregion

        #region Constructors

        public CleanUp()
        {
            _columnIdsToDelete = new List<int>();
            _tableIdsToDelete = new List<int>();
        }

        #endregion

        #region Support Methods

        private void AddIdIfItDoesNotExist<T>(IList<int> listToAddTo, T item) where T : IIdModel
        {
            if (listToAddTo.All(x => x != item.Id))
            {
                listToAddTo.Add(item.Id);
            }
        }

        private void RunDelete<T>(ISession session, IEnumerable<int> idList, Func<int, T> method)
        {
            var createdList = idList.Select(method);

            using (var transaction = session.BeginTransaction())
            {
                foreach (var entity in createdList.Where(entity => entity != null))
                {
                    session.Delete(entity);
                }

                try
                {
                    transaction.Commit();
                }
                catch (Exception)
                {

                }
            }
        }

        #endregion

        #region Methods

        public void AddForDeletion(object item)
        {
            if (item.GetType() == typeof(TableEntity))
            {
                var converted = ((TableEntity)item);

                AddIdIfItDoesNotExist(_tableIdsToDelete, converted);
            }

            if (item.GetType() == typeof(ColumnEntity))
            {
                var converted = ((ColumnEntity)item);

                AddIdIfItDoesNotExist(_columnIdsToDelete, converted);
                AddForDeletion(converted.ParentTable);
            }
        }

        public void DeleteAll(ISession session)
        {
            RunDelete(session, _columnIdsToDelete, (outer => session.Query<ColumnEntity>().FirstOrDefault(inner => inner.Id == outer)));
            RunDelete(session, _tableIdsToDelete, (outer => session.Query<TableEntity>().FirstOrDefault(inner => inner.Id == outer)));
        }

        #endregion
    }
}