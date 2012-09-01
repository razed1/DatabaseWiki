using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.TableQueryTest
{
    [TestFixture]
    public class WhenCheckingIfATableExists : MappingTestBase
    {
        #region Fields

        #endregion

        #region Test Hooks

        [SetUp]
        public void TestInitialize()
        {

        }

        #endregion

        #region Test Methods

        [Test]
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

        [Test]
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