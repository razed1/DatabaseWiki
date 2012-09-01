using System;
using NUnit.Framework;
using FluentAssertions;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.ColumnQueryTest
{
    [TestFixture]
    public class WhenRetrievingColumnInformationByColumnId : MappingTestBase
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
        public void AndTheColumnNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.RetrieveColumnInformationByColumnId(_column.Id, session).ColumnName.Should().Be(_column.Name);
            }
        }

        [Test]
        public void AndTheDataTypeIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.RetrieveColumnInformationByColumnId(_column.Id, session).DataType.Should().Be(_column.DataType);
            }
        }

        [Test]
        public void AndTheDescriptionIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.RetrieveColumnInformationByColumnId(_column.Id, session).Description.Should().Be(_column.Description);
            }
        }

        #endregion
    }
}