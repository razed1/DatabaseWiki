using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.ColumnQueryTest
{
    [TestClass]
    public class WhenRetrievingColumnInformationByColumnId : MappingTestBase
    {
        #region Fields

        private ColumnEntity _column;

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            using (var session = SessionHelper.CreateASession())
            {
                _column = new ColumnEntityCreator().Create(session, CleanUp);
            }
        }

        #endregion

        #region Test Methods

        [TestCategory("Integration"), TestMethod]
        public void AndTheColumnNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.RetrieveColumnInformationByColumnId(_column.Id, session).ColumnName.Should().Be(_column.Name);
            }
        }

        [TestCategory("Integration"), TestMethod]
        public void AndTheDataTypeIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                ColumnQuery.RetrieveColumnInformationByColumnId(_column.Id, session).DataType.Should().Be(_column.DataType);
            }
        }

        [TestCategory("Integration"), TestMethod]
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