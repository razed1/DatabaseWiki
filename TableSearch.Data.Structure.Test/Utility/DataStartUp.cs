using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Structure.Test.Utility
{
    [TestClass]
    public class DataStartUp
    {
        #region Fields
        #endregion

        #region Test Hooks
        #endregion

        #region Test Methods

        [TestCategory("BVT"), TestMethod]
        public void CreateData()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var mappingTestBase = new MappingTestBase();
                var tableCreator = new TableEntityCreator();
                var columnCreator = new ColumnEntityCreator();
                Enumerable
                    .Range(0, 3)
                    .Select(x => tableCreator.Create(session, mappingTestBase.CleanUp))
                    .Select(x => Enumerable.Range(0, 3).Select(inner => columnCreator.Create(session, mappingTestBase.CleanUp, table: x)).ToList())
                    .ToList();
            }
        }

        #endregion
    }
}