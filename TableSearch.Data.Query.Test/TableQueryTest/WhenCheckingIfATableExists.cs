using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.TableQueryTest
{
    [TestClass]
    public class WhenCheckingIfATableExists : MappingTestBase
    {
        #region Fields

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {

        }

        #endregion

        #region Test Methods

        [TestCategory("BVT"), TestMethod]
        public void TheTableExistsSoTrueIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var name = Guid.NewGuid().ToString();

                new TableEntityCreator().Create(session, CleanUp, name);
                var createdId = session.Query<TableEntity>().First(x => x.Name == name).Id;
                TableQuery.TableExists(createdId, session).Should().BeTrue();

            }
        }

        [TestCategory("BVT"), TestMethod]
        public void TheTableDoesNotExistSoFalseIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                TableQuery.TableExists(-12312, session).Should().BeFalse();
            }
        }

        #endregion
    }
}