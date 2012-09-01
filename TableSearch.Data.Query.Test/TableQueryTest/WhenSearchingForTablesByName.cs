using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;
using TableSearch.Shared.Test;

namespace TableSearch.Data.Query.Test.TableQueryTest
{
    [TestFixture]
    public class WhenSearchingForTablesByName : MappingTestBase
    {
        private string _tableName;

        #region Fields

        #endregion

        #region Test Hooks

        [SetUp]
        public void TestInitialize()
        {
            _tableName = RandomTool.RandomString(10);
        }

        #endregion

        #region Test Methods

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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

        [Test]
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