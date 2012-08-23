using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.ColumnQueryTest
{
    [TestClass]
    public class WhenRetrievingColumnsByTableId : MappingTestBase
    {
        private const int ColumnCount = 3;

        #region Fields

        private TableEntity _parentTable;

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            using (var session = SessionHelper.CreateASession())
            {
                _parentTable = new TableEntityCreator().Create(session, CleanUp);

                var columnCreator = new ColumnEntityCreator();
                Enumerable.Range(0, ColumnCount).Select(x => columnCreator.Create(session, CleanUp, table: _parentTable)).ToList();
            }

        }

        #endregion
        #region Test Methods
        [TestCategory("Integration"), TestMethod]
        public void TheQueryFindsNoValuesSoAnEmptyListIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery
                    .RetrievingColumnsByTableId(-21312231, session)
                    .Any()
                    .Should()
                    .BeFalse();
            }
        }

        [TestCategory("Integration"), TestMethod]
        public void TheQueryFindsItemsSoAListIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery
                    .RetrievingColumnsByTableId(_parentTable.Id, session)
                    .Count()
                    .Should()
                    .Be(ColumnCount);
            }
        }

        #endregion
    }
}