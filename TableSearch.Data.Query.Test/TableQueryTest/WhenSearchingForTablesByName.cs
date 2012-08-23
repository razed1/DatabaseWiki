using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Shared.Test;

namespace TableSearch.Data.Query.Test.TableQueryTest
{
    [TestClass]
    public class WhenSearchingForTablesByName : MappingTestBase
    {
        private string _tableName;

        #region Fields

        #endregion

        #region Test Hooks

        [TestInitialize]
        public void TestInitialize()
        {
            _tableName = RandomTool.RandomString(10);
        }

        #endregion

        #region Test Methods

        [TestCategory("Integration"), TestMethod]
        public void TheQueryFindsNoValuesSoAnEmptyListIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                TableQuery
                   .SearchForTablesByName(RandomTool.RandomString(), session)
                   .Any()
                   .Should()
                   .BeFalse();
            }
           

        }

        [TestCategory("Integration"), TestMethod]
        public void TheQueryFindsValuesSoAListIsReturned()
        {
            using (var session = SessionHelper.CreateASession())
            {
                new TableEntityCreator().Create(session, CleanUp, name: _tableName);
                TableQuery
                   .SearchForTablesByName(_tableName, session)
                   .Any()
                   .Should()
                   .BeTrue();
            }
        }

        [TestCategory("Integration"), TestMethod]
        public void TheReturnedResultDatabaseNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var databaseName = RandomTool.RandomString();

                new TableEntityCreator().Create(session, CleanUp, name: _tableName, database: databaseName);

                TableQuery
                    .SearchForTablesByName(_tableName, session)
                    .First()
                    .DatabaseName
                    .Should()
                    .Be(databaseName);
            }
        }

        [TestCategory("Integration"), TestMethod]
        public void TheReturnedResultSchemaNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                var schemaName = Guid.NewGuid().ToString();

                new TableEntityCreator().Create(session, CleanUp, name: _tableName, schema: schemaName);
                TableQuery
                    .SearchForTablesByName(_tableName, session)
                    .First()
                    .SchemaName
                    .Should()
                    .Be(schemaName);
            }
        }

        [TestCategory("Integration"), TestMethod]
        public void TheReturnedResultTableNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                new TableEntityCreator().Create(session, CleanUp, name: _tableName);
                
                TableQuery
                    .SearchForTablesByName(_tableName, session)
                    .First()
                    .TableName
                    .Should()
                    .Be(_tableName);
            }
        }

        #endregion
    }
}