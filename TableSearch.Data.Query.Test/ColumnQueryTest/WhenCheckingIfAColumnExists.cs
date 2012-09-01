using System;
using FluentAssertions;
using NUnit.Framework;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.ColumnQueryTest
{
    [TestFixture]
    public class WhenCheckingIfAColumnExists : MappingTestBase
    {

        #region Fields

        private ColumnEntity _column;

        #endregion

        #region Test Hooks

        [SetUp]
        public void TestInitialize()
        {
            using (var session = SessionHelper.CreateASession())
            {
                _column = new ColumnEntityCreator().Create(session, CleanUp);
            }
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheColumnExistsSoTrueIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.ColumnExists(_column.Id, session).Should().BeTrue(); 
            }
        }

        [Test]
        public void TheColumnDoesNotExistSoFalseIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.ColumnExists(int.MinValue, session).Should().BeFalse();
            }
        }

        #endregion
    }
}