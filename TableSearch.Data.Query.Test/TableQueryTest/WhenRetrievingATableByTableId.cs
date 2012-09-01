using System.Linq;
using NUnit.Framework;
using FluentAssertions;
using NHibernate.Linq;
using TableSearch.Data.Structure.Entity;
using TableSearch.Data.Structure.Test.Creator;
using TableSearch.Data.Structure.Test.MappingTest;
using TableSearch.Data.Structure.Utlitiy;

namespace TableSearch.Data.Query.Test.TableQueryTest
{
    [TestFixture]
    public class WhenRetrievingATableByTableId : MappingTestBase
    {
        private int _createdId;

        #region Fields

        private const string Name = "thisIsATestNameSoYeah";
        private const string DatabaseName = "databaseName";
        private const string Description = "description";
        private const string Schema = "schema";

        #endregion

        #region Test Hooks

        [SetUp]
        public void TestInitialize()
        {
            using (var session = SessionHelper.CreateASession())
            {
                new TableEntityCreator().Create(session, CleanUp, name: Name, database: DatabaseName, description: Description, schema: Schema);
                _createdId = session.Query<TableEntity>().First(x => x.Name == Name).Id;
            }
        }

        #endregion

        #region Test Methods

        [Test]
        public void TheReturnedResultDatabaseNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                TableQuery
                   .RetrieveTableInformationByTableId(_createdId, session)
                   .DatabaseName
                   .Should()
                   .Be(DatabaseName);
            }
           
        }

        [Test]
        public void TheReturnedResultDescriptionIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                TableQuery
                    .RetrieveTableInformationByTableId(_createdId, session)
                    .Description
                    .Should()
                    .Be(Description);
            }
        }

        [Test]
        public void TheReturnedResultSchemaNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                TableQuery
                    .RetrieveTableInformationByTableId(_createdId, session)
                    .SchemaName
                    .Should()
                    .Be(Schema);
            }
        }

        [Test]
        public void TheReturnedResultTableNameIsCorrect()
        {
            using (var session = SessionHelper.CreateASession())
            {
                TableQuery
                    .RetrieveTableInformationByTableId(_createdId, session)
                    .TableName
                    .Should()
                    .Be(Name);
            }
        }

        #endregion
    }
}